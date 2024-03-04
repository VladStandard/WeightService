namespace Ws.Domain.Services.Features.ProductionSite.Validators;

internal sealed class ProductionSiteNewValidator : ProductionSiteValidator
{
    public ProductionSiteNewValidator()
    {
        RuleFor(item => item.IsNew).Equal(true);
    }
}