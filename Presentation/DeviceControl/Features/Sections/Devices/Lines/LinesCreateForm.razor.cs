using DeviceControl.Features.Sections.Shared.Form;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Entities.SchemaRef.Lines;
using Ws.StorageCore.Entities.SchemaRef.Printers;
using Ws.StorageCore.Entities.SchemaRef.Warehouses;
using Ws.StorageCore.Enums;

namespace DeviceControl.Features.Sections.Devices.Lines;

public sealed partial class LinesCreateForm: SectionFormBase<SqlLineEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;

    private IEnumerable<SqlPrinterEntity> PrinterEntities { get; set; } = new List<SqlPrinterEntity>();
    private IEnumerable<SqlWarehouseEntity> WarehousesEntities { get; set; } = new List<SqlWarehouseEntity>();
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