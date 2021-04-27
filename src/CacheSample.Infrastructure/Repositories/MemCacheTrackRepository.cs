using System;
using System.Collections.Generic;

using Microsoft.Extensions.Caching.Memory;

using CacheSample.Domain.Tracks;
using CacheSample.Infrastructure.Commons;

namespace CacheSample.Infrastructure.Repositories
{
    public class MemCacheTrackRepository : ITrackRepository
    {
        private readonly IMemoryCache cache;
        private readonly ITrackRepository trackRepository;

        public MemCacheTrackRepository(IMemoryCache memoryCache, ITrackRepository trackRepository)
        {
            cache = memoryCache;
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
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time, reset time if accessed.
                    .SetSlidingExpiration(slidingExpiration);

                // Save data in cache.
                cache.Set(entry, tracks, cacheEntryOptions);
            }

            return tracks;
        }
    }
}
