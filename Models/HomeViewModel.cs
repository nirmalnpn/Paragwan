namespace Paragwan.Models
{
    public class HomeViewModel
    {
        public IEnumerable<ParaglidingDetail> Adventures { get; set; }
        public IEnumerable<Photo> Photos { get; set; }
        public IEnumerable<ClientExperience> Experiences { get; set; }
    }
}
