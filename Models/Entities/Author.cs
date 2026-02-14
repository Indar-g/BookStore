using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models.Entities
{
    [Table("Authors")]
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<Book> Books { get; set; } = new List<Book>();
    }
}
