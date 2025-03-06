using MediatR;
using RDManipulacao.Infrastructure.Data;

namespace RDManipulacao.Application.Video.CreateVideo
{
    public class CreateVideoCommandHandler : IRequestHandler<CreateVideoCommand, int>
    {
        private readonly ApplicationDbContext _context;

        public CreateVideoCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateVideoCommand request, CancellationToken cancellationToken)
        {
            var video = new Domain.Entities.Video(request.Titulo, request.Descricao, request.Autor, request.Duracao, request.DataPublicacao);

            _context.Videos.Add(video);
            await _context.SaveChangesAsync(cancellationToken);

            return video.Id;
        }
    }
}
