using BookStore.Models.Entities;

namespace BookStore.Interfaces
{
    public interface IReviewRepo
    {
        Task <Review> CreateAsync(Review review);
        Task <List<Review>> GetAllAsync();
        Task <Review?> Delete(int id);
        Task<Review?> UpdateAsync(Review review, int id);

    }
}
