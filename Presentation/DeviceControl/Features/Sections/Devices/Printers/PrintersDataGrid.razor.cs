using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.Printer;

namespace DeviceControl.Features.Sections.Devices.Printers;

public sealed partial class PrintersDataGrid : SectionDataGridBase<PrinterEntity>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IPrinterService PrinterService { get; set; } = null!;

    #endregion

    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<PrintersCreateDialog>(new());

    protected override async Task OpenDataGridEntityModal(PrinterEntity item)
        => await OpenSectionModal<PrintersUpdateDialog>(item);

    protected override async Task OpenItemInNewTab(PrinterEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionPrinters}/{item.Uid.ToString()}");

    protected override IEnumerable<PrinterEntity> SetSqlSectionCast() => PrinterService.GetAll();

    protected override IEnumerable<PrinterEntity> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [PrinterService.GetItemByUid(itemUid)];
    }
}