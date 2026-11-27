using Domain.Asset;

namespace Application.Assets.Exceptions;

public abstract class AssetException(AssetId assetId, string message, Exception? innerException = null)
    : Exception(message, innerException)
{
    public AssetId AssetId { get; } = assetId;
}

public class AssetAlreadyExistsException(AssetId assetId) : AssetException(assetId, $"Asset already exists under id {assetId}.");

public class AssetNotFoundException(AssetId assetId) : AssetException(assetId, $"Asset with ID {assetId} does not exist.");

public class AssetUnhandledException(AssetId assetId, Exception? innerException = null) 
    : AssetException(assetId, $"An unexpected error occurred while processing asset {{assetId}}.", innerException);