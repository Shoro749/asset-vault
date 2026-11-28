using Domain.Asset;

namespace Application.Common.Interfaces;

public interface IPreviewImageRepository
{
    Task<PreviewImage> Add(PreviewImage image, CancellationToken cancellationToken = default);
    Task Delete(PreviewImage image, CancellationToken cancellationToken = default);
}