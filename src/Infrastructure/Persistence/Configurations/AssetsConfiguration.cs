using Domain.Asset;
using Infrastructure.Persistence.Conventers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class AssetsConfiguration : IEntityTypeConfiguration<Asset>
{
    public void Configure(EntityTypeBuilder<Asset> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(x => x.Value, x => new AssetId(x));

        builder.Property(x => x.Title)
            .HasColumnType("varchar(255)")
            .IsRequired();
        
        builder.Property(x => x.Description)
            .HasColumnType("varchar(255)")
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .HasConversion(new DateTimeUtcConverter());
        
        builder.Property(x => x.CreatedAt)
            .HasConversion(new DateTimeUtcConverter())
            .HasDefaultValueSql("timezone('utc', now())")
            .IsRequired();

        builder.OwnsOne<AssetAttributes>(x => x.Attributes, attributesBuilder =>
        {
            attributesBuilder.ToJson("attributes");

            attributesBuilder.Property(x => x.FileFormat).HasJsonPropertyName("file_format");
            attributesBuilder.Property(x => x.HasTextures).HasJsonPropertyName("has_textures");
            attributesBuilder.Property(x => x.PolygonCount).HasJsonPropertyName("polygon_count");
            attributesBuilder.Property(x => x.RenderEngine).HasJsonPropertyName("render_engine");
        });
    }
}