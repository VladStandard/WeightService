namespace Ws.Domain.Services.Features.ProductionSite.Validators;

internal sealed class ProductionSiteNewValidator : ProductionSiteValidator
{
    public ProductionSiteNewValidator() : base()
    {
        RuleFor(item => item.IsNew).Equal(true);
    }
}