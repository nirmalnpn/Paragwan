namespace Paragwan.Models
{
    public class Photo
    {
        public int PhotoId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
