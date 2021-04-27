using CacheSample.Domain.Tracks;
using CacheSample.Infrastructure.Data.Configuries;

using Microsoft.EntityFrameworkCore;

namespace CacheSample.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new AlbumConfigure().Configure(modelBuilder.Entity<Album>());
            new TrackConfigure().Configure(modelBuilder.Entity<Track>());
        }

        public DbSet<Album> Albums { get; private set; }
        public DbSet<Track> Tracks { get; private set; }
    }
}
