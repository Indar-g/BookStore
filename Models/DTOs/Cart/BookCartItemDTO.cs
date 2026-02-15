namespace BookStore.Models.DTOs.Cart
{
    public class BookCartItemDTO
    {
        public int Quantity { get; set; } = 1;
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? BookImage { get; set; }
        public int AuthorId { get; set; }
        public decimal SubTotal { get; set; }
    }
}
