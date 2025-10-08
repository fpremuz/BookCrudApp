using Microsoft.EntityFrameworkCore;
using BookCrudApi.Data;
using BookCrudApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Listen on HTTP only; Render handles HTTPS termination
builder.WebHost.UseUrls("http://0.0.0.0:8080");

// Add services
builder.Services.AddControllers();

// EF Core with environment variable connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (!string.IsNullOrEmpty(connectionString))
{
    connectionString = connectionString
        .Replace("${DB_HOST}", Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost")
        .Replace("${DB_PORT}", Environment.GetEnvironmentVariable("DB_PORT") ?? "5432")
        .Replace("${DB_NAME}", Environment.GetEnvironmentVariable("DB_NAME") ?? "BookCrudApp")
        .Replace("${DB_USER}", Environment.GetEnvironmentVariable("DB_USER") ?? "postgres")
        .Replace("${DB_PASSWORD}", Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "password");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Add AI service
builder.Services.AddHttpClient<IEmbeddingService, EmbeddingService>();
builder.Services.AddScoped<IEmbeddingService, EmbeddingService>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS — allow everything temporarily
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Middleware
app.UseCors("AllowAll"); // MUST be before MapControllers
app.UseAuthorization();
app.UseHttpsRedirection(); // optional, safe to keep

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

// Ensure DB is created and migrations applied
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Error applying migrations");
    }
}

app.Run();