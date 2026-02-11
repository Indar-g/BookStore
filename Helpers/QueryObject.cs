namespace BookStore.Helpers
{
    public class QueryObject
    {
        public string? Title { get; set; } = null;
        public string? Genre { get; set; } = null;
        public string? SortBy { get; set; } = null;
        public bool IsDescending { get; set; } = false;
    }
}
