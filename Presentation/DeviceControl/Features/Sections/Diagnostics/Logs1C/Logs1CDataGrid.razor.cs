using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Diag;
using Ws.Services.Features.LogWeb;

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
    
    protected override void SetSqlSectionCast() =>
        SectionItems = LogWebService.GetAll();
    
    protected override void SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        SectionItems = [LogWebService.GetByUid(itemUid)];
    }
}