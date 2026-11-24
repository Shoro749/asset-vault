namespace Domain.Asset;

public class AssetAtributes
{
    public int PolygonCount { get; set; }
    public string FileFormat { get; set; }
    public string RenderEngine { get; set; }
    public bool HasTextures { get; set; }

    private AssetAtributes(int polygonCount, string fileFormat, string renderEngine, bool hasTextures)
    {
        PolygonCount = polygonCount;
        FileFormat = fileFormat;
        RenderEngine = renderEngine;
        HasTextures = hasTextures;
    }
    
    public static AssetAtributes New(int polygonCount, string fileFormat, string renderEngine, bool hasTextures)
        => new(polygonCount, fileFormat, renderEngine, hasTextures);
}