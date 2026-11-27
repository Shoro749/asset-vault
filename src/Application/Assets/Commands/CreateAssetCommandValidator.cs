using FluentValidation;

namespace Application.Assets.Commands;

public class CreateAssetCommandValidator : AbstractValidator<CreateAssetCommand>
{
    public CreateAssetCommandValidator()
    {
        RuleFor(a => a.Title).NotEmpty().MinimumLength(3).MaximumLength(255);
        RuleFor(a => a.Description).NotEmpty().MinimumLength(3).MaximumLength(255);
        RuleFor(a => a.Attributes).NotNull();
    }
}