using DeviceControl.Source.Widgets.Section;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.ProductionSite;
using Ws.Shared.Resources;

namespace DeviceControl.Source.Pages.References.ProductionSites;

public sealed partial class ProductionSitesUpdateForm : SectionFormBase<ProductionSiteEntity>
{
    # region Injects

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IProductionSiteService ProductionSiteService { get; set; } = default!;

    # endregion

    protected override ProductionSiteEntity UpdateItemAction(ProductionSiteEntity item) =>
        ProductionSiteService.Update(item);

    protected override Task DeleteItemAction(ProductionSiteEntity item)
    {
        ProductionSiteService.Delete(item);
        return Task.CompletedTask;
    }
}

public class ProductionSiteUpdateFormValidator : AbstractValidator<ProductionSiteEntity>
{
    public ProductionSiteUpdateFormValidator()
    {
        RuleFor(item => item.Name).NotEmpty();
        RuleFor(item => item.Address).NotEmpty();
    }
}