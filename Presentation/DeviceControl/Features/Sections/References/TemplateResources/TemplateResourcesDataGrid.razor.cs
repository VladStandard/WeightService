using Blazorise.DataGrid;
using DeviceControl.Features.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Helpers;

namespace DeviceControl.Features.Sections.References.TemplateResources;

public sealed partial class TemplateResourcesDataGrid: SectionDataGridBase<SqlTemplateResourceEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    private SqlTemplateResourceRepository TemplateResourceRepository { get; } = new();
    
    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<TemplateResourcesCreateDialog>(new());
    
    protected override async Task OpenDataGridEntityModal(SqlTemplateResourceEntity item)
        => await OpenSectionModal<TemplateResourcesUpdateDialog>(item);
    
    protected override async Task OpenItemInNewTab(SqlTemplateResourceEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionTemplateResources}/{item.IdentityValueUid.ToString()}");

    protected override void SetSqlSectionCast() =>
        SectionItems = TemplateResourceRepository.GetList(SqlCrudConfigSection);
    
    protected override void SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        SectionItems = new() { SqlCoreHelper.Instance.GetItemByUid<SqlTemplateResourceEntity>(itemUid) };
    }
    
    private static string ConvertBytes(int fileSize) =>
        fileSize > 1024 ? $"{fileSize / 1024:### ##0} Kb" : $"{fileSize:##0} bytes";
}
