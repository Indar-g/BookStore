using System.Security.Claims;

namespace BookStore.Data.Extensions
{
    public static class ClaimsExtensions
    {
        public static string? GetUsername(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.GivenName);
        }
    }
}
