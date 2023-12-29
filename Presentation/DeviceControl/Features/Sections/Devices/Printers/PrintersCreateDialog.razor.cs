using DeviceControl.Features.Shared.Modal;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Entities.SchemaRef.Printers;

namespace DeviceControl.Features.Sections.Devices.Printers;

public sealed partial class PrintersCreateDialog: SectionDialogBase<SqlPrinterEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        TabsList = new()
        {
            new(Localizer["SectionPrinters"], "main"),
        };
    }
}