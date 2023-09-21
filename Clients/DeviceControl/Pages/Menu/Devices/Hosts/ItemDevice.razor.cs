namespace DeviceControl.Pages.Menu.Devices.Hosts;

public sealed partial class ItemDevice : ItemBase<WsSqlDeviceModel>
{
    #region Public and private fields, properties, constructor

    private WsSqlDeviceTypeRepository DeviceTypeRepository { get; } = new();
    private WsSqlDeviceTypeFkRepository DeviceTypeFkRepository { get; } = new();
    private List<WsSqlDeviceTypeModel> DeviceTypes { get; set; }
    private WsSqlDeviceTypeModel DeviceType { get; set; }
    private WsSqlDeviceTypeFkModel DeviceTypeFk { get; set; }

    #endregion

    public ItemDevice() : base()
    {
        DeviceType = new();
        DeviceTypeFk = new();
    }

    #region Public and private methods

    protected override void SetSqlItemCast()
    {
        base.SetSqlItemCast();
        DeviceTypes = DeviceTypeRepository.GetList(WsSqlCrudConfigFactory.GetCrudActual());
        DeviceTypeFk = DeviceTypeFkRepository.GetItemByDevice(SqlItemCast);
        DeviceType = DeviceTypeFk.Type;
    }

    protected override void ItemSave()
    {
        base.ItemSave();
        SqlItemSave(DeviceTypeFk);
    }
    
    #endregion
}
