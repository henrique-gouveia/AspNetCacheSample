namespace CacheSample.Infrastructure.Commons
{
    public static class CacheKeys
    {
        public static string TracksEntry => "@CacheSample::Tracks";
        public static string TracksByAlbumIdEntry => "@CacheSample::Tracks:Albums:{0}";
    }
}
