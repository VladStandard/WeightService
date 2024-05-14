namespace Ws.Domain.Services.Features.Brand.Validators;

internal abstract class BrandValidator : AbstractValidator<Models.Entities.Ref1c.Brand>
{
    protected BrandValidator()
    {
        RuleFor(item => item.Name)
            .NotNull()
            .MaximumLength(128);
    }
}