using Application.Assets.Exceptions;
using Application.Common.Interfaces;
using Domain.Asset;
using LanguageExt;
using MediatR;

namespace Application.Assets.Commands;

public record UploadAssetImageCommand : IRequest<Either<AssetException, PreviewImage>>
{
    public required Stream FileStream { get; init; }
    public required string OriginalName { get; init; }
    public required Guid AssetId { get; init; }
}

public class UploadAssetImageCommandHandler(
    IAssetRepository assetRepository,
    IPreviewImageRepository previewImageRepository,
    IFileStorage fileStorage) 
    : IRequestHandler<UploadAssetImageCommand, Either<AssetException,PreviewImage>>
{
    public async Task<Either<AssetException, PreviewImage>> Handle(UploadAssetImageCommand request, CancellationToken cancellationToken)
    {
        var assetId = new AssetId(request.AssetId);

        var asset = await assetRepository.GetById(assetId, cancellationToken);

        return await asset.MatchAsync(
            a => UploadImage(a, request, cancellationToken),
            () => Task.FromResult<Either<AssetException, PreviewImage>>(
                new AssetNotFoundException(assetId)));
    }

    private async Task<Either<AssetException, PreviewImage>> UploadImage(
        Asset asset,
        UploadAssetImageCommand request,
        CancellationToken cancellationToken)
    {
        PreviewImage? image = null;

        try
        {
            image = PreviewImage.New(PreviewImageId.New(), Path.GetFileName(request.OriginalName), asset.Id);

            await previewImageRepository.Add(image, cancellationToken);
            await fileStorage.SaveFile(request.FileStream, image.FilePath, cancellationToken);

            return image;
        }
        catch (Exception ex)
        {
            if (image is not null)
            {
                try
                {
                    await previewImageRepository.Delete(image, CancellationToken.None);
                }
                catch
                {
                    // Cleanup failed. The original exception is returned below.
                }
            }
            
            return new AssetUnhandledException(asset.Id, ex);
        }
    }
}