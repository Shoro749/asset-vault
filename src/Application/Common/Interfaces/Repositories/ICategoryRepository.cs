using Domain.Category;
using LanguageExt;

namespace Application.Common.Interfaces;

public interface ICategoryRepository
{
    Task<Option<Category>> GetById(CategoryId id, CancellationToken cancellationToken);

    Task<Option<Category>> GetByName(string name, CancellationToken cancellationToken);

    Task<Category> Add(Category category, CancellationToken cancellationToken);
}