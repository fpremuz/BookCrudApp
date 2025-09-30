using System.ComponentModel.DataAnnotations;

namespace BookCrudApi.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = "";
        
        [Required]
        [MaxLength(100)]
        public string Author { get; set; } = "";
        
        [Range(1, int.MaxValue, ErrorMessage = "Pages must be greater than 0")]
        public int Pages { get; set; }
        
        // Vector embedding for AI-powered search (384 dimensions for all-MiniLM-L6-v2)
        // Stored as JSON string in database
        public string? EmbeddingJson { get; set; }
        
        // Optional: AI-generated summary
        public string? Summary { get; set; }
    }
}
