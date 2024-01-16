using Blazorise;
using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Entities.SchemaPrint.Labels;
using Ws.StorageCore.Entities.SchemaPrint.ViewLabels;
using Ws.StorageCore.Helpers;

namespace DeviceControl.Features.Sections.Operations.Labels;

public sealed partial class LabelsDataGrid : SectionDataGridBase<SqlViewLabel>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IModalService ModalService { get; set; } = null!;
    
    private SqlViewLabelRepository LabelRepository { get; } = new();

    protected override async Task OpenDataGridEntityModal(SqlViewLabel item)
    {
        SqlLabelEntity labelItem = SqlCoreHelper.Instance.GetItemByUid<SqlLabelEntity>(item.IdentityValueUid);
        await ModalService.Show<LabelsUpdateDialog>(p =>
        {
            p.Add(x => x.DialogSectionEntity, labelItem);
            p.Add(x => x.OnDataChangedAction, new(this, OnModalSubmit));
        });
    }
    
    protected override async Task OpenItemInNewTab(SqlViewLabel item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionLabels}/{item.IdentityValueUid.ToString()}");

    protected override void SetSqlSectionCast() =>
        SectionItems = LabelRepository.GetList(SqlCrudConfigSection);
    
    protected override void SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        SectionItems = new[] { SqlCoreHelper.Instance.GetItemByUid<SqlViewLabel>(itemUid) };
    }
}