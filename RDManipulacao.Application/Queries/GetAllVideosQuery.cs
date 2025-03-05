using MediatR;
using RDManipulacao.Domain.Entities;

namespace RDManipulacao.Application.Queries
{
    public record GetAllVideosQuery() : IRequest<IEnumerable<Video>>;
}
