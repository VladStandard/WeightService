using DeviceControl.Features.Sections.Shared.Form;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;

namespace DeviceControl.Features.Sections.References.ProductionSites;

public sealed partial class ProductionSitesUpdateForm: SectionFormBase<ProductionSiteEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
}