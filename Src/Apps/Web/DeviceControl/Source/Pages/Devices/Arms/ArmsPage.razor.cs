using DeviceControl.Source.Widgets.Section.Dialogs;
using Ws.Domain.Models.Entities.Devices.Arms;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.Arms;

namespace DeviceControl.Source.Pages.Devices.Arms;

// ReSharper disable ClassNeverInstantiated.Global
public sealed partial class ArmsPage : SectionDataGridPageBase<Arm>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IArmService ArmService { get; set; } = default!;

    #endregion

    [CascadingParameter] private ProductionSite UserProductionSite { get; set; } = default!;

    private ProductionSite ProductionSite { get; set; } = new();

    protected override void OnInitialized()
    {
        ProductionSite = UserProductionSite;
        base.OnInitialized();
    }

    protected override async Task OpenSectionCreateForm()
        => await DialogService.ShowDialogAsync<ArmsCreateDialog>(
            new SectionDialogContentWithProductionSite<Arm> { Item = new(), ProductionSite = ProductionSite },
            DialogParameters);

    protected override async Task OpenDataGridEntityModal(Arm item)
        => await OpenSectionModal<ArmsUpdateDialog>(item);

    protected override async Task OpenItemInNewTab(Arm item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionLines}/{item.Uid.ToString()}");

    protected override IEnumerable<Arm> SetSqlSectionCast() =>
        ProductionSite.IsNew ? [] : ArmService.GetAllByProductionSite(ProductionSite)
            .OrderBy(item => item.Number);

    protected override IEnumerable<Arm> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [ArmService.GetItemByUid(itemUid)];
    }

    protected override Task DeleteItemAction(Arm item)
    {
        ArmService.Delete(item);
        return Task.CompletedTask;
    }

}