using MediatR;

namespace RDManipulacao.Application.Commands
{
    public record CreateVideoCommand(string Titulo, string Descricao, string Autor, TimeSpan Duracao, DateTime DataPublicacao) : IRequest<int>;

}
