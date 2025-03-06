using MediatR;
using RDManipulacao.Domain.Interfaces;

namespace RDManipulacao.Application.Video.GetVideo
{
    public class GetVideoByIdQueryHandler : IRequestHandler<GetVideoByIdQuery, Domain.Entities.Video>
    {
        private readonly IVideoRepository _videoRepository;

        public GetVideoByIdQueryHandler(IVideoRepository videoRepository)
        {
            _videoRepository = videoRepository;
        }

        public async Task<Domain.Entities.Video> Handle(GetVideoByIdQuery request, CancellationToken cancellationToken)
        {
            return await _videoRepository.GetByIdAsync(request.Id);
        }
    }
}
