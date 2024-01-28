using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Diag;
using Ws.Domain.Services.Features.LogWeb;

namespace DeviceControl.Features.Sections.Diagnostics.Logs1C;

public sealed partial class Logs1CDataGrid: SectionDataGridBase<LogWebEntity>
{
    #region Inject
    
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private ILogWebService LogWebService { get; set; } = null!;
    
    #endregion
    
    protected override async Task OpenDataGridEntityModal(LogWebEntity item)
        => await OpenSectionModal<Logs1CUpdateDialog>(item);
    
    protected override async Task OpenItemInNewTab(LogWebEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.Section1CLogs}/{item.IdentityValueUid.ToString()}");
    
    protected override IEnumerable<LogWebEntity> SetSqlSectionCast() => LogWebService.GetAll();
    
    protected override IEnumerable<LogWebEntity> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [LogWebService.GetItemByUid(itemUid)];
    }
}