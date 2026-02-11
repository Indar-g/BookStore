using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.DTOs.Author
{
    public class UpdateAuthorRequestDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Имя должно быть не менее 3 символов")]
        [MaxLength(30, ErrorMessage = "Имя должно быть не более 30 символов")]
        public string Name { get; set; } = string.Empty;
    }
}
