using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.DTOs.Author
{
    public class CreateAuthorRequestDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Имя должно быть не менее 3 символов")]
        [MaxLength(30, ErrorMessage ="Имя должно быть не более 30 символов")]
        public required string Name { get; set; }
    }
}
