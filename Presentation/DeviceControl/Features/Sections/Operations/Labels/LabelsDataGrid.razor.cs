using Blazorise;
using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Print;
using Ws.StorageCore.Entities.Print.ViewLabels;
using Ws.StorageCore.Helpers;

namespace DeviceControl.Features.Sections.Operations.Labels;

public sealed partial class LabelsDataGrid : SectionDataGridBase<ViewLabel>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IModalService ModalService { get; set; } = null!;
    
    private ViewLabelRepository LabelRepository { get; } = new();

    protected override async Task OpenDataGridEntityModal(ViewLabel item)
    {
        LabelEntity labelItem = SqlCoreHelper.Instance.GetItemByUid<LabelEntity>(item.IdentityValueUid);
        await ModalService.Show<LabelsUpdateDialog>(p =>
        {
            p.Add(x => x.DialogSectionEntity, labelItem);
            p.Add(x => x.OnDataChangedAction, new(this, OnModalSubmit));
        });
    }
    
    protected override async Task OpenItemInNewTab(ViewLabel item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionLabels}/{item.IdentityValueUid.ToString()}");

    protected override void SetSqlSectionCast() =>
        SectionItems = LabelRepository.GetList(new());
    
    protected override void SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        SectionItems = new[] { SqlCoreHelper.Instance.GetItemByUid<ViewLabel>(itemUid) };
    }
}