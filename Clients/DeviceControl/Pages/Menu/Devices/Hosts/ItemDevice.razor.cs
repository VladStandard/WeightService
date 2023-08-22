// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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

    protected override bool ValidateItemBeforeSave()
    { 
        DeviceTypeFk.Type = DeviceType;
        DeviceTypeFk.Device = SqlItemCast; 
        if (!SqlItemValidateWithMsg(DeviceTypeFk, !(DeviceTypeFk?.IsNew ?? false))) 
            return false;
        return base.ValidateItemBeforeSave();
    }

    protected override void ItemSave()
    {
        base.ItemSave();
        SqlItemSave(DeviceTypeFk);
    }
    
    #endregion
}
