using Domain.Asset;

namespace Domain.Category;

public class Category
{
    public CategoryId Id { get; }
    public string Name { get; private set; }
    public DateTime CreatedAt { get; }
    
    public ICollection<AssetCategory>? AssetCategories { get; } = [];

    private Category(CategoryId id, string name, DateTime createdAt)
        => (Id, Name, CreatedAt) = (id, name, createdAt);
    
    public static Category New(CategoryId id, string name)
        => new(id, name, DateTime.UtcNow);

    public void UpdateName(string name) => Name = name;
}