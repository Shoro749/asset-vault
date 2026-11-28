using Domain.Category;
using LanguageExt;

namespace Application.Common.Interfaces.Queries;

public interface ICategoryQueries
{
    Task<Option<Category>> GetById(CategoryId id, CancellationToken cancellationToken);
}