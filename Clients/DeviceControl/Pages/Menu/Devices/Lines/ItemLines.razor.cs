// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsDataCore.Enums;
using WsDataCore.Protocols;
using WsStorageCore.Tables.TableScaleFkModels.DeviceScalesFks;
using WsStorageCore.Tables.TableScaleModels.Devices;
using WsStorageCore.Tables.TableScaleModels.Printers;
using WsStorageCore.Tables.TableScaleModels.Scales;
using WsStorageCore.Tables.TableScaleModels.WorkShops;

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

    private WsSqlDeviceLineFkRepository DeviceLineFkRepository { get; } = new();
    
    public ItemLines() : base()
    {
        Device = new();
        DeviceScaleFk = new();
        ComPorts = new();
    }

    #endregion

    #region Public and private methods

    protected override void SetSqlItemCast()
    {
        base.SetSqlItemCast();
        PrinterModels =  new WsSqlPrinterRepository().GetList(WsSqlCrudConfigFactory.GetCrudActual());
        HostModels =  new WsSqlDeviceRepository().GetList(WsSqlCrudConfigFactory.GetCrudActual());
        WorkShopModels = new WsSqlWorkShopRepository().GetList(WsSqlCrudConfigFactory.GetCrudActual());
        
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