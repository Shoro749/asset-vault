using Domain.Asset;

namespace Api;

public record AssetDto(
    Guid? Id, 
    string Title, 
    string Description, 
    AssetAttributesDto Attributes)
{
    public static AssetDto FromDomainModel(Asset asset)
        => new(
            Id: asset.Id.Value,
            Title: asset.Title,
            Description: asset.Description,
            Attributes: AssetAttributesDto.FromDomainModel(asset.Attributes));
}

public record AssetAttributesDto(
    int? PolygonCount,
    string? FileFormat,
    string? RenderEngine,
    bool? HasTextures)
{
    public static AssetAttributesDto FromDomainModel(AssetAttributes attributes) 
        => new(
            attributes.PolygonCount,
            attributes.FileFormat,
            attributes.RenderEngine,
            attributes.HasTextures);

    public static AssetAttributes ToDomainModel(AssetAttributesDto attributes)
        => AssetAttributes.New(
            attributes.PolygonCount,
            attributes.FileFormat,
            attributes.RenderEngine,
            attributes.HasTextures);
}