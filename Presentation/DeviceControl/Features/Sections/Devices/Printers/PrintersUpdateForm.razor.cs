using DeviceControl.Features.Shared.Form;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Entities.SchemaRef.Printers;
using Ws.StorageCore.Enums;

namespace DeviceControl.Features.Sections.Devices.Printers;

public sealed partial class PrintersUpdateForm: SectionFormBase<SqlPrinterEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;

    private IEnumerable<PrinterTypeEnum> PrinterTypesEntities { get; set; } = new List<PrinterTypeEnum>();

    protected override void OnInitialized()
    {
        PrinterTypesEntities = Enum.GetValues(typeof(PrinterTypeEnum)).Cast<PrinterTypeEnum>().ToList();
    }

    private PrinterTypeEnum GetPrinterTypeByValue(string typeValue) => 
        typeValue.ToEnum<PrinterTypeEnum>();
}