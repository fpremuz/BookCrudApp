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
    }
}
