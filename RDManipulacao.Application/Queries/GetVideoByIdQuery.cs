using MediatR;
using RDManipulacao.Domain.Entities;

namespace RDManipulacao.Application.Queries
{
    public record GetVideoByIdQuery(int Id) : IRequest<Video>;
}
