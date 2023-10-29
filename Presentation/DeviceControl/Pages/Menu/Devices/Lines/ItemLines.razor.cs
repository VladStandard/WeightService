using WsStorageCore.Entities.SchemaRef.Hosts;
namespace DeviceControl.Pages.Menu.Devices.Lines;

public sealed partial class ItemLines : ItemBase<WsSqlScaleEntity>
{
    #region Public and private fields, properties, constructor
    
    private List<string> ComPorts { get; set; }
    private List<WsSqlPrinterEntity> PrinterModels { get; set; }
    private List<WsSqlHostEntity> HostModels { get; set; }
    private List<WsSqlWorkShopEntity> WorkShopModels { get; set; }

    public ItemLines() : base()
    {
        ComPorts = new();
        HostModels = new();
        PrinterModels = new();
        WorkShopModels = new();
    }

    #endregion

    #region Public and private methods

    protected override void SetSqlItemCast()
    {
        base.SetSqlItemCast();
        PrinterModels = new WsSqlPrinterRepository().GetEnumerable(WsSqlCrudConfigFactory.GetCrudActual()).ToList();
        HostModels = new WsSqlHostRepository().GetEnumerable(WsSqlCrudConfigFactory.GetCrudActual()).ToList();
        WorkShopModels = new WsSqlWorkShopRepository().GetEnumerable(WsSqlCrudConfigFactory.GetCrudActual()).ToList();
        ComPorts = MdSerialPortsUtils.ComPorts;
    }

    #endregion
}
