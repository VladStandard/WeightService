namespace Ws.Domain.Services.Features.ProductionSites.Validators;

internal sealed class ProductionSiteUpdateValidator : ProductionSiteValidator
{
    public ProductionSiteUpdateValidator()
    {
        RuleFor(item => item.IsExists).Equal(true);
    }
}