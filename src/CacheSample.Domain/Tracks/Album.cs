using System.Collections.Generic;

namespace CacheSample.Domain.Tracks
{
    public class Album
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<Track> Tracks { get; set; }
    }
}
