namespace BookStore.Models.DTOs.Review
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; } = string.Empty;
        public int? BookId { get; set; }

    }
}
