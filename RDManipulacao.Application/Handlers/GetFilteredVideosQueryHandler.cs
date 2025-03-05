using MediatR;
using RDManipulacao.Application.Queries;
using RDManipulacao.Domain.Entities;
using RDManipulacao.Domain.Interfaces;

namespace RDManipulacao.Application.Handlers
{
    public class GetFilteredVideosQueryHandler : IRequestHandler<GetFilteredVideosQuery, IEnumerable<Video>>
    {
        private readonly IVideoRepository _videoRepository;

        public GetFilteredVideosQueryHandler(IVideoRepository videoRepository)
        {
            _videoRepository = videoRepository;
        }
        public async Task<IEnumerable<Video>> Handle(GetFilteredVideosQuery request, CancellationToken cancellationToken)
        {
            var videos = await _videoRepository.GetAllAsync();

            videos = videos.Where(v => !v.IsDeleted);

            if (!string.IsNullOrEmpty(request.Titulo))
                videos = videos.Where(v => v.Title.Contains(request.Titulo, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(request.Duracao) && TimeSpan.TryParseExact(request.Duracao, @"hh\:mm\:ss", null, out var parsedDuracao))
                videos = videos.Where(v => v.Duration == parsedDuracao);

            if (!string.IsNullOrEmpty(request.Author))
                videos = videos.Where(v => v.ChannelName.Contains(request.Author, StringComparison.OrdinalIgnoreCase));

            if (request.DataPublicacao.HasValue)
                videos = videos.Where(v => v.PublishedAt >= request.DataPublicacao.Value);

            if (!string.IsNullOrEmpty(request.Q))
                videos = videos.Where(v => v.Title.Contains(request.Q, StringComparison.OrdinalIgnoreCase) ||
                                           v.Description.Contains(request.Q, StringComparison.OrdinalIgnoreCase) ||
                                           v.ChannelName.Contains(request.Q, StringComparison.OrdinalIgnoreCase));

            return videos.ToList();
        }
    }
}
