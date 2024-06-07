using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Domain.Services.Features.Brands.Validators;

internal abstract class BrandValidator : AbstractValidator<Brand>
{
    protected BrandValidator()
    {
        RuleFor(item => item.Name)
            .NotNull()
            .MaximumLength(128);
    }
}