using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Domain.Services.Features.Bundles.Validators;

internal abstract class BundleValidator : AbstractValidator<Bundle>
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