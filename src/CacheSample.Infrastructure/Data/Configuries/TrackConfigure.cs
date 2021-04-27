using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using CacheSample.Domain.Tracks;

namespace CacheSample.Infrastructure.Data.Configuries
{
    public class TrackConfigure : IEntityTypeConfiguration<Track>
    {
        public void Configure(EntityTypeBuilder<Track> builder)
        {
            builder.ToTable("Track");
            
            builder.HasIndex(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("TrackId");

            builder.HasOne(t => t.Album).WithMany(a => a.Tracks).HasForeignKey(t => t.AlbumId);

            builder.Property(t => t.Name).HasColumnName("Name");
            builder.Property(t => t.Composer).HasColumnName("Composer");
            builder.Property(t => t.Bytes).HasColumnName("Bytes");
            builder.Property(t => t.Milliseconds).HasColumnName("Milliseconds");
            builder.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
        }
    }
}
