using BookStore.Data;
using BookStore.Interfaces;
using BookStore.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories
{
    public class CartRepo : ICartRepo
    {
        private readonly AppDbContext _context;
        public CartRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Book>> GetUserCart(AppUser user)
        {
            return await _context.Carts.Where(u => u.AppUserId == user.Id)
                .Select(book => new Book
                {
                    Id = book.BookId,
                    Title = book.Book.Title,
                    Price = book.Book.Price,
                    Genre = book.Book.Genre,
                    BookImage = book.Book.BookImage,
                    AuthorId = book.Book.AuthorId
                }).ToListAsync();
        }
    }
}
