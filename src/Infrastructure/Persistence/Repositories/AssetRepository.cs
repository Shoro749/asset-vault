using Application.Common.Interfaces;
using Application.Common.Interfaces.Queries;
using Domain.Asset;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class AssetRepository(ApplicationDbContext context) : IAssetRepository, IAssetQueries
{
    public async Task<IReadOnlyList<Asset>> GetAll(CancellationToken cancellationToken)
        => await context.Assets.AsNoTracking().ToListAsync(cancellationToken);

    public async Task<Option<Asset>> GetById(AssetId id, CancellationToken cancellationToken)
    {
        var entity = await context.Assets.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        
        return entity ?? Option<Asset>.None;
    }

    public async Task<Option<Asset>> GetByTitle(string title, CancellationToken cancellationToken)
    {
        var entity = await context.Assets.FirstOrDefaultAsync(a => a.Title == title, cancellationToken);

        return entity ?? Option<Asset>.None;
    }

    public async Task<Asset> Add(Asset asset, CancellationToken cancellationToken)
    {
        await context.Assets.AddAsync(asset, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return asset;
    }

    public async Task<Asset> Update(Asset asset, CancellationToken cancellationToken)
    {
        context.Assets.Update(asset);
        await context.SaveChangesAsync(cancellationToken);

        return asset;
    }

    public async Task Delete(Asset asset, CancellationToken cancellationToken)
    {
        context.Assets.Remove(asset);
        await context.SaveChangesAsync(cancellationToken);
    }
}