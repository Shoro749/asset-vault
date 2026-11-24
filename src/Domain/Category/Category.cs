using Domain.Asset;

namespace Domain.Category;

public class Category
{
    public CategoryId Id { get; }
    public string Name { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    public ICollection<AssetCategory>? AssetCategories { get; } = null;

    private Category(CategoryId id, string name, DateTime createdAt)
        => (Id, Name, CreatedAt) = (id, name, createdAt);
    
    public static Category New(CategoryId id, string name, DateTime createdAt)
        => new(id, name, DateTime.UtcNow);

    public void UpdateName(string name) => Name = name;
}