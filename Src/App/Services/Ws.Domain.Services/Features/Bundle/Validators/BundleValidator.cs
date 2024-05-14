namespace Ws.Domain.Services.Features.Bundle.Validators;

internal abstract class BundleValidator : AbstractValidator<Models.Entities.Ref1c.Bundle>
{
    protected BundleValidator()
    {
        RuleFor(item => item.Name)
            .NotNull();
        RuleFor(item => item.Weight)
            .NotNull()
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(100);
    }
}