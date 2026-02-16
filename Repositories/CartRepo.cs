using BookStore.Data;
using BookStore.Interfaces;
using BookStore.Models.DTOs.Cart;
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

        public async Task<CartResult<BookCartItemDTO>> AddItemToCart(AppUser user, int bookId)
        {
            var cartItem = await _context.Carts.FirstOrDefaultAsync(c => c.AppUserId == user.Id && c.BookId == bookId);
            if(cartItem != null)
            {
                cartItem.Quantity++;
            }
            else
            {
                var newCartItem = new Cart
                {
                    AppUserId = user.Id,
                    BookId = bookId,
                    Quantity = 1
                };
                _context.Carts.Add(newCartItem);
            }

            await _context.SaveChangesAsync();

            var items = await _context.Carts.Where(c => c.AppUserId == user.Id).Include(c => c.Book).Select(c => new BookCartItemDTO
            {
                Quantity = c.Quantity,
                Id = c.Book.Id,
                Title = c.Book.Title,
                Genre = c.Book.Genre,
                AuthorId = c.Book.AuthorId,
                BookImage = c.Book.BookImage,
                Price = c.Book.Price,
                SubTotal = c.Book.Price * c.Quantity
            }).ToListAsync();

            var TotalPrice = items.Sum(i => i.SubTotal);

            return new CartResult<BookCartItemDTO>
            {
                Total = TotalPrice,
                Data = items
            };

        }

        public async Task<CartResult<BookCartItemDTO>> GetUserCart(AppUser user)
        {
            var items = await _context.Carts.Where(c => c.AppUserId == user.Id).Include(c => c.Book).Select(c => new BookCartItemDTO
            {
                Quantity = c.Quantity,
                Id = c.Book.Id,
                Title = c.Book.Title,
                Genre = c.Book.Genre,
                AuthorId = c.Book.AuthorId,
                BookImage = c.Book.BookImage,
                Price = c.Book.Price,
                SubTotal = c.Book.Price * c.Quantity
            }).ToListAsync();

            var TotalPrice = items.Sum(i => i.SubTotal);

            return new CartResult<BookCartItemDTO>
            {
                Total = TotalPrice,
                Data = items
            };
        }

        public async Task<CartResult<BookCartItemDTO>> RemoveItemFromCart(AppUser user, int bookId)
        {
            var cartItem = await _context.Carts.FirstOrDefaultAsync(c => c.AppUserId == user.Id && c.BookId == bookId);

            if (cartItem != null)
            {
                cartItem.Quantity--;
            }

            if (cartItem == null)
            {
                return null;
            }

            if (cartItem.Quantity == 1)
            {
                _context.Carts.Remove(cartItem);
            } 
            
                await _context.SaveChangesAsync();

            var items = await _context.Carts.Where(c => c.AppUserId == user.Id).Include(c => c.Book).Select(c => new BookCartItemDTO
            {
                Quantity = c.Quantity,
                Id = c.Book.Id,
                Title = c.Book.Title,
                Genre = c.Book.Genre,
                AuthorId = c.Book.AuthorId,
                BookImage = c.Book.BookImage,
                Price = c.Book.Price,
                SubTotal = c.Book.Price * c.Quantity
            }).ToListAsync();

            var TotalPrice = items.Sum(i => i.SubTotal);

            return new CartResult<BookCartItemDTO>
            {
                Total = TotalPrice,
                Data = items
            };
        }
    }
}
