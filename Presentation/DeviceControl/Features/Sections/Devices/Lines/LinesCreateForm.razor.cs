using DeviceControl.Features.Sections.Shared.Form;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Enums;
using Ws.Infrastructure.Models.Enums;
using Ws.StorageCore.Entities.Ref.Printers;
using Ws.StorageCore.Entities.Ref.Warehouses;

namespace DeviceControl.Features.Sections.Devices.Lines;

public sealed partial class LinesCreateForm: SectionFormBase<LineEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;

    private IEnumerable<PrinterEntity> PrinterEntities { get; set; } = new List<PrinterEntity>();
    private IEnumerable<WarehouseEntity> WarehousesEntities { get; set; } = new List<WarehouseEntity>();
    private IEnumerable<LineTypeEnum> LineTypesEntities { get; set; } = new List<LineTypeEnum>();
    
    protected override void OnInitialized()
    {
        SectionEntity.Warehouse.Name = Localizer["SectionFormWarehouseDefaultName"];
        SectionEntity.Printer.Name = Localizer["SectionFormPrinterDefaultName"];

        WarehousesEntities = new SqlWarehouseRepository().GetEnumerable();
        PrinterEntities = new SqlPrinterRepository().GetEnumerable();

        PrinterEntities = PrinterEntities.Append(SectionEntity.Printer);
        WarehousesEntities = WarehousesEntities.Append(SectionEntity.Warehouse);
        
        LineTypesEntities = Enum.GetValues(typeof(LineTypeEnum)).Cast<LineTypeEnum>().ToList();
    }
}