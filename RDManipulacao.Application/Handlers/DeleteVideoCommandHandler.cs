using MediatR;
using RDManipulacao.Application.Commands;
using RDManipulacao.Domain.Interfaces;

namespace RDManipulacao.Application.Handlers
{
    public class DeleteVideoCommandHandler : IRequestHandler<DeleteVideoCommand, bool>
    {
        private readonly IVideoRepository _videoRepository;

        public DeleteVideoCommandHandler(IVideoRepository videoRepository)
        {
            _videoRepository = videoRepository;
        }

        public async Task<bool> Handle(DeleteVideoCommand request, CancellationToken cancellationToken)
        {
            var video = await _videoRepository.GetByIdAsync(request.Id);
            if (video == null) return false;

            video.Delete();
            await _videoRepository.UpdateAsync(video);
            return true;
        }
    }
}
