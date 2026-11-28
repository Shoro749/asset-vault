using Domain.Asset;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class PreviewImagesConfiguration : IEntityTypeConfiguration<PreviewImage>
{
    public void Configure(EntityTypeBuilder<PreviewImage> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(x => x.Value, x => new PreviewImageId(x));

        builder.Property(x => x.AssetId).HasConversion(x => x.Value, x => new AssetId(x));

        builder.Property(x => x.OriginalName)
            .HasColumnType("varchar(255)")
            .IsRequired();

        builder.HasOne(x => x.Asset)
            .WithMany(x => x.PreviewImages)
            .HasForeignKey(x => x.AssetId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
