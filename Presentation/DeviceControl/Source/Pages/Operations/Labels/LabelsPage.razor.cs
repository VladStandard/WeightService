using DeviceControl.Source.Shared.Localization;
using DeviceControl.Source.Shared.Utils;
using DeviceControl.Source.Widgets.Section;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.FluentUI.AspNetCore.Components;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Features.Label;
using Ws.Shared.Resources;

namespace DeviceControl.Source.Pages.Operations.Labels;

// ReSharper disable ClassNeverInstantiated.Global
public sealed partial class LabelsPage : SectionDataGridPageBase<LabelEntity>
{
    # region Injects

    [Inject] private ILabelService LabelService { get; set; } = default!;
    [Inject] private IDialogService DialogService { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;

    # endregion

    protected override async Task OpenDataGridEntityModal(LabelEntity item)
    {
        LabelEntity labelItem = LabelService.GetItemByUid(item.Uid);
        await DialogService.ShowDialogAsync<LabelsUpdateDialog>(new SectionDialogContent<LabelEntity> { Item = labelItem }, DialogParameters);
    }

    protected override async Task OpenItemInNewTab(LabelEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionLabels}/{item.Uid.ToString()}");

    protected override IEnumerable<LabelEntity> SetSqlSectionCast() => LabelService.GetAll();

    protected override IEnumerable<LabelEntity> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return new[] { LabelService.GetItemByUid(itemUid) };
    }
}