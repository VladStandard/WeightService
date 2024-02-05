using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Features.Box;

namespace DeviceControl.Features.Sections.References1C.Boxes;


public sealed partial class BoxesDataGrid: SectionDataGridBase<BoxEntity>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IBoxService BoxService { get; set; } = null!;
    
    #endregion

    protected override async Task OpenDataGridEntityModal(BoxEntity item)
        => await OpenSectionModal<BoxesUpdateDialog>(item);
    
    protected override async Task OpenItemInNewTab(BoxEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionBoxes}/{item.IdentityValueUid.ToString()}");

    protected override IEnumerable<BoxEntity> SetSqlSectionCast() => BoxService.GetAll();
    
    protected override IEnumerable<BoxEntity> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [BoxService.GetItemByUid(itemUid)];
    }
}
