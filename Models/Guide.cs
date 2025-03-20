namespace Paragwan.Models
{
    public class Guide
    {
        public int GuideId { get; set; }
        public int UserId { get; set; }
        public string Bio { get; set; }
        public int ExperienceYears { get; set; }
        public decimal Ratings { get; set; }
    }
}
