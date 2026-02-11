using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.DTOs.Book
{
    public class UpdateBookRequestDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Название должно быть не менее 3 символов")]
        [MaxLength(30, ErrorMessage = "Название должно быть не более 30 символов")]
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        [Required]
        [Range(-9999, 100000)]
        public decimal Price { get; set; }
        
        public int AuthorId { get; set; }

        public IFormFile? BookImage { get; set; }
    }
}
