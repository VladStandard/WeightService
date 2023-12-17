using DeviceControl.Features.Shared.Form;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Utils;

namespace DeviceControl.Features.Sections.References.Workshops;

public sealed partial class WorkshopsCreateForm: SectionFormBase<SqlWorkShopEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;

    private IEnumerable<SqlProductionSiteEntity> PlatformEntities { get; set; } = new List<SqlProductionSiteEntity>();

    protected override void OnInitialized()
    {
        SectionEntity.ProductionSite.Name = Localizer["SectionFormPlatformDefaultName"];
        PlatformEntities = new SqlProductionSiteRepository().GetEnumerable(SqlCrudConfigFactory.GetCrudActual());
        PlatformEntities = PlatformEntities.Append(SectionEntity.ProductionSite);
    }
    
    private SqlProductionSiteEntity GetPlatformByUid(string platformUid) =>
        PlatformEntities.First(x => x.IdentityValueUid == Guid.Parse(platformUid));
}