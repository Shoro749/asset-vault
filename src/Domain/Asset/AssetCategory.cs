using Domain.Category;

namespace Domain.Asset;

public class AssetCategory
{
    public AssetId AssetId { get; }
    public CategoryId CategoryId { get; }

    public Asset? Asset { get; } = null;
    public Category.Category? Category { get; } = null;

    private AssetCategory(AssetId assetId, CategoryId categoryId)
        => (AssetId, CategoryId) = (assetId, categoryId);

    public static AssetCategory New(AssetId assetId, CategoryId categoryId)
        => new(assetId, categoryId);
}