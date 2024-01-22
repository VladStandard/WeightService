using DeviceControl.Features.Sections.Shared.Form;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Enums;
using Ws.Services.Features.Printer;
using Ws.Services.Features.Warehouse;

namespace DeviceControl.Features.Sections.Devices.Lines;

public sealed partial class LinesUpdateForm: SectionFormBase<LineEntity>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IPrinterService PrinterService { get; set; } = null!;
    [Inject] private IWarehouseService WarehouseService { get; set; } = null!;

    #endregion
    
    private IEnumerable<PrinterEntity> PrinterEntities { get; set; } = new List<PrinterEntity>();
    private IEnumerable<WarehouseEntity> WarehousesEntities { get; set; } = new List<WarehouseEntity>();
    private IEnumerable<LineTypeEnum> LineTypesEntities { get; set; } = new List<LineTypeEnum>();

    protected override void OnInitialized()
    {
        PrinterEntities = PrinterService.GetAll();
        WarehousesEntities = WarehouseService.GetAll();
        LineTypesEntities = Enum.GetValues(typeof(LineTypeEnum)).Cast<LineTypeEnum>().ToList();
    }
}