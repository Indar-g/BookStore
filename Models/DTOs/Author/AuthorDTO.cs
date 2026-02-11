using BookStore.Models.DTOs.Book;

namespace BookStore.Models.DTOs.Author
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<BookDTO> Books { get; set; } = new List<BookDTO>();
    }
}
