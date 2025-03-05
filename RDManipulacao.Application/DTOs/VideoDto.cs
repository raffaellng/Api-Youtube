namespace RDManipulacao.Application.DTOs
{
    public class VideoDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ChannelName { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime PublishedAt { get; set; }
    }
}
