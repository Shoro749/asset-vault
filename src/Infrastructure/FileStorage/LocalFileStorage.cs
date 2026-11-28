using Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.FileStorage;

public class LocalFileStorage : IFileStorage
{
    private readonly string _rootPath;

    public LocalFileStorage(IConfiguration configuration)
    {
        _rootPath = configuration["FileStorage:RootPath"] ?? Path.Combine(AppContext.BaseDirectory, "uploads");
    }
        
    public async Task SaveFile(
        Stream stream, 
        string filePath, 
        CancellationToken cancellationToken)
    {
        var fullPath = Path.GetFullPath(Path.Combine(_rootPath, filePath));

        var rootPath = Path.GetFullPath(_rootPath) + Path.DirectorySeparatorChar;

        if (!fullPath.StartsWith(rootPath, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Invalid file path.");
        }

        var directory = Path.GetDirectoryName(fullPath)!;

        Directory.CreateDirectory(directory);

        await using var destination = new FileStream(
            fullPath,
            FileMode.Create,
            FileAccess.Write,
            FileShare.None,
            bufferSize: 81920,
            useAsync: true);

        await stream.CopyToAsync(destination, cancellationToken);
    }
}