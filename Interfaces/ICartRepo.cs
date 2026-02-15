using BookStore.Models.DTOs.Cart;
using BookStore.Models.Entities;

namespace BookStore.Interfaces
{
    public interface ICartRepo
    {
        Task<CartResult<BookCartItemDTO>> GetUserCart(AppUser user);
        Task<List<BookCartItemDTO>> AddItemToCart(AppUser user, int bookId);
        Task<List<BookCartItemDTO>> RemoveItemFromCart(AppUser user, int bookId);
    }
}
