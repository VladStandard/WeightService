using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Entities.SchemaDiag.LogsWebs;
using Ws.StorageCore.Helpers;

namespace DeviceControl.Features.Sections.Diagnostics.Logs1C;

public sealed partial class Logs1CDataGrid: SectionDataGridBase<SqlLogWebEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    private SqlLogWebRepository SqlLogWebRepository { get; } = new();
    
    protected override async Task OpenDataGridEntityModal(SqlLogWebEntity item)
        => await OpenSectionModal<Logs1CUpdateDialog>(item);
    
    protected override async Task OpenItemInNewTab(SqlLogWebEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.Section1CLogs}/{item.IdentityValueUid.ToString()}");
    
    protected override void SetSqlSectionCast() =>
        SectionItems = SqlLogWebRepository.GetList(new()).ToList();
    
    protected override void SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        SectionItems = [SqlCoreHelper.Instance.GetItemByUid<SqlLogWebEntity>(itemUid)];
    }
}