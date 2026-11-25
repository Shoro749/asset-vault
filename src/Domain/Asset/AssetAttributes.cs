namespace Domain.Asset;

public class AssetAttributes
{
    public int? PolygonCount { get; }
    public string? FileFormat { get; }
    public string? RenderEngine { get; }
    public bool? HasTextures { get; }

    private AssetAttributes(int? polygonCount, string? fileFormat, string? renderEngine, bool? hasTextures)
    {
        PolygonCount = polygonCount;
        FileFormat = fileFormat;
        RenderEngine = renderEngine;
        HasTextures = hasTextures;
    }
    
    public static AssetAttributes New(int? polygonCount, string? fileFormat, string? renderEngine, bool? hasTextures)
        => new(polygonCount, fileFormat, renderEngine, hasTextures);
    
    public static AssetAttributes Empty() => new(null, null, null, null);
}