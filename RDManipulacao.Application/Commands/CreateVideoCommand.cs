using MediatR;

namespace RDManipulacao.Application.Commands
{
    public record CreateVideoCommand(string Title, string Description, string ChannelName, TimeSpan Duration, DateTime PublishedAt) : IRequest<int>;

}
