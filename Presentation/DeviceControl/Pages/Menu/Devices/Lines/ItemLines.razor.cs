using Ws.StorageCore.Entities.SchemaRef.Hosts;

namespace DeviceControl.Pages.Menu.Devices.Lines;

public sealed partial class ItemLines : ItemBase<SqlLineEntity>
{
    private List<string> ComPorts { get; set; }
    private List<SqlPrinterEntity> PrinterModels { get; set; }
    private List<SqlHostEntity> HostModels { get; set; }
    private List<SqlWorkShopEntity> WorkShopModels { get; set; }

    public ItemLines() : base()
    {
        ComPorts = new();
        HostModels = new();
        PrinterModels = new();
        WorkShopModels = new();
    }
    
    protected override void SetSqlItemCast()
    {
        base.SetSqlItemCast();
        PrinterModels = new SqlPrinterRepository().GetEnumerable(SqlCrudConfigFactory.GetCrudActual()).ToList();
        HostModels = new SqlHostRepository().GetEnumerable(SqlCrudConfigFactory.GetCrudActual()).ToList();
        WorkShopModels = new SqlWorkShopRepository().GetEnumerable(SqlCrudConfigFactory.GetCrudActual()).ToList();
        ComPorts = Enumerable.Range(1, 10).Select(i => $"COM{i}").ToList();;
    }
}
