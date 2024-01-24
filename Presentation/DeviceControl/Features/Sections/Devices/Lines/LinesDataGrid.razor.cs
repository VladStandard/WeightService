using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.Line;

namespace DeviceControl.Features.Sections.Devices.Lines;

public sealed partial class LinesDataGrid: SectionDataGridBase<LineEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private ILineService LineService { get; set; } = null!;
    
    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<LinesCreateDialog>(new());
    
    protected override async Task OpenDataGridEntityModal(LineEntity item)
        => await OpenSectionModal<LinesUpdateDialog>(item);
    
    protected override async Task OpenItemInNewTab(LineEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionLines}/{item.IdentityValueUid.ToString()}");

    protected override IEnumerable<LineEntity> SetSqlSectionCast() => LineService.GetAll().ToList();

    protected override IEnumerable<LineEntity> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [LineService.GetByUid(itemUid)];
    }
}