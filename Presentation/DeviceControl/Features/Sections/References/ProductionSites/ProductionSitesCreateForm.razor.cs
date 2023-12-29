using DeviceControl.Features.Shared.Form;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Entities.SchemaRef.ProductionSites;

namespace DeviceControl.Features.Sections.References.ProductionSites;

public sealed partial class ProductionSitesCreateForm: SectionFormBase<SqlProductionSiteEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
}