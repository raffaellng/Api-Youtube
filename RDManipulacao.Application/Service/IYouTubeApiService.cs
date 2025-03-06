namespace RDManipulacao.Application.Service
{
    public interface IYouTubeApiService
    {
        public Task<List<Domain.Entities.Video>> GetVideosAsync();
    }
}
