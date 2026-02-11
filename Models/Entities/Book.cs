namespace BookStore.Models.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? BookImage { get; set; }
        

        public Author? Author { get; set; }
        public int AuthorId { get; set; }

        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}
