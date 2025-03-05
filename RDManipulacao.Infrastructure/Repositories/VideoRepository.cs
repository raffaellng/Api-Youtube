using Microsoft.EntityFrameworkCore;
using RDManipulacao.Domain.Entities;
using RDManipulacao.Domain.Interfaces;
using RDManipulacao.Infrastructure.Data;

namespace RDManipulacao.Infrastructure.Repositories
{
    public class VideoRepository : IVideoRepository
    {
        private readonly ApplicationDbContext _context;

        public VideoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Video video)
        {
            await _context.Videos.AddAsync(video);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Video>> GetAllAsync()
        {
            return await _context.Videos.ToListAsync();
        }

        public async Task<Video> GetByIdAsync(int id)
        {
            return await _context.Videos.FindAsync(id);
        }

        public async Task RemoveAsync(int id)
        {
            var produto = await _context.Videos.FindAsync(id);
            if (produto != null)
            {
                _context.Videos.Remove(produto);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Video video)
        {
            _context.Videos.Update(video);
            await _context.SaveChangesAsync();
        }
    }
}
