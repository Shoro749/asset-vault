namespace Domain.Asset;

public class Asset
{
    public AssetId Id  { get; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime CreatedAt { get; }

    public AssetAttributes Attributes { get; private set; }
    
    public ICollection<PreviewImage> PreviewImages { get; private set; } = [];
    
    public ICollection<AssetCategory>? AssetCategories { get; } = [];

    private Asset(AssetId id, string title, string description, DateTime createdAt, AssetAttributes attributes)
        => (Id, Title, Description, CreatedAt, Attributes) = (id, title, description, createdAt, attributes);

    public static Asset New(AssetId id, string title, string description, AssetAttributes attributes)
        => new(id, title, description, DateTime.UtcNow, attributes);

    public void Update(string title, string description)
    {
        Title = title;
        Description = description;
        UpdatedAt = DateTime.UtcNow;
    }
}