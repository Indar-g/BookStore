using BookStore.Data;
using BookStore.Interfaces;
using BookStore.Models.DTOs.Author;
using BookStore.Models.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories
{
    public class AuthorRepo : IAuthorRepo
    {
        private readonly AppDbContext _context;

        public AuthorRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Author> CreateAsync(Author authorModel)
        {
            await _context.Authors.AddAsync(authorModel);
            await _context.SaveChangesAsync();

            return authorModel;

        }

        public async Task<Author?> Delete(int id)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id); 
            if(author == null)
            {
                return null;
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return author;
        }

        public async Task<bool> Exists(string name)
        {
            return await _context.Authors.AnyAsync(a => a.Name.ToLower() == name.ToLower());
        }

        public async Task<bool> ExistsById(int id)
        {
            return await _context.Authors.AnyAsync(a => a.Id == id);
        }

        public async Task<List<Author>> GetAllAsync()
        {
            return await _context.Authors.Include(a => a.Books).ThenInclude(b => b.Reviews).ThenInclude(r => r.AppUser).ToListAsync();
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            return await _context.Authors.Include(a => a.Books).ThenInclude(b => b.Reviews).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Author?> GetByNameAsync(string name)
        {
            return await _context.Authors.Include(a => a.Books).ThenInclude(b => b.Reviews).FirstOrDefaultAsync(a => a.Name.ToLower()  == name.ToLower());
        }

        public async Task<Author?> UpdateAsync(int id, UpdateAuthorRequestDTO authorDTO)
        {
            var existingAuthor = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);

            if (existingAuthor == null)
            {
                return null;
            }

            var exists = _context.Authors.FirstOrDefaultAsync(a => a.Name == authorDTO.Name);

            existingAuthor.Name = authorDTO.Name;

            await _context.SaveChangesAsync();

            return existingAuthor;
        }
    }
}
