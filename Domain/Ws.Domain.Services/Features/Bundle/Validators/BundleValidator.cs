using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Domain.Services.Features.Bundle.Validators;

internal abstract class BundleValidator : AbstractValidator<BundleEntity>
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