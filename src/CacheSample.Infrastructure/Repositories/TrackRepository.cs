using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using CacheSample.Domain.Tracks;
using CacheSample.Infrastructure.Data;

namespace CacheSample.Infrastructure.Repositories
{
    public class TrackRepository : ITrackRepository
    {
        private readonly AppDbContext dbContext;

        public TrackRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Track> FindAll()
        {
            var tracks = dbContext
                .Tracks
                .AsNoTracking()
                .ToList();

            return tracks;
        }

        public IEnumerable<Track> FindByAlbumId(int albumId)
        {
            var tracks = dbContext
                .Tracks
                .Where(t => t.AlbumId == albumId)
                .AsNoTracking()
                .ToList();

            return tracks;
        }
    }
}
