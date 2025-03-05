using MediatR;
using RDManipulacao.Application.Queries;
using RDManipulacao.Domain.Entities;
using RDManipulacao.Domain.Interfaces;

namespace RDManipulacao.Application.Handlers
{
    public class GetVideoByIdQueryHandler : IRequestHandler<GetVideoByIdQuery, Video>
    {
        private readonly IVideoRepository _videoRepository;

        public GetVideoByIdQueryHandler(IVideoRepository videoRepository)
        {
            _videoRepository = videoRepository;
        }

        public async Task<Video> Handle(GetVideoByIdQuery request, CancellationToken cancellationToken)
        {
            return await _videoRepository.GetByIdAsync(request.Id);
        }
    }
}
