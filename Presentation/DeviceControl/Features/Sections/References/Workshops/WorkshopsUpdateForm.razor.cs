using DeviceControl.Features.Shared.Form;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Utils;

namespace DeviceControl.Features.Sections.References.Workshops;

public sealed partial class WorkshopsUpdateForm: SectionFormBase<SqlWorkShopEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;

    private IEnumerable<SqlProductionSiteEntity> PlatformEntities { get; set; } = new List<SqlProductionSiteEntity>();

    protected override void OnInitialized()
    {
        PlatformEntities = new SqlProductionSiteRepository().GetEnumerable(SqlCrudConfigFactory.GetCrudActual()).ToList();
    }
    
    private SqlProductionSiteEntity GetPlatformByUid(string platformUid) =>
        PlatformEntities.First(x => x.IdentityValueUid == Guid.Parse(platformUid));
}