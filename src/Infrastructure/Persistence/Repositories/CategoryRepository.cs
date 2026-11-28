using Application.Common.Interfaces;
using Application.Common.Interfaces.Queries;
using Domain.Category;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class CategoryRepository(ApplicationDbContext context) : ICategoryRepository, ICategoryQueries
{
    public async Task<Option<Category>> GetById(CategoryId id, CancellationToken cancellationToken)
    {
        var category = await context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        return category ?? Option<Category>.None;
    }

    public async Task<Option<Category>> GetByName(string name, CancellationToken cancellationToken)
    {
        var category = await context.Categories.FirstOrDefaultAsync(c => c.Name == name, cancellationToken);

        return category ?? Option<Category>.None;
    }

    public async Task<Category> Add(Category category, CancellationToken cancellationToken)
    {
        await context.Categories.AddAsync(category, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return category;
    }
}