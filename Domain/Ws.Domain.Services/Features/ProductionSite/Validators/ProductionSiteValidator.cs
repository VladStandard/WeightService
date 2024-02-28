using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Services.Features.ProductionSite.Validators;

internal abstract class ProductionSiteValidator : AbstractValidator<ProductionSiteEntity>
{
    protected ProductionSiteValidator()
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .MaximumLength(150);
        RuleFor(item => item.Address)
            .NotEmpty()
            .MaximumLength(512);
    }
}
