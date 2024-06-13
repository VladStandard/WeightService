using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Services.Features.ProductionSites.Validators;

internal abstract class ProductionSiteValidator : AbstractValidator<ProductionSite>
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