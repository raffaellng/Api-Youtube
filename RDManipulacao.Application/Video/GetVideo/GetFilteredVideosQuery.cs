using MediatR;

namespace RDManipulacao.Application.Video.GetVideo
{
    public class GetFilteredVideosQuery : IRequest<IEnumerable<Domain.Entities.Video>>
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
