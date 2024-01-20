using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.StorageCore.Entities.Ref1c.Plus;
using Ws.StorageCore.Helpers;

namespace DeviceControl.Features.Sections.References1C.Plus;

public sealed partial class PlusDataGrid : SectionDataGridBase<PluEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    private SqlPluRepository PluRepository { get; } = new();

    protected override async Task OpenDataGridEntityModal(PluEntity item)
        => await OpenSectionModal<PlusUpdateDialog>(item);
    
    protected override async Task OpenItemInNewTab(PluEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionPlus}/{item.IdentityValueUid.ToString()}");

    protected override void SetSqlSectionCast() =>
        SectionItems = PluRepository.GetEnumerableNotGroup().ToList();
    
    protected override void SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        SectionItems = [SqlCoreHelper.Instance.GetItemByUid<PluEntity>(itemUid)];
    }
}