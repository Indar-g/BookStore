using BookStore.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
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
        public DbSet<Cart> Carts { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Cart>(x => x.HasKey(c => new { c.AppUserId, c.BookId }));

            builder.Entity<Cart>()
                .HasOne(u => u.AppUser)
                .WithMany(u => u.Carts)
                .HasForeignKey(c => c.AppUserId);

            builder.Entity<Cart>()
                .HasOne(u => u.Book)
                .WithMany(u => u.Carts)
                .HasForeignKey(c => c.BookId);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = "AdminId", //обязательно, чтобы EF не ругался при создании миграции 
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = "UserId", //обязательно, чтобы EF не ругался при создании миграции 
                    Name = "User",
                    NormalizedName = "USER"
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
