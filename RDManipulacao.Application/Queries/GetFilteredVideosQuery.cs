using MediatR;
using RDManipulacao.Domain.Entities;

namespace RDManipulacao.Application.Queries
{
    public class GetFilteredVideosQuery : IRequest<IEnumerable<Video>>
    {
        public string? Titulo { get; set; }
        public string? Duracao { get; set; }
        public string? Author { get; set; }
        public DateTime? DataPublicacao { get; set; }
        public string? Q { get; set; }

        public GetFilteredVideosQuery(string? titulo, string? duracao, string? author, DateTime? dataPublicacao, string? q)
        {
            Titulo = titulo;
            Duracao = duracao;
            Author = author;
            DataPublicacao = dataPublicacao;
            Q = q;
        }
    }
}
