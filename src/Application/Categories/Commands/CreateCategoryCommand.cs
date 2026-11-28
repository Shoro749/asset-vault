using Application.Categories.Exceptions;
using Application.Common.Interfaces;
using Domain.Category;
using LanguageExt;
using MediatR;

namespace Application.Categories.Commands;

public record CreateCategoryCommand : IRequest<Either<CategoryException, Category>>
{
    public required string Name { get; init; }
}

public class CreateCategoryCommandHandler(ICategoryRepository repository)
    : IRequestHandler<CreateCategoryCommand, Either<CategoryException, Category>>
{
    public async Task<Either<CategoryException, Category>> Handle(
        CreateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var name = request.Name.Trim();

        var existing = await repository.GetByName(name, cancellationToken);

        return await existing.MatchAsync(
            category => new CategoryAlreadyExistsException(category.Id),
            () => CreateCategory(name, cancellationToken));
    }

    private async Task<Either<CategoryException, Category>> CreateCategory(
        string name,
        CancellationToken cancellationToken)
    {
        try
        {
            var category = Category.New(CategoryId.New(), name);

            return await repository.Add(category, cancellationToken);
        }
        catch (Exception ex)
        {
            return new CategoryUnhandledException(CategoryId.Empty(), ex);
        }
    }
}