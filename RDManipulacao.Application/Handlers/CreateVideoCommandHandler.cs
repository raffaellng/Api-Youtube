using MediatR;
using RDManipulacao.Application.Commands;
using RDManipulacao.Domain.Entities;
using RDManipulacao.Infrastructure.Data;

namespace RDManipulacao.Application.Handlers
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
            var video = new Video
            {
                Title = request.Title,
                Description = request.Description,
                ChannelName = request.ChannelName,
                Duration = request.Duration,
                PublishedAt = request.PublishedAt,
                IsDeleted = false
            };

            _context.Videos.Add(video);
            await _context.SaveChangesAsync(cancellationToken);

            return video.Id;
        }
    }
}
