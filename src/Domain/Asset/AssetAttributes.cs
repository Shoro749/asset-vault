namespace Domain.Asset;

public class AssetAttributes
{
    public int? PolygonCount { get; private set; }
    public string? FileFormat { get; private set; }
    public string? RenderEngine { get; private set; }
    public bool? HasTextures { get; private set; }

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

    public void Update(int? polygonCount, string? fileFormat, string? renderEngine, bool? hasTextures)
    {
        PolygonCount = polygonCount;
        FileFormat = fileFormat;
        RenderEngine = renderEngine;
        HasTextures = hasTextures;
    }
}