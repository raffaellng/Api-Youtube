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

            video.Update(request.Titulo, request.Descricao, request.Autor, request.Duracao, request.DataPublicacao);
            await _videoRepository.UpdateAsync(video);
            return true;
        }
    }
}
