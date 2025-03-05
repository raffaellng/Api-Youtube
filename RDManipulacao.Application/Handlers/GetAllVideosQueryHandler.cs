//using MediatR;
//using RDManipulacao.Application.Queries;
//using RDManipulacao.Domain.Entities;
//using RDManipulacao.Domain.Interfaces;

//namespace RDManipulacao.Application.Handlers
//{
//    public class GetAllVideosQueryHandler : IRequestHandler<GetAllVideosQuery, IEnumerable<Video>>
//    {
//        private readonly IVideoRepository _videoRepository;

//        public GetAllVideosQueryHandler(IVideoRepository videoRepository)
//        {
//            _videoRepository = videoRepository;
//        }

//        public async Task<IEnumerable<Video>> Handle(GetAllVideosQuery request, CancellationToken cancellationToken)
//        {
//            return await _videoRepository.GetAllAsync();
//        }
//    }
//}
