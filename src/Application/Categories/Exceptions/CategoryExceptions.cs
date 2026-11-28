using Domain.Category;

namespace Application.Categories.Exceptions;

public abstract class CategoryException(CategoryId categoryId, string message, Exception? innerException = null)
    : Exception(message, innerException)
{
    public CategoryId CategoryId { get; } = categoryId;
}

public class CategoryAlreadyExistsException(CategoryId categoryId) : CategoryException(categoryId, $"Category already exists under id {categoryId}.");

public class CategoryNotFoundException(CategoryId categoryId) : CategoryException(categoryId, $"Category with ID {categoryId} does not exist.");

public class CategoryUnhandledException(CategoryId categoryId, Exception? innerException = null) : CategoryException(categoryId, $"An unexpected error occurred while processing category {categoryId}.", innerException);