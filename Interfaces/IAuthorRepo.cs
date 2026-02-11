using BookStore.Models.DTOs.Author;
using BookStore.Models.Entities;

namespace BookStore.Interfaces
{
    public interface IAuthorRepo
    {
        Task<List<Author>> GetAllAsync();
        Task<Author?> GetByNameAsync(string name);
        Task<Author> CreateAsync(Author authorModel);
        Task<Author?> UpdateAsync(int id, UpdateAuthorRequestDTO authorDTO);
        Task<Author?> Delete(int id);
        Task<bool> Exists(string name);
        Task<bool> ExistsById(int id);
        Task<Author?> GetByIdAsync(int id);
    }
}
