// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsDataCore.Models;
using WsDataCore.Protocols;
using WsStorageCore.TableScaleFkModels.DeviceScalesFks;
using WsStorageCore.TableScaleModels.Devices;
using WsStorageCore.TableScaleModels.Printers;
using WsStorageCore.TableScaleModels.Scales;
using WsStorageCore.TableScaleModels.WorkShops;

namespace BlazorDeviceControl.Pages.Menu.Devices.SectionLines;

/// <summary>
/// Scale item page.
/// </summary>
public sealed partial class ItemLines : RazorComponentItemBase<WsSqlScaleModel>
{
    #region Public and private fields, properties, constructor

	private WsSqlDeviceModel _device;
	private WsSqlDeviceModel Device { get => _device; set { _device = value; SqlLinkedItems = new() { Device }; } }
	private WsSqlDeviceScaleFkModel DeviceScaleFk { get; set; }
	private List<TypeModel<string>> ComPorts { get; set; }
    
    private List<WsSqlPrinterModel> PrinterModels { get; set; }

    private List<WsSqlDeviceModel> HostModels { get; set; }
    
    private List<WsSqlWorkShopModel> WorkShopModels { get; set; }
    
	public ItemLines() : base()
	{
		SqlCrudConfigItem.IsGuiShowItemsCount = true;
		SqlCrudConfigItem.IsGuiShowFilterAdditional = true;
		SqlCrudConfigItem.IsGuiShowFilterMarked = true;
        _device = new();
		DeviceScaleFk = new();
        ComPorts = new();
    }

    #endregion

    #region Public and private methods

	protected override void OnParametersSet()
	{
		RunActionsParametersSet(new()
		{
			() =>
			{
                PrinterModels = ContextManager.ContextList.GetListNotNullable<WsSqlPrinterModel>(WsSqlCrudConfigUtils.GetCrudConfigComboBox());
                HostModels = ContextManager.ContextList.GetListNotNullable<WsSqlDeviceModel>(WsSqlCrudConfigUtils.GetCrudConfigComboBox());
		        WorkShopModels = ContextManager.ContextList.GetListNotNullable<WsSqlWorkShopModel>(WsSqlCrudConfigUtils.GetCrudConfigComboBox());

				SqlItemCast = ContextManager.AccessManager.AccessItem.GetItemNotNullable<WsSqlScaleModel>(IdentityId);
				SqlItemCast.PrinterMain ??= ContextManager.AccessManager.AccessItem.GetItemNewEmpty<WsSqlPrinterModel>();
				SqlItemCast.PrinterShipping ??= ContextManager.AccessManager.AccessItem.GetItemNewEmpty<WsSqlPrinterModel>();
                SqlItemCast.WorkShop ??= ContextManager.AccessManager.AccessItem.GetItemNewEmpty<WsSqlWorkShopModel>();
                
				DeviceScaleFk = ContextManager.ContextItem.GetItemDeviceScaleFkNotNullable(SqlItemCast);
				Device = DeviceScaleFk.Device.IsNotNew ? DeviceScaleFk.Device : ContextManager.AccessManager.AccessItem.GetItemNewEmpty<WsSqlDeviceModel>();

                // ComPorts
                ComPorts = MdSerialPortsUtils.GetListTypeComPorts(Lang.English);
                // ScaleFactor
                SqlItemCast.ScaleFactor ??= 1000;
            }
        });
    }

    #endregion
}