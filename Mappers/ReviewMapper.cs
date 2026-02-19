using BookStore.Models.DTOs.Review;
using BookStore.Models.Entities;

namespace BookStore.Mappers
{
    public static class ReviewMapper
    {
        public static ReviewDTO ToReviewDTO(this Review review)
        {
            return new ReviewDTO
            {
                Id = review.Id,
                Title = review.Title,
                Content = review.Content,
                Created = review.Created,
                BookId = review.BookId,
                CreatedBy = review.AppUser.UserName
            };
        }
        public static Review ToReviewFromCreateDTO(this CreateReviewRequestDTO reviewDTO, int bookId)
        {
            return new Review
            {
                Title = reviewDTO.Title,
                Content = reviewDTO.Content,
                BookId = bookId
            };
        }

        public static Review ToReviewFromUpdateDTO(this UpdateReviewRequestDTO reviewDTO)
        {
            return new Review
            {
                Title = reviewDTO.Title,
                Content = reviewDTO.Content
            };
        }
    }
}
