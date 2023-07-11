// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsDataCore.Enums;
using WsDataCore.Protocols;
using WsStorageCore.TableScaleFkModels.DeviceScalesFks;
using WsStorageCore.TableScaleModels.Devices;
using WsStorageCore.TableScaleModels.Printers;
using WsStorageCore.TableScaleModels.Scales;
using WsStorageCore.TableScaleModels.WorkShops;

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
        PrinterModels = ContextManager.ContextList.GetListNotNullable<WsSqlPrinterModel>(WsSqlCrudConfigUtils.GetCrudConfigComboBox());
        HostModels = ContextManager.ContextList.GetListNotNullable<WsSqlDeviceModel>(WsSqlCrudConfigUtils.GetCrudConfigComboBox());
        WorkShopModels = ContextManager.ContextList.GetListNotNullable<WsSqlWorkShopModel>(WsSqlCrudConfigUtils.GetCrudConfigComboBox());

        SqlItemCast.PrinterMain ??= SqlItemNewEmpty<WsSqlPrinterModel>();
        SqlItemCast.PrinterShipping ??= SqlItemNewEmpty<WsSqlPrinterModel>();
        SqlItemCast.WorkShop ??= SqlItemNewEmpty<WsSqlWorkShopModel>();

        DeviceScaleFk = ContextManager.ContextItem.GetItemDeviceScaleFkNotNullable(SqlItemCast);
        Device = DeviceScaleFk.Device.IsNotNew ? DeviceScaleFk.Device : SqlItemNewEmpty<WsSqlDeviceModel>();
        ComPorts = MdSerialPortsUtils.GetListTypeComPorts(WsEnumLanguage.English);
    }

    protected override void SqlItemSaveAdditional()
    {
        if (Device.IsNotNew)
        {
            DeviceScaleFk.Device = Device;
            DeviceScaleFk.Scale = SqlItemCast;
            SqlItemSave(DeviceScaleFk);
            return;
        }

        ContextManager.SqlCore.Delete(DeviceScaleFk);
    }

    #endregion
}