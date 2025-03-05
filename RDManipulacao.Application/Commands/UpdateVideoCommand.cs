using MediatR;

namespace RDManipulacao.Application.Commands
{
    public record UpdateVideoCommand(int Id, string Titulo, string Descricao, string Autor, TimeSpan Duracao, DateTime DataPublicacao) : IRequest<bool>;
}
