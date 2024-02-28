using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Domain.Services.Features.Brand.Validators;

internal abstract class BrandValidator : AbstractValidator<BrandEntity>
{
    protected BrandValidator()
    {
        RuleFor(item => item.Name)
            .NotNull()
            .MaximumLength(128);
    }
}