using FluentValidation;

namespace Application.Assets.Commands;

public class UpdateAssetCommandValidator : AbstractValidator<UpdateAssetCommand>
{
    public UpdateAssetCommandValidator()
    {
        RuleFor(a => a.AssetId).NotEmpty();
        RuleFor(a => a.Title).NotEmpty().MinimumLength(3).MaximumLength(255);
        RuleFor(a => a.Description).NotEmpty().MinimumLength(3).MaximumLength(255);
    }
}