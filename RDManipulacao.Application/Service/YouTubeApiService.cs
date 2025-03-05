using Microsoft.Extensions.Configuration;
using RDManipulacao.Domain.Entities;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace RDManipulacao.Application.Service
{
    public class YouTubeApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public YouTubeApiService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["YouTubeApiKey"];
        }

        public async Task<List<Video>> GetVideosAsync()
        {
            var searchUrl = $"https://www.googleapis.com/youtube/v3/search" +
                              $"?part=snippet" +
                              $"&maxResults=10" +
                              $"&type=video" +
                              $"&q=manipulação%20medicamentos" +
                              $"&regionCode=BR" +
                              $"&publishedAfter=2025-01-01T00:00:00Z" +
                              $"&publishedBefore=2026-01-01T00:00:00Z" +
                              $"&key={_apiKey}";

            var responseSearch = await _httpClient.GetAsync(searchUrl);

            if(!responseSearch.IsSuccessStatusCode)
                throw new Exception("Error fetching YouTube videos. Status: " + responseSearch.StatusCode);

            var response = await _httpClient.GetStringAsync(searchUrl);
            var jsonResponse = JsonSerializer.Deserialize<JsonElement>(response);

            var videos = new List<Video>();

            foreach (var item in jsonResponse.GetProperty("items").EnumerateArray())
            {
                var videoId = item.GetProperty("id").GetProperty("videoId").GetString();
                var detalheUrl = $"https://www.googleapis.com/youtube/v3/videos" +
                                 $"?part=contentDetails" +
                                 $"&id={videoId}" +
                                 $"&key={_apiKey}";

                var videoDetails = await _httpClient.GetStringAsync(detalheUrl);
                var videoDetailsJson = JsonSerializer.Deserialize<JsonElement>(videoDetails);

                var duration = videoDetailsJson.GetProperty("items")[0]
                    .GetProperty("contentDetails")
                    .GetProperty("duration").GetString();

                var video = new Video
                {
                    Title = item.GetProperty("snippet").GetProperty("title").GetString(),
                    Description = item.GetProperty("snippet").GetProperty("description").GetString(),
                    ChannelName = item.GetProperty("snippet").GetProperty("channelTitle").GetString(),
                    PublishedAt = DateTime.Parse(item.GetProperty("snippet").GetProperty("publishedAt").GetString()),
                    Duration = ParseYouTubeDuration(duration), // Para converter a duração para TimeSpan
                    IsDeleted = false
                };

                videos.Add(video);
            }

            return videos;
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
    }
}
