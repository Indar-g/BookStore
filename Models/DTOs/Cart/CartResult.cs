namespace BookStore.Models.DTOs.Cart
{
    public class CartResult<T>
    {
        public decimal Subtotal { get; set; }
        public IEnumerable<BookCartItemDTO> Data { get; set; } = [];

    }
}
