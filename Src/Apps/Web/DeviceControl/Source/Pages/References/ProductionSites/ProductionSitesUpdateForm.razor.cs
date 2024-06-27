using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.ProductionSites;

namespace DeviceControl.Source.Pages.References.ProductionSites;

public sealed partial class ProductionSitesUpdateForm : SectionFormBase<ProductionSite>
{
    # region Injects

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IProductionSiteService ProductionSiteService { get; set; } = default!;

    # endregion

    protected override ProductionSite UpdateItemAction(ProductionSite item) =>
        ProductionSiteService.Update(item);

    protected override Task DeleteItemAction(ProductionSite item)
    {
        ProductionSiteService.DeleteById(item.Uid);
        return Task.CompletedTask;
    }
}

public class ProductionSiteUpdateFormValidator : AbstractValidator<ProductionSite>
{
    public ProductionSiteUpdateFormValidator()
    {
        RuleFor(item => item.Name).NotEmpty();
        RuleFor(item => item.Address).NotEmpty();
    }
}