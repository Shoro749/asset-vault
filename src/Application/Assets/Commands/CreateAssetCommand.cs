using Application.Assets.Exceptions;
using Application.Common.Interfaces;
using Domain.Asset;
using LanguageExt;
using MediatR;

namespace Application.Assets.Commands;

public record CreateAssetCommand : IRequest<Either<AssetException, Asset>>
{
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required AssetAttributes Attributes { get; init; }
}

public class CreateAssetCommandHandler(IAssetRepository assetRepository)
    : IRequestHandler<CreateAssetCommand, Either<AssetException, Asset>>
{
    public async Task<Either<AssetException, Asset>> Handle(
        CreateAssetCommand request, 
        CancellationToken cancellationToken)
    {
        var existingAsset = await assetRepository.GetByTitle(request.Title.Trim(), cancellationToken);
        return await existingAsset.MatchAsync(
            a => new AssetAlreadyExistsException(a.Id),
            () => CreateEntity(request, cancellationToken));
    }

    private async Task<Either<AssetException, Asset>> CreateEntity(
        CreateAssetCommand request, 
        CancellationToken cancellationToken)
    {
        try
        {
            var asset = Asset.New(AssetId.New(), request.Title, request.Description, request.Attributes);
            return await assetRepository.Add(asset, cancellationToken);
        }
        catch (Exception ex)
        {
            return new AssetUnhandledException(AssetId.Empty(), ex);
        }
    }
}