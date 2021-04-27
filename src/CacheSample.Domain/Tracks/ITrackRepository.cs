using System.Collections.Generic;

namespace CacheSample.Domain.Tracks
{
    public interface ITrackRepository
    {
        IEnumerable<Track> FindAll();

        IEnumerable<Track> FindByAlbumId(int albumId);
    }
}
