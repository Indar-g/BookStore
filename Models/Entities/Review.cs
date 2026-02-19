using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models.Entities
{
    [Table("Reviews")]
    public class Review
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime Created { get; set; }

        public Book? Book { get; set; }
        public int BookId { get; set; }

        public AppUser AppUser { get; set; } 
        public string AppUserId { get; set; }
    }
}
