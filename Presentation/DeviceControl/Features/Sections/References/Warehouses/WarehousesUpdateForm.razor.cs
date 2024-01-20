using DeviceControl.Features.Sections.Shared.Form;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.StorageCore.Entities.Ref.ProductionSites;

namespace DeviceControl.Features.Sections.References.Warehouses;

public sealed partial class WarehousesUpdateForm: SectionFormBase<WarehouseEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;

    private IEnumerable<ProductionSiteEntity> PlatformEntities { get; set; } = new List<ProductionSiteEntity>();

    protected override void OnInitialized()
    {
        PlatformEntities = new SqlProductionSiteRepository().GetEnumerable().ToList();
    }
}