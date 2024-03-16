using DeviceControl2.Source.Shared.Utils;
using DeviceControl2.Source.Widgets.Section;
using Microsoft.AspNetCore.Components;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Features.Plu;

namespace DeviceControl2.Source.Pages.References1C.Plus;

// ReSharper disable ClassNeverInstantiated.Global
public sealed partial class PlusPage: SectionDataGridPageBase<PluEntity>
{
    [Inject] private IPluService PluService { get; set; } = default!;
    
    protected override async Task OpenDataGridEntityModal(PluEntity item)
        => await OpenSectionModal<PlusUpdateDialog>(item);
    
    protected override async Task OpenItemInNewTab(PluEntity item) =>
        await OpenLinkInNewTab($"{RouteUtils.SectionPlus}/{item.Uid.ToString()}");
  
    protected override IEnumerable<PluEntity> SetSqlSectionCast() => PluService.GetAll();

    protected override IEnumerable<PluEntity> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [PluService.GetItemByUid(itemUid)];
    }
}