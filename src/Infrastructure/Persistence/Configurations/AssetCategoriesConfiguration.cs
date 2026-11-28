using Domain.Asset;
using Domain.Category;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class AssetCategoriesConfiguration : IEntityTypeConfiguration<AssetCategory>
{
    public void Configure(EntityTypeBuilder<AssetCategory> builder)
    {
        builder.HasKey(x => new { x.AssetId, x.CategoryId });

        builder.Property(x => x.AssetId) .HasConversion(x => x.Value, x => new AssetId(x));

        builder.Property(x => x.CategoryId) .HasConversion(x => x.Value, x => new CategoryId(x));

        builder.HasOne(x => x.Asset)
            .WithMany(x => x.AssetCategories)
            .HasForeignKey(x => x.AssetId)
            .HasConstraintName("fk_asset_categories_asset_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Category)
            .WithMany(x => x.AssetCategories)
            .HasForeignKey(x => x.CategoryId)
            .HasConstraintName("fk_asset_categories_categories_id")
            .OnDelete(DeleteBehavior.Restrict);
    }
}
