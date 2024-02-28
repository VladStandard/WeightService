namespace Ws.Domain.Services.Features.ProductionSite.Validators;

internal sealed class ProductionSiteUpdateValidator : ProductionSiteValidator
{
    public ProductionSiteUpdateValidator() : base()
    {
        RuleFor(item => item.IsExists).Equal(true);
    }
}