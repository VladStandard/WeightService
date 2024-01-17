using DeviceControl.Features.Sections.Shared.Form;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Entities.SchemaRef.Lines;
using Ws.StorageCore.Entities.SchemaRef.Printers;
using Ws.StorageCore.Entities.SchemaRef.Warehouses;

namespace DeviceControl.Features.Sections.Devices.Lines;

public sealed partial class LinesUpdateForm: SectionFormBase<SqlLineEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;

    private IEnumerable<SqlPrinterEntity> PrinterEntities { get; set; } = new List<SqlPrinterEntity>();
    private IEnumerable<SqlWarehouseEntity> WarehousesEntities { get; set; } = new List<SqlWarehouseEntity>();

    protected override void OnInitialized()
    {
        PrinterEntities = new SqlPrinterRepository().GetEnumerable().ToList();
        WarehousesEntities = new SqlWarehouseRepository().GetEnumerable().ToList();
    }
}