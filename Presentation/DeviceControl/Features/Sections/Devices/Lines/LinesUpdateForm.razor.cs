using DeviceControl.Features.Sections.Shared.Form;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Entities.SchemaRef.Lines;
using Ws.StorageCore.Entities.SchemaRef.Printers;
using Ws.StorageCore.Entities.SchemaRef.WorkShops;

namespace DeviceControl.Features.Sections.Devices.Lines;

public sealed partial class LinesUpdateForm: SectionFormBase<SqlLineEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;

    private IEnumerable<SqlPrinterEntity> PrinterEntities { get; set; } = new List<SqlPrinterEntity>();
    private IEnumerable<SqlWorkShopEntity> WorkShopEntities { get; set; } = new List<SqlWorkShopEntity>();

    protected override void OnInitialized()
    {
        PrinterEntities = new SqlPrinterRepository().GetEnumerable().ToList();
        WorkShopEntities = new SqlWorkShopRepository().GetEnumerable().ToList();
    }
}