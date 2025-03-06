using RDManipulacao.Domain.Validation;
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

            DomainValidation.When(string.IsNullOrEmpty(title), "Title cannot be empty");
            DomainValidation.When(string.IsNullOrEmpty(description), "Description cannot be empty");
            DomainValidation.When(string.IsNullOrEmpty(channelName), "Channel name cannot be empty");
            DomainValidation.When(duration.TotalSeconds <= 0, "Duration must be greater than zero");
            DomainValidation.When(publishedAt.Year != 2025, "Only videos from 2025 are allowed");

            Title = title;
            Description = description;
            ChannelName = channelName;
            Duration = duration;
            PublishedAt = publishedAt;
        }
    }
}
