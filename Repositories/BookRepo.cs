using BookStore.Data;
using BookStore.Data.Extensions;
using BookStore.Helpers;
using BookStore.Interfaces;
using BookStore.Mappers;
using BookStore.Models.DTOs.Book;
using BookStore.Models.Entities;
using BookStore.Models.Paging;
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

        public async Task<PagedResult<Book>> GetAllAsync(QueryObject query)
        {
            var books = _context.Books.Include(b => b.Author).Include(b => b.Reviews).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Title))
            {
                books = books.Where(b => b.Title.ToLower().Contains(query.Title.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(query.Genre))
            {
                books = books.Where(b => b.Genre.ToLower().Contains(query.Genre.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if(query.SortBy.Equals("Цена", StringComparison.OrdinalIgnoreCase))
                {
                    books = query.IsDescending ? books.OrderByDescending(b => b.Price) : books.OrderBy(b => b.Price);
                }
                else if(query.SortBy.Equals("Айди", StringComparison.OrdinalIgnoreCase))
                {
                    books = query.IsDescending ? books.OrderByDescending(b => b.Id) : books.OrderBy(b => b.Id);
                }
            }

            var totalCount = await books.CountAsync();
            var items = await books //создаем объект который будет показываться с пагинацией
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();
            
            var totalPages = (int)Math.Ceiling(totalCount / (double)query.PageSize); //общее кол-во страниц

            return new PagedResult<Book>
            {
                Data = items,
                CurrentPage = query.PageNumber,
                PageSize = query.PageSize,
                TotalCount = totalCount,
                TotalPages = totalPages,
                HasNext = query.PageNumber < totalPages,
                HasPrevious = query.PageNumber > 1
            };
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
