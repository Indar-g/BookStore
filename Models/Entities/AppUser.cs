using Microsoft.AspNetCore.Identity;

namespace BookStore.Models.Entities
{
    public class AppUser : IdentityUser
    {
        public List<Cart> Carts { get; set; } = new List<Cart>();
    }
}
