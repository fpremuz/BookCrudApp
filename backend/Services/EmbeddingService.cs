using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using BookCrudApi.Data;
using BookCrudApi.Models;

namespace BookCrudApi.Services
{
    public interface IEmbeddingService
    {
        Task<float[]> GenerateEmbeddingAsync(string text);
        Task StoreEmbeddingAsync(int bookId, float[] embedding);
        Task<List<Book>> SearchSimilarBooksAsync(string query, int limit = 5);
        Task GenerateAndStoreEmbeddingsAsync();
    }

    public class EmbeddingService : IEmbeddingService
    {
        private readonly HttpClient _httpClient;
        private readonly AppDbContext _context;
        private readonly ILogger<EmbeddingService> _logger;

        public EmbeddingService(HttpClient httpClient, AppDbContext context, ILogger<EmbeddingService> logger)
        {
            _httpClient = httpClient;
            _context = context;
            _logger = logger;
        }

        public async Task<float[]> GenerateEmbeddingAsync(string text)
        {
            try
            {
                // Using Hugging Face's free sentence-transformers model
                var requestBody = new
                {
                    inputs = text,
                    options = new { wait_for_model = true }
                };

                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                // Using a free, fast embedding model from Hugging Face
                var response = await _httpClient.PostAsync(
                    "https://api-inference.huggingface.co/models/sentence-transformers/all-MiniLM-L6-v2",
                    content);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"Failed to generate embedding: {response.StatusCode}");
                    throw new Exception($"Failed to generate embedding: {response.StatusCode}");
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                var embeddingArray = JsonSerializer.Deserialize<float[]>(responseContent);

                if (embeddingArray == null || embeddingArray.Length == 0)
                {
                    throw new Exception("Invalid embedding response");
                }

                return embeddingArray;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating embedding for text: {Text}", text);
                throw;
            }
        }

        public async Task StoreEmbeddingAsync(int bookId, float[] embedding)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null)
            {
                throw new ArgumentException($"Book with ID {bookId} not found");
            }

            book.EmbeddingJson = JsonSerializer.Serialize(embedding);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Book>> SearchSimilarBooksAsync(string query, int limit = 5)
        {
            try
            {
                // Generate embedding for the search query
                var queryEmbedding = await GenerateEmbeddingAsync(query);

                // Get all books with embeddings
                var booksWithEmbeddings = await _context.Books
                    .Where(b => b.EmbeddingJson != null)
                    .ToListAsync();

                // Calculate cosine similarity for each book
                var bookSimilarities = new List<(Book book, double similarity)>();

                foreach (var book in booksWithEmbeddings)
                {
                    try
                    {
                        var bookEmbedding = JsonSerializer.Deserialize<float[]>(book.EmbeddingJson!);
                        if (bookEmbedding != null)
                        {
                            var similarity = CalculateCosineSimilarity(queryEmbedding, bookEmbedding);
                            bookSimilarities.Add((book, similarity));
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Failed to deserialize embedding for book {BookId}", book.Id);
                    }
                }

                // Sort by similarity and return top results
                return bookSimilarities
                    .OrderByDescending(x => x.similarity)
                    .Take(limit)
                    .Select(x => x.book)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching similar books for query: {Query}", query);
                throw;
            }
        }

        private double CalculateCosineSimilarity(float[] vectorA, float[] vectorB)
        {
            if (vectorA.Length != vectorB.Length)
                return 0;

            double dotProduct = 0;
            double magnitudeA = 0;
            double magnitudeB = 0;

            for (int i = 0; i < vectorA.Length; i++)
            {
                dotProduct += vectorA[i] * vectorB[i];
                magnitudeA += vectorA[i] * vectorA[i];
                magnitudeB += vectorB[i] * vectorB[i];
            }

            magnitudeA = Math.Sqrt(magnitudeA);
            magnitudeB = Math.Sqrt(magnitudeB);

            if (magnitudeA == 0 || magnitudeB == 0)
                return 0;

            return dotProduct / (magnitudeA * magnitudeB);
        }

        public async Task GenerateAndStoreEmbeddingsAsync()
        {
            var booksWithoutEmbeddings = await _context.Books
                .Where(b => b.EmbeddingJson == null)
                .ToListAsync();

            _logger.LogInformation("Generating embeddings for {Count} books", booksWithoutEmbeddings.Count);

            foreach (var book in booksWithoutEmbeddings)
            {
                try
                {
                    // Create a text representation of the book for embedding
                    var bookText = $"{book.Title} by {book.Author}. {book.Pages} pages.";
                    
                    var embedding = await GenerateEmbeddingAsync(bookText);
                    await StoreEmbeddingAsync(book.Id, embedding);
                    
                    _logger.LogInformation("Generated embedding for book: {Title}", book.Title);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to generate embedding for book: {Title}", book.Title);
                }
            }
        }
    }
}
