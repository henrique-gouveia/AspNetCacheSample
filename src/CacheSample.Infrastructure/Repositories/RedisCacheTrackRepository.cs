using System;
using System.Collections.Generic;

using Microsoft.Extensions.Caching.Distributed;

using CacheSample.Domain.Tracks;
using CacheSample.Infrastructure.Extensions;
using CacheSample.Infrastructure.Commons;

namespace CacheSample.Infrastructure.Repositories
{
    public class RedisCacheTrackRepository : ITrackRepository
    {
        private readonly IDistributedCache cache;
        private readonly ITrackRepository trackRepository;

        public RedisCacheTrackRepository(IDistributedCache cache, ITrackRepository trackRepository)
        {
            this.cache = cache;
            this.trackRepository = trackRepository;
        }

        public IEnumerable<Track> FindAll()
        {
            string cacheEntry = CacheKeys.TracksEntry;
            var tracks = GetOrSet(cacheEntry, TimeSpan.FromSeconds(30), () => trackRepository.FindAll());

            return tracks;
        }

        public IEnumerable<Track> FindByAlbumId(int albumId)
        {
            string cacheEntry = string.Format(CacheKeys.TracksByAlbumIdEntry, albumId);
            var tracks = GetOrSet(cacheEntry, TimeSpan.FromSeconds(15), () => trackRepository.FindByAlbumId(albumId));

            return tracks;
        }

        private IEnumerable<Track> GetOrSet(string entry, TimeSpan slidingExpiration, Func<IEnumerable<Track>> getData)
        {
            // Look for cache key.
            if (!cache.TryGetValue(entry, out IEnumerable<Track> tracks))
            {
                // Key not in cache, so get data.
                tracks = getData();

                // Set cache options.
                var cacheEntryOptions = new DistributedCacheEntryOptions()
                    // Keep in cache for this time, reset time if accessed.
                    .SetSlidingExpiration(slidingExpiration);

                // Save data in cache.
                cache.SetValue(entry, tracks, cacheEntryOptions);
            }

            return tracks;
        }
    }
}
