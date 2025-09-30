using Microsoft.EntityFrameworkCore;
using BookCrudApi.Models;

namespace BookCrudApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the Book entity
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Author).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Pages).IsRequired();
                
                // Configure embedding JSON column
                entity.Property(e => e.EmbeddingJson)
                    .HasMaxLength(10000) // JSON string for embedding
                    .HasDefaultValue(null);
                    
                entity.Property(e => e.Summary)
                    .HasMaxLength(1000)
                    .HasDefaultValue(null);
            });
        }
    }
}
