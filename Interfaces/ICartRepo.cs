using BookStore.Models.Entities;

namespace BookStore.Interfaces
{
    public interface ICartRepo
    {
        Task<List<Book>> GetUserCart(AppUser user);
    }
}
