namespace Domain.Asset;

public class PreviewImage
{
    public PreviewImageId  Id { get; }
    public string OriginalName { get; }

    public string FilePath => $"images/{Id}.jpeg";

    private PreviewImage(PreviewImageId id, string originalName)
        => (Id, OriginalName) = (id, originalName);
    
    public static PreviewImage New(PreviewImageId id, string originalName)
        => new(id, originalName);
}