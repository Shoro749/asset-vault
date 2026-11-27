using Domain.Asset;

namespace Application.Assets.Exceptions;

public abstract class AssetException(AssetId assetId, string message, Exception? innerException = null)
    : Exception(message, innerException)
{
    public AssetId AssetId { get; } = assetId;
}

public class AssetAlreadyExistsException(AssetId assetId) : AssetException(assetId, $"Asset with ID {assetId} already exists.");

public class AssetNotFoundException(AssetId assetId) : AssetException(assetId, $"Asset with ID {assetId} does not exist.");

public class AssetUnhandledException(AssetId assetId, Exception? innerException = null) 
    : AssetException(assetId, $"Unhandled asset with ID {assetId} unhandled.", innerException);