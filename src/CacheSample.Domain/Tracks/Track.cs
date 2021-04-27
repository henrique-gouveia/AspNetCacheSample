﻿namespace CacheSample.Domain.Tracks
{
    public class Track
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int AlbumId { get; set; }
        public Album Album { get; set; }

        public string Composer { get; set; }

        public int Milliseconds { get; set; }

        public int Bytes { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
