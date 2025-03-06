using MediatR;
using RDManipulacao.Application.Service;
using RDManipulacao.Domain.Interfaces;

namespace RDManipulacao.Application.Service.InsertVideos
{
    public class FetchAndInsertVideosCommandHandler : IRequestHandler<FetchAndInsertVideosCommand>
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IYouTubeApiService _youTubeApiService;

        public FetchAndInsertVideosCommandHandler(IVideoRepository videoRepository, IYouTubeApiService youTubeApiService)
        {
            _videoRepository = videoRepository;
            _youTubeApiService = youTubeApiService;
        }
        public async Task<Unit> Handle(FetchAndInsertVideosCommand request, CancellationToken cancellationToken)
        {
            var videos = await _youTubeApiService.GetVideosAsync();

            foreach (var video in videos)
            {
                await _videoRepository.CreateAsync(video);
            }

            return Unit.Value;
        }
    }
}
