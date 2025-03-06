using MediatR;

namespace RDManipulacao.Application.Video.GetVideo
{
    public record GetVideoByIdQuery(int Id) : IRequest<Domain.Entities.Video>;
}
