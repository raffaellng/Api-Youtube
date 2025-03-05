using MediatR;

namespace RDManipulacao.Application.Commands
{
    public record UpdateVideoCommand(int Id, string Title, string Description, string ChannelName, TimeSpan Duration, DateTime PublishedAt) : IRequest<bool>;
}
