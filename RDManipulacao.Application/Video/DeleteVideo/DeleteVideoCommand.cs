using MediatR;

namespace RDManipulacao.Application.Video.DeleteVideo
{
    public record DeleteVideoCommand(int Id) : IRequest<bool>;
}
