using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.DTOs.Review
{
    public class CreateReviewRequestDTO
    {
        [Required(ErrorMessage = "Название обязательно!")]
        [MinLength(3, ErrorMessage = "Название должно быть не менее 3 символов")]
        [MaxLength(30, ErrorMessage = "Название должно быть не более 30 символов")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Описание обязательно!")]
        [MinLength(3, ErrorMessage = "Описание должно быть не менее 3 символов")]
        [MaxLength(30, ErrorMessage = "Описание должно быть не более 30 символов")]
        public string Content { get; set; } = string.Empty;
    }
}
