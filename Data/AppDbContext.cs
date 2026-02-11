using BookStore.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration Configuration;
        public AppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(Configuration.GetConnectionString("BookStoreDB"));
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
