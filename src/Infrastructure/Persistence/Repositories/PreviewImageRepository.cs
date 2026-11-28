using Application.Common.Interfaces;
using Domain.Asset;

namespace Infrastructure.Persistence.Repositories;

public class PreviewImageRepository(ApplicationDbContext context)
    : IPreviewImageRepository
{
    public async Task<PreviewImage> Add(PreviewImage image, CancellationToken cancellationToken)
    {
        await context.PreviewImages.AddAsync(image, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return image;
    }

    public async Task Delete(PreviewImage image, CancellationToken cancellationToken)
    {
        context.PreviewImages.Remove(image);
        await context.SaveChangesAsync(cancellationToken); 
    }
}