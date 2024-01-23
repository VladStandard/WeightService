using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Features.Plu;

namespace DeviceControl.Features.Sections.References1C.Plus;

public sealed partial class PlusDataGrid : SectionDataGridBase<PluEntity>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IPluService PluService { get; set; } = null!;

    #endregion

    protected override async Task OpenDataGridEntityModal(PluEntity item)
        => await OpenSectionModal<PlusUpdateDialog>(item);
    
    protected override async Task OpenItemInNewTab(PluEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionPlus}/{item.IdentityValueUid.ToString()}");

    protected override void SetSqlSectionCast() =>
        SectionItems = PluService.GetAllNotGroup();
    
    protected override void SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        SectionItems = [PluService.GetByUid(itemUid)];
    }
}