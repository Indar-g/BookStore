using BookStore.Models.DTOs.Review;

namespace BookStore.Models.DTOs.Book
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? BookImage { get; set; }

        public int AuthorId { get; set; }
        public List<ReviewDTO> Reviews { get; set; } = new List<ReviewDTO>();

    }
}
