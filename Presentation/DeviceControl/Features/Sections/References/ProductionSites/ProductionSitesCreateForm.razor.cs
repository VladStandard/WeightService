using DeviceControl.Features.Sections.Shared.Form;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.ProductionSite;

namespace DeviceControl.Features.Sections.References.ProductionSites;

public sealed partial class ProductionSitesCreateForm : SectionFormBase<ProductionSiteEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IProductionSiteService ProductionSiteService { get; set; } = null!;
}