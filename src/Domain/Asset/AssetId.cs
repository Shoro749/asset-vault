namespace Domain.Asset;

public record AssetId(Guid Value)
{
    public static AssetId Empty() => new(Guid.Empty);
    public static AssetId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}