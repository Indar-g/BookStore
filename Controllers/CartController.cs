using BookStore.Data.Extensions;
using BookStore.Interfaces;
using BookStore.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IBookRepo _bookRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICartRepo _cartRepo;
        public CartController(UserManager<AppUser> userManager, IBookRepo bookRepo, ICartRepo cartRepo)
        {
            _userManager = userManager;
            _bookRepo = bookRepo;
            _cartRepo = cartRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCart()
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username); //всё нормально, null не будет из-за того что есть [Authorize]
            var userCart = await _cartRepo.GetUserCart(appUser);
            return Ok(userCart);

        }
    }
}
