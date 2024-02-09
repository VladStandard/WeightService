using DeviceControl.Features.Sections.Shared.Form;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.ProductionSite;

namespace DeviceControl.Features.Sections.References.Warehouses;

public sealed partial class WarehousesUpdateForm : SectionFormBase<WarehouseEntity>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IProductionSiteService ProductionSiteService { get; set; } = null!;

    #endregion

    private IEnumerable<ProductionSiteEntity> PlatformEntities { get; set; } = new List<ProductionSiteEntity>();

    protected override void OnInitialized()
    {
        PlatformEntities = ProductionSiteService.GetAll();
    }
}