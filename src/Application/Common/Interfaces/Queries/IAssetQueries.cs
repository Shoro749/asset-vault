using Domain.Asset;
using LanguageExt;

namespace Application.Common.Interfaces.Queries;

public interface IAssetQueries
{
    Task<IReadOnlyList<Asset>> GetAll(CancellationToken cancellationToken);
}