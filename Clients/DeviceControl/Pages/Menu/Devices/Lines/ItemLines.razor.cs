using WsStorageCore.Tables.TableRefModels.WorkShops;

namespace DeviceControl.Pages.Menu.Devices.Lines;

public sealed partial class ItemLines : ItemBase<WsSqlScaleModel>
{
    #region Public and private fields, properties, constructor

    private WsSqlDeviceModel Device { get; set; }
    private WsSqlDeviceScaleFkModel DeviceScaleFk { get; set; }
    private List<WsEnumTypeModel<string>> ComPorts { get; set; }
    private List<WsSqlPrinterModel> PrinterModels { get; set; }
    private List<WsSqlDeviceModel> HostModels { get; set; }
    private List<WsSqlWorkShopModel> WorkShopModels { get; set; }
    private WsSqlDeviceLineFkRepository DeviceLineFkRepository { get; }

    public ItemLines() : base()
    {
        ComPorts = new();
        Device = new();
        DeviceLineFkRepository = new();
        DeviceScaleFk = new();
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
        HostModels = new WsSqlDeviceRepository().GetEnumerable(WsSqlCrudConfigFactory.GetCrudActual()).ToList();
        WorkShopModels = new WsSqlWorkShopRepository().GetEnumerable(WsSqlCrudConfigFactory.GetCrudActual()).ToList();

        DeviceScaleFk = DeviceLineFkRepository.GetItemByLine(SqlItemCast);
        Device = DeviceScaleFk.Device;
        ComPorts = MdSerialPortsUtils.GetListTypeComPorts(WsEnumLanguage.English);
    }

    protected override bool ValidateItemBeforeSave()
    {
        DeviceScaleFk.Device = Device;
        DeviceScaleFk.Scale = SqlItemCast;
        if (!SqlItemValidateWithMsg(DeviceScaleFk, !(DeviceScaleFk?.IsNew ?? false)))
            return false;
        return base.ValidateItemBeforeSave();
    }

    protected override void ItemSave()
    {
        base.ItemSave();
        SqlItemSave(DeviceScaleFk);
    }

    #endregion
}
