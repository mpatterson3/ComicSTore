namespace ComicSTore.Models
{
    public class Comic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string CoverImageUrl { get; set; } // To store the image URL
    }
}
