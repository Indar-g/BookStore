using BookStore.Models.Entities;

namespace BookStore.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user, IList<string> roles);
    }
}
