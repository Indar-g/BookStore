namespace BookStore.Helpers
{
    public class ReviewQueryObject
    {
        public string BookTitle { get; set; } = string.Empty;
        public bool IsDescending { get; set; } = true;
    }
}
