using DeviceControl.Features.Sections.Shared.Form;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.ProductionSite;
using Ws.Domain.Services.Features.Warehouse;

namespace DeviceControl.Features.Sections.References.Warehouses;

public sealed partial class WarehousesCreateForm : SectionFormBase<WarehouseEntity>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IProductionSiteService ProductionSiteService { get; set; } = null!;
    [Inject] private IWarehouseService WarehouseService { get; set; } = null!;

    #endregion

    private IEnumerable<ProductionSiteEntity> PlatformEntities { get; set; } = new List<ProductionSiteEntity>();

    protected override void OnInitialized()
    {
        SectionEntity.ProductionSite.Name = Localizer["SectionFormPlatformDefaultName"];
        PlatformEntities = ProductionSiteService.GetAll();
        PlatformEntities = PlatformEntities.Append(SectionEntity.ProductionSite);
    }
}