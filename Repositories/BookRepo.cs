using BookStore.Data;
using BookStore.Interfaces;
using BookStore.Models.DTOs.Book;
using BookStore.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace BookStore.Repositories
{
    public class BookRepo : IBookRepo
    {
        private readonly AppDbContext _context;
        public BookRepo(AppDbContext context)
        {
            _context = context;
        }

        

        public async Task<Book> CreateAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<Book?> DeleteAsync(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return null;
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return book;
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            return await _context.Books.AnyAsync(b => b.Id == id);
        }

        public async Task<bool> ExistsByTitleAndAuthorId(string title, int id)
        {
            return await _context.Books.AnyAsync(b =>
            b.Title.ToLower() == title.ToLower() &&
            b.AuthorId == id);
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _context.Books.Include(b => b.Author).Include(b => b.Reviews).ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {   
            return await _context.Books.Include(b => b.Author).Include(b => b.Reviews).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Book?> GetByTitleAsync(string title)
        {
            return await _context.Books.Include(b => b.Author).Include(b => b.Reviews).FirstOrDefaultAsync(b => b.Title.ToLower() == title.ToLower());
        }

        public async Task<Book> UpdateAsync(Book book)
        {
            //_context.Books.Update(book); - update() не нужен потому что в контроллере я уже получаю книгу по GetByIdAsync()
            await _context.SaveChangesAsync();
            return book;
        }

        
    }
}
