using Blazorise;
using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Features.Label;

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

    protected override IEnumerable<ViewLabel> SetSqlSectionCast() => LabelService.GetAll();
    
    protected override IEnumerable<ViewLabel> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return new[] { LabelService.GetViewByUid(itemUid) };
    }
}
