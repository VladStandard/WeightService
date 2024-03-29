using DeviceControl2.Source.Shared.Localization;
using DeviceControl2.Source.Widgets.Section;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.ProductionSite;
using Ws.Domain.Services.Features.Warehouse;
using Ws.Shared.Resources;

namespace DeviceControl2.Source.Pages.References.ProductionSites;

public sealed partial class ProductionSitesCreateForm: SectionFormBase<ProductionSiteEntity>
{
    # region Injects

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IProductionSiteService ProductionSiteService { get; set; } = default!;

    # endregion

    protected override ProductionSiteEntity CreateItemAction(ProductionSiteEntity item) =>
        ProductionSiteService.Create(item);
}

public class ProductionSitesCreateFormValidator : AbstractValidator<ProductionSiteEntity>
{
    public ProductionSitesCreateFormValidator()
    {
        RuleFor(item => item.Name).NotEmpty();
        RuleFor(item => item.Address).NotEmpty();
    }
}
