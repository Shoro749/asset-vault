namespace Domain.Asset;

public class PreviewImage
{
    public PreviewImageId Id { get; }
    public string OriginalName { get; }
    public AssetId AssetId { get; }
    
    public Asset? Asset { get; private set; }

    public string FilePath => $"{AssetId}/{Id}{Path.GetExtension(OriginalName)}";

    private PreviewImage(PreviewImageId id, string originalName, AssetId assetId)
        => (Id, OriginalName, AssetId) = (id, originalName, assetId);
    
    public static PreviewImage New(PreviewImageId id, string originalName, AssetId assetId)
        => new(id, originalName, assetId);
}