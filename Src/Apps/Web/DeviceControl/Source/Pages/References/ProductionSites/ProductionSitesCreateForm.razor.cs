using Ws.DeviceControl.Models.Dto.References.ProductionSites.Queries;

namespace DeviceControl.Source.Pages.References.ProductionSites;

public sealed partial class ProductionSitesCreateForm : SectionFormBase<ProductionSiteDto>
{
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;

    protected override ProductionSiteDto CreateItemAction(ProductionSiteDto item) =>
        throw new NotImplementedException();
}

public class ProductionSitesCreateFormValidator : AbstractValidator<ProductionSiteDto>
{
    public ProductionSitesCreateFormValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.Name).NotEmpty().WithName(wsDataLocalizer["ColName"]);
        RuleFor(item => item.Address).NotEmpty().WithName(wsDataLocalizer["ColAddress"]);
    }
}