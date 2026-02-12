using BookStore.Helpers;
using BookStore.Models.DTOs.Book;
using BookStore.Models.Entities;
using BookStore.Models.Paging;

namespace BookStore.Interfaces
{
    public interface IBookRepo
    {
        public Task<Book> UpdateAsync(Book book);
        public Task<Book?> GetByIdAsync(int id);
        public Task<bool> ExistsByTitleAndAuthorId(string title, int id);
        public Task<Book> CreateAsync(Book book);
        public Task<PagedResult<Book>> GetAllAsync(QueryObject query);
        public Task<Book?> GetByTitleAsync(string title);
        public Task<Book?> DeleteAsync(int id);
        public Task<bool> ExistsByIdAsync(int id);
    }
}
