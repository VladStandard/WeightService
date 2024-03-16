using DeviceControl.Features.Sections.Shared.Form;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.ProductionSite;
using Ws.Domain.Services.Features.Warehouse;

namespace DeviceControl.Features.Sections.References.Warehouses;

public sealed partial class WarehousesUpdateForm : SectionFormBase<WarehouseEntity>
{
    #region Inject
    [Inject] private RedirectUtils RedirectUtils { get; set; } = null!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IProductionSiteService ProductionSiteService { get; set; } = null!;
    [Inject] private IWarehouseService WarehouseService { get; set; } = null!;

    #endregion

    private IEnumerable<ProductionSiteEntity> PlatformEntities { get; set; } = new List<ProductionSiteEntity>();

    protected override void OnInitialized()
    {
        PlatformEntities = ProductionSiteService.GetAll();
    }
}