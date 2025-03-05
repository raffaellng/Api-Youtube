using Microsoft.EntityFrameworkCore;
using RDManipulacao.Domain.Entities;

namespace RDManipulacao.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Video> Videos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Video>()
            .HasKey(p => p.Id);
        }
    }
}
