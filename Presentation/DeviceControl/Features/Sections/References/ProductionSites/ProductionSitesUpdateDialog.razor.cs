using DeviceControl.Features.Sections.Shared.Modal;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Shared.Enums;
using Ws.StorageCore.Entities.SchemaRef.ProductionSites;

namespace DeviceControl.Features.Sections.References.ProductionSites;

public sealed partial class ProductionSitesUpdateDialog: SectionDialogBase<SqlProductionSiteEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    protected override List<EnumTypeModel<string>> InitializeTabList() =>
        new() { new(Localizer["SectionProductionSites"], "main") };
}