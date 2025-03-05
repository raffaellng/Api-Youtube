using MediatR;

namespace RDManipulacao.Application.Commands
{
    public record DeleteVideoCommand(int Id) : IRequest<bool>;
}
