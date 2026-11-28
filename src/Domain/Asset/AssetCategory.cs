using Domain.Category;

namespace Domain.Asset;

public class AssetCategory
{
    public AssetId AssetId { get; }
    public CategoryId CategoryId { get; }

    public Asset? Asset { get; private set; }
    public Category.Category? Category { get; private set; }

    private AssetCategory(AssetId assetId, CategoryId categoryId)
        => (AssetId, CategoryId) = (assetId, categoryId);

    public static AssetCategory New(AssetId assetId, CategoryId categoryId)
        => new(assetId, categoryId);
}