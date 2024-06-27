using DeviceControl.Source.Widgets.Section.Dialogs;
using Ws.Domain.Models.Entities.Devices;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.Printers;

namespace DeviceControl.Source.Pages.Devices.Printers;

public sealed partial class PrintersPage : SectionDataGridPageBase<Printer>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IPrinterService PrinterService { get; set; } = default!;

    #endregion

    [CascadingParameter] private ProductionSite UserProductionSite { get; set; } = default!;

    private ProductionSite ProductionSite { get; set; } = new();

    protected override void OnInitialized()
    {
        ProductionSite = UserProductionSite;
        base.OnInitialized();
    }

    protected override async Task OpenSectionCreateForm()
        => await DialogService.ShowDialogAsync<PrintersCreateDialog>(
            new SectionDialogContentWithProductionSite<Printer>
                { Item = new(), ProductionSite = ProductionSite }, DialogParameters);

    protected override async Task OpenDataGridEntityModal(Printer item)
        => await OpenSectionModal<PrintersUpdateDialog>(item);

    protected override async Task OpenItemInNewTab(Printer item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionPrinters}/{item.Uid.ToString()}");

    protected override IEnumerable<Printer> SetSqlSectionCast() =>
        ProductionSite.IsNew ? [] : PrinterService.GetAllByProductionSite(ProductionSite);

    protected override IEnumerable<Printer> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [PrinterService.GetItemByUid(itemUid)];
    }

    protected override Task DeleteItemAction(Printer item)
    {
        PrinterService.DeleteById(item.Uid);
        return Task.CompletedTask;
    }
}