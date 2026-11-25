using Domain.Asset;
using LanguageExt;

namespace Application.Common.Interfaces.Queries;

public interface IAssetQueries
{
    Task<IReadOnlyList<Asset>> GetAll(CancellationToken cancellationToken);
    Task<Option<Asset>> GetById(AssetId id, CancellationToken cancellationToken);
    Task<Option<Asset>> GetByTitle(string title, CancellationToken cancellationToken);
    Task<Asset> Add(Asset asset, CancellationToken cancellationToken);
    Task<Asset> Update(Asset asset, CancellationToken cancellationToken);
    Task Delete(Asset asset, CancellationToken cancellationToken);
}