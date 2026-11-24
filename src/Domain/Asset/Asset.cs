namespace Domain.Asset;

public class Asset
{
    public AssetId Id  { get; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public DateTime CreatedAt { get; }

    public List<AssetAtributes> Atributes { get; private set; } = [];
    
    public ICollection<AssetCategory>? AssetCategories { get; } = null;

    private Asset(AssetId id, string title, string description, DateTime createdAt)
        => (Id, Title, Description, CreatedAt) = (id, title, description, createdAt);
    
    public static Asset New(AssetId id, string title, string description, List<AssetAtributes> atributes)
        => new(id, title, description, DateTime.UtcNow)
        {
            Atributes = atributes
        };

    public void Update(string title, string description)
    {
        Title = title;
        Description = description;
    }
}