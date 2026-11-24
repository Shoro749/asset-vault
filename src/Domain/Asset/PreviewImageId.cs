namespace Domain.Asset;

public record PreviewImageId(Guid Value)
{
    public static PreviewImageId Empty() => new(Guid.Empty);
    public static PreviewImageId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}