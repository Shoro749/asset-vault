namespace Domain.Asset;

public class PreviewImage
{
    public PreviewImageId Id { get; }
    public string OriginalName { get; }
    public AssetId AssetId { get; }
    
    public Asset? Asset { get; }

    public string FilePath => $"images/{Id}.jpeg";

    private PreviewImage(PreviewImageId id, string originalName, AssetId assetId)
        => (Id, OriginalName, AssetId) = (id, originalName, assetId);
    
    public static PreviewImage New(PreviewImageId id, string originalName, AssetId assetId)
        => new(id, originalName, assetId);
}