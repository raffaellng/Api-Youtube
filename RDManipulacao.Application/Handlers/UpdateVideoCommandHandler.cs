using MediatR;
using RDManipulacao.Application.Commands;
using RDManipulacao.Domain.Interfaces;

namespace RDManipulacao.Application.Handlers
{
    public class UpdateVideoCommandHandler : IRequestHandler<UpdateVideoCommand, bool>
    {
        private readonly IVideoRepository _videoRepository;

        public UpdateVideoCommandHandler(IVideoRepository videoRepository)
        {
            _videoRepository = videoRepository;
        }

        public async Task<bool> Handle(UpdateVideoCommand request, CancellationToken cancellationToken)
        {
            var video = await _videoRepository.GetByIdAsync(request.Id);
            if (video == null) return false;

            video.Update(request.Title, request.Description, request.ChannelName, request.Duration, request.PublishedAt);
            await _videoRepository.UpdateAsync(video);
            return true;
        }
    }
}
