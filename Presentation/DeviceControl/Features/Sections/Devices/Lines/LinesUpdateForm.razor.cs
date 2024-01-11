using DeviceControl.Features.Sections.Shared.Form;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Entities.SchemaRef.Hosts;
using Ws.StorageCore.Entities.SchemaRef.Lines;
using Ws.StorageCore.Entities.SchemaRef.Printers;
using Ws.StorageCore.Entities.SchemaRef.WorkShops;

namespace DeviceControl.Features.Sections.Devices.Lines;

public sealed partial class LinesUpdateForm: SectionFormBase<SqlLineEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;

    private IEnumerable<SqlPrinterEntity> PrinterEntities { get; set; } = new List<SqlPrinterEntity>();
    private IEnumerable<SqlWorkShopEntity> WorkShopEntities { get; set; } = new List<SqlWorkShopEntity>();
    private IEnumerable<SqlHostEntity> HostEntities { get; set; } = new List<SqlHostEntity>();

    protected override void OnInitialized()
    {
        PrinterEntities = new SqlPrinterRepository().GetEnumerable(new()).ToList();
        HostEntities = new SqlHostRepository().GetEnumerable(new()).ToList();
        WorkShopEntities = new SqlWorkShopRepository().GetEnumerable(new()).ToList();
    }

    private SqlPrinterEntity GetPrinterByUid(string printUid) =>
        PrinterEntities.First(x => x.IdentityValueUid == Guid.Parse(printUid));

    private SqlHostEntity GetHostByUid(string hostUid) =>
        HostEntities.First(x => x.IdentityValueUid == Guid.Parse(hostUid));
    
    private SqlWorkShopEntity GetWorkShopByUid(string workShopUid) =>
        WorkShopEntities.First(x => x.IdentityValueUid == Guid.Parse(workShopUid));
}