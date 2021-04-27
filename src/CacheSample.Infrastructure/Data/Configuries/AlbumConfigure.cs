using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using CacheSample.Domain.Tracks;

namespace CacheSample.Infrastructure.Data.Configuries
{
    public class AlbumConfigure : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.ToTable("Album");

            builder.HasKey(a => a.Id);
            builder.Property(t => t.Id).HasColumnName("AlbumId");

            builder.Property(t => t.Name).HasColumnName("Name");
        }
    }
}
