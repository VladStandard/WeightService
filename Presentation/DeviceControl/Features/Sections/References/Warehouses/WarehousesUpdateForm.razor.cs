using DeviceControl.Features.Sections.Shared.Form;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Entities.SchemaRef.ProductionSites;
using Ws.StorageCore.Entities.SchemaRef.Warehouses;

namespace DeviceControl.Features.Sections.References.Warehouses;

public sealed partial class WarehousesUpdateForm: SectionFormBase<SqlWarehouseEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;

    private IEnumerable<SqlProductionSiteEntity> PlatformEntities { get; set; } = new List<SqlProductionSiteEntity>();

    protected override void OnInitialized()
    {
        PlatformEntities = new SqlProductionSiteRepository().GetEnumerable().ToList();
    }
}