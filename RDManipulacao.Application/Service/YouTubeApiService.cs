using Microsoft.Extensions.Configuration;
using RDManipulacao.Domain.Validation;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace RDManipulacao.Application.Service
{
    public class YouTubeApiService : IYouTubeApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public YouTubeApiService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["YouTubeApiKey"];

            if (string.IsNullOrWhiteSpace(_apiKey))
                DomainValidation.When(true, "YouTube API Key is missing or invalid.");
        }

        public async Task<List<Domain.Entities.Video>> GetVideosAsync()
        {
            if (!await IsYouTubeApiOnline())
                DomainValidation.When(true, "YouTube API is currently unavailable.");

            var searchUrl = $"https://www.googleapis.com/youtube/v3/search" +
                              $"?part=snippet" +
                              $"&maxResults=10" +
                              $"&type=video" +
                              $"&q=manipulação%20medicamentos" +
                              $"&regionCode=BR" +
                              $"&publishedAfter=2025-01-01T00:00:00Z" +
                              $"&publishedBefore=2025-12-31T00:00:00Z" +
                              $"&key={_apiKey}";

            try
            {
                var response = await _httpClient.GetAsync(searchUrl);

                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Error fetching YouTube videos. Status: {response.StatusCode}");

                var responseBody = await response.Content.ReadAsStringAsync();
                var jsonResponse = JsonSerializer.Deserialize<JsonElement>(responseBody);

                if (!jsonResponse.TryGetProperty("items", out var itemsArray) || itemsArray.GetArrayLength() == 0)
                    DomainValidation.When(true, "No videos found or invalid API response.");

                var videos = new List<Domain.Entities.Video>();

                foreach (var item in itemsArray.EnumerateArray())
                {
                    var videoId = item.GetProperty("id").GetProperty("videoId").GetString();
                    var video = await GetVideoDetailsAsync(videoId);

                    if (video != null)
                    {
                        videos.Add(video);
                    }
                }

                return videos;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Network error while fetching videos: " + ex.Message);
            }
            catch (JsonException ex)
            {
                throw new Exception("Error parsing YouTube API response: " + ex.Message);
            }
        }

        private async Task<Domain.Entities.Video?> GetVideoDetailsAsync(string videoId)
        {
            var detailUrl = $"https://www.googleapis.com/youtube/v3/videos" +
                            $"?part=contentDetails,snippet" +
                            $"&id={videoId}" +
                            $"&key={_apiKey}";

            try
            {
                var response = await _httpClient.GetAsync(detailUrl);

                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Error fetching video details. Status: {response.StatusCode}");

                var responseBody = await response.Content.ReadAsStringAsync();
                var videoDetailsJson = JsonSerializer.Deserialize<JsonElement>(responseBody);

                if (!videoDetailsJson.TryGetProperty("items", out var itemsArray) || itemsArray.GetArrayLength() == 0)
                    return null;

                var videoItem = itemsArray[0];

                var durationString = videoItem.GetProperty("contentDetails").GetProperty("duration").GetString();
                var duration = ParseYouTubeDuration(durationString);

                return new Domain.Entities.Video
                {
                    Title = videoItem.GetProperty("snippet").GetProperty("title").GetString(),
                    Description = videoItem.GetProperty("snippet").GetProperty("description").GetString(),
                    ChannelName = videoItem.GetProperty("snippet").GetProperty("channelTitle").GetString(),
                    PublishedAt = DateTime.Parse(videoItem.GetProperty("snippet").GetProperty("publishedAt").GetString()),
                    Duration = duration,
                    IsDeleted = false
                };
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Network error while fetching video details: " + ex.Message);
            }
            catch (JsonException ex)
            {
                throw new Exception("Error parsing YouTube API response for details: " + ex.Message);
            }
        }

        private TimeSpan ParseYouTubeDuration(string duration)
        {
            var regex = new Regex(@"PT(?:(\d+)H)?(?:(\d+)M)?(?:(\d+)S)?");
            var match = regex.Match(duration);

            var hours = match.Groups[1].Success ? int.Parse(match.Groups[1].Value) : 0;
            var minutes = match.Groups[2].Success ? int.Parse(match.Groups[2].Value) : 0;
            var seconds = match.Groups[3].Success ? int.Parse(match.Groups[3].Value) : 0;

            return new TimeSpan(hours, minutes, seconds);
        }

        private async Task<bool> IsYouTubeApiOnline()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://www.googleapis.com/youtube/v3/search?key=" + _apiKey);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
