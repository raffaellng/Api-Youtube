using RDManipulacao.Domain.Entities;

namespace RDManipulacao.Domain.Interfaces
{
    public interface IVideoRepository
    {
        Task<Video> GetByIdAsync(int id);
        Task<IEnumerable<Video>> GetAllAsync();
        Task CreateAsync(Video video);
        Task UpdateAsync(Video video);
        Task RemoveAsync(int id);
    }
}
