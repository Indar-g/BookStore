using BookStore.Data;
using BookStore.Interfaces;
using BookStore.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories
{
    public class ReviewRepo : IReviewRepo
    {
        private readonly AppDbContext _context;
        public ReviewRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Review> CreateAsync(Review review)
        {
            await _context.AddAsync(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<Review?> Delete(int id)
        {
            var review = _context.Reviews.FirstOrDefault(x => x.Id == id);

            if (review is null)
            {
                return null;
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<bool> DeleteReviewOfUser(int reviewId, string userId)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == reviewId && r.AppUserId == userId);

            if(review is null)
            {
                return false;
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Review>> GetAllAsync()
        {
            return await _context.Reviews.Include(r => r.AppUser).ToListAsync();
        }

        public async Task<Review?> GetByIdAsync(int id)
        {
            var review = await _context.Reviews.Include(r => r.AppUser).FirstOrDefaultAsync(r => r.Id == id);

            if (review is null)
            {
                return null;
            }

            return review;
        }

        public async Task<Review?> UpdateAsync(Review review, int id)
        {
            var existingReview = await _context.Reviews.FindAsync(id);

            if (existingReview is null)
            {
                return null;
            }

            existingReview.Title = review.Title;
            existingReview.Content = review.Content;
            await _context.SaveChangesAsync();
            return existingReview;
        }
    }
}
