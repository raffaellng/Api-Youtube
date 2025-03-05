using System.Text.Json.Serialization;

namespace RDManipulacao.Domain.Entities
{
    public class Video
    {
        public int Id { get; set; }

        [JsonPropertyName("titulo")]
        public string? Title { get; set; }

        [JsonPropertyName("duracao")]
        public TimeSpan? Duration { get; set; }

        [JsonPropertyName("dataPublicacao")]
        public DateTime? PublishedAt { get; set; }

        [JsonPropertyName("descricao")]
        public string? Description { get; set; }

        [JsonPropertyName("autor")]
        public string? ChannelName { get; set; }

        [JsonPropertyName("excluido")]
        public bool IsDeleted { get; set; }

        public Video() { }

        public Video(string title, string description, string channelName, TimeSpan duration, DateTime publishedAt)
        {
            ValidateDomain(title, description, channelName, duration, publishedAt);
            IsDeleted = false;
        }

        public void Update(string title, string description, string channelName, TimeSpan duration, DateTime publishedAt)
        {
            ValidateDomain(title, description, channelName, duration, publishedAt);
        }

        public void Delete()
        {
            IsDeleted = true;
        }

        private void ValidateDomain(string title, string description, string channelName, TimeSpan duration, DateTime publishedAt)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty");

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description cannot be empty");

            if (string.IsNullOrWhiteSpace(channelName))
                throw new ArgumentException("Channel name cannot be empty");

            if (duration.TotalSeconds <= 0)
                throw new ArgumentException("Duration must be greater than zero");

            if (publishedAt.Year != 2025)
                throw new ArgumentException("Only videos from 2025 are allowed");

            Title = title;
            Description = description;
            ChannelName = channelName;
            Duration = duration;
            PublishedAt = publishedAt;
        }
    }
}
