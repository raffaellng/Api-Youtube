using MediatR;

namespace RDManipulacao.Application.Video.CreateVideo
{
    public record CreateVideoCommand(string Titulo, string Descricao, string Autor, TimeSpan Duracao, DateTime DataPublicacao) : IRequest<int>;

}
