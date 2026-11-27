using Application.Assets.Exceptions;
using Application.Common.Interfaces;
using Domain.Asset;
using LanguageExt;
using Unit = LanguageExt.Unit;
using MediatR;

namespace Application.Assets.Commands;

public record DeleteAssetCommand : IRequest<Either<AssetException, Unit>>
{
    public required Guid AssetId { get; init; }
}

public class DeleteAssetCommandHandler(IAssetRepository repository)
    : IRequestHandler<DeleteAssetCommand, Either<AssetException, Unit>>
{
    public async Task<Either<AssetException, Unit>> Handle(
        DeleteAssetCommand request, 
        CancellationToken cancellationToken)
    {
        var assetId = new AssetId(request.AssetId);

        try
        {
            var asset = await repository.GetById(assetId, cancellationToken);

            return await asset.MatchAsync(
                a => DeleteEntity(a, cancellationToken),
                () => new AssetNotFoundException(assetId));
        }
        catch (Exception ex)
        {
            return new AssetUnhandledException(assetId, ex);
        }
    }

    private async Task<Either<AssetException, Unit>> DeleteEntity(
        Asset asset, 
        CancellationToken cancellationToken)
    {
        try
        {
            await repository.Delete(asset, cancellationToken);
            return Unit.Default;
        }
        catch (Exception ex)
        {
            return new AssetUnhandledException(asset.Id, ex);
        }
    }
}