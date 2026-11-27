using Application.Assets.Exceptions;
using Application.Common.Interfaces;
using Domain.Asset;
using LanguageExt;
using MediatR;
using Unit = LanguageExt.Unit;

namespace Application.Assets.Commands;

public record UpdateAssetCommand : IRequest<Either<AssetException, Asset>>
{
    public required Guid AssetId { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
}

public class UpdateAssetCommandHandler(IAssetRepository assetRepository)
    : IRequestHandler<UpdateAssetCommand, Either<AssetException, Asset>>
{
    public async Task<Either<AssetException, Asset>> Handle(
        UpdateAssetCommand request, 
        CancellationToken cancellationToken)
    {
        var assetId = new AssetId(request.AssetId);
        
        var asset = await assetRepository.GetById(assetId, cancellationToken);
        return await asset.MatchAsync(
            a => CheckDuplicates(a.Id, request.Title, cancellationToken)
                .BindAsync(_ => UpdateEntity(a, request, cancellationToken)),
            () => new AssetNotFoundException(assetId));
    }

    private async Task<Either<AssetException, Asset>> UpdateEntity(
        Asset asset, 
        UpdateAssetCommand request, 
        CancellationToken cancellationToken)
    {
        try
        {
            asset.Update(request.Title, request.Description);

            return await assetRepository.Update(asset, cancellationToken);
        }
        catch (Exception ex)
        {
            return new AssetUnhandledException(asset.Id, ex);
        }
    }

    private async Task<Either<AssetException, Unit>> CheckDuplicates(
        AssetId aId, 
        string title, 
        CancellationToken cancellationToken)
    {
        var asset = await assetRepository.GetByTitle(title, cancellationToken);

        return asset.Match<Either<AssetException, Unit>>(
            a => a.Id == aId ? Unit.Default : new AssetAlreadyExistsException(a.Id),
            () => Unit.Default);
    }
}