using Ws.DeviceControl.Models.Dto.References.ProductionSites.Queries;

namespace DeviceControl.Source.Pages.References.ProductionSites;

public sealed partial class ProductionSitesUpdateForm : SectionFormBase<ProductionSiteDto>
{
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;

    protected override ProductionSiteDto UpdateItemAction(ProductionSiteDto item) =>
        throw new NotImplementedException();

    protected override Task DeleteItemAction(ProductionSiteDto item) =>
        throw new NotImplementedException();
}

public class ProductionSiteUpdateFormValidator : AbstractValidator<ProductionSiteDto>
{
    public ProductionSiteUpdateFormValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.Name).NotEmpty().WithName(wsDataLocalizer["ColName"]);
        RuleFor(item => item.Address).NotEmpty().WithName(wsDataLocalizer["ColAddress"]);
    }
}