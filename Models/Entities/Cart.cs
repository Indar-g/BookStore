using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models.Entities
{
    [Table("Carts")]
    public class Cart
    {
        public string AppUserId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; } = 1;
        
        public AppUser AppUser { get; set; }
        public Book Book { get; set; }
    }
}
