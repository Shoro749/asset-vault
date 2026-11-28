using Domain.Category;

namespace Api.Dtos;

public record CategoryDto(
    Guid? Id,
    string Name,
    DateTime CreatedAt)
{
    public static CategoryDto FromDomainModel(Category category)
        => new(
            Id: category.Id.Value,
            Name: category.Name,
            CreatedAt: category.CreatedAt);
}