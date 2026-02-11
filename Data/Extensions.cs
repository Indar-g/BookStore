using Microsoft.EntityFrameworkCore;

namespace BookStore.Data
{
    public static class Extensions
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<AppDbContext>(x =>
            {
                x.UseNpgsql(configuration.GetConnectionString("BookStoreDB"));
            });
            return serviceCollection;
        }
    }
}
