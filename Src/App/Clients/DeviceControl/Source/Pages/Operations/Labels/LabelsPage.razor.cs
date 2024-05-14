using Microsoft.FluentUI.AspNetCore.Components;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Features.Label;

namespace DeviceControl.Source.Pages.Operations.Labels;

// ReSharper disable ClassNeverInstantiated.Global
public sealed partial class LabelsPage : SectionDataGridPageBase<Label>
{
    # region Injects

    [Inject] private ILabelService LabelService { get; set; } = default!;
    [Inject] private IDialogService DialogService { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;

    # endregion

    protected override async Task OpenDataGridEntityModal(Label item)
    {
        Label labelItem = LabelService.GetItemByUid(item.Uid);
        await DialogService.ShowDialogAsync<LabelsUpdateDialog>(new SectionDialogContent<Label> { Item = labelItem }, DialogParameters);
    }

    protected override async Task OpenItemInNewTab(Label item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionLabels}/{item.Uid.ToString()}");

    protected override IEnumerable<Label> SetSqlSectionCast() => LabelService.GetAll();

    protected override IEnumerable<Label> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return new[] { LabelService.GetItemByUid(itemUid) };
    }
}