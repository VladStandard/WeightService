using Blazorise;
using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Print;
using Ws.Services.Features.Label;
using Ws.Services.Features.Line;

namespace DeviceControl.Features.Sections.Operations.Labels;

public sealed partial class LabelsDataGrid : SectionDataGridBase<ViewLabel>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IModalService ModalService { get; set; } = null!;
    [Inject] private ILabelService LabelService { get; set; } = null!;

    #endregion

    protected override async Task OpenDataGridEntityModal(ViewLabel item)
    {
        LabelEntity labelItem = LabelService.GetByUid(item.IdentityValueUid);
        await ModalService.Show<LabelsUpdateDialog>(p =>
        {
            p.Add(x => x.DialogSectionEntity, labelItem);
            p.Add(x => x.OnDataChangedAction, new(this, OnModalSubmit));
        });
    }
    
    protected override async Task OpenItemInNewTab(ViewLabel item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionLabels}/{item.IdentityValueUid.ToString()}");

    protected override void SetSqlSectionCast() =>
        SectionItems = LabelService.GetAll();
    
    protected override void SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        SectionItems = new[] { LabelService.GetViewByUid(itemUid) };
    }
}