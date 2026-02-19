using BookStore.Models.Entities;

namespace BookStore.Interfaces
{
    public interface IReviewRepo
    {
        Task <Review> CreateAsync(Review review);
        Task <List<Review>> GetAllAsync();
        Task <Review?> Delete(int id);
        Task<Review?> UpdateAsync(Review review, int id);
        Task<Review?> GetByIdAsync(int id);
        Task<bool> DeleteReviewOfUser(int reviewId, string userId);

    }
}
