using System.Net.Http;

namespace ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var youtubeApiKey = "AIzaSyBXqqfwhrUMd4tV1yu_CF1hb2dCFhJpsHI";
            //var youtubeApiKey = Environment.GetEnvironmentVariable("YOUTUBE_API_KEY");

            if (string.IsNullOrEmpty(youtubeApiKey))
            {
                Console.WriteLine("A chave da API do YouTube não foi configurada.");
                return;
            }

            Console.WriteLine("Chave da API do YouTube configurada com sucesso!");

            // Aqui você pode instanciar serviços ou chamar métodos
            var youtubeService = new YouTubeService(youtubeApiKey);
            await youtubeService.BuscarVideos("Tratamento Global de Erros (Global Error Handling) em Minimal APIs com C#");

        }
    }

    internal class YouTubeService
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        public YouTubeService(string apiKey)
        {
            _apiKey = apiKey;
            _httpClient = new HttpClient();
        }

        public void TestarChamada()
        {
            Console.WriteLine($"Chave da API: {_apiKey}");
        }

        public async Task BuscarVideos(string query)
        {
            try
            {
                var url = $"https://www.googleapis.com/youtube/v3/search?part=snippet&q={Uri.EscapeDataString(query)}&type=video&key={_apiKey}";

                    var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Resposta da API do YouTube:");
                Console.WriteLine(content);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erro ao acessar a API do YouTube: {ex.Message}");
            }
        }
    }
}
