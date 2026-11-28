namespace Application.Common.Interfaces;

public interface IFileStorage
{
    Task SaveFile(Stream stream, string filePath, CancellationToken cancellationToken);
}