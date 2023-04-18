// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsDataCore.Models;
using WsDataCore.Protocols;
using WsStorageCore.TableScaleFkModels.DeviceScalesFks;
using WsStorageCore.TableScaleModels.Devices;
using WsStorageCore.TableScaleModels.DeviceTypes;
using WsStorageCore.TableScaleModels.Printers;
using WsStorageCore.TableScaleModels.Scales;
using WsStorageCore.TableScaleModels.Templates;
using WsStorageCore.TableScaleModels.WorkShops;
using WsStorageCore.Utils;
using WsStorageCore.TableScaleFkModels.DeviceScalesFks;
using WsStorageCore.TableScaleModels.Devices;
using WsStorageCore.TableScaleModels.DeviceTypes;
using WsStorageCore.TableScaleModels.Printers;
using WsStorageCore.TableScaleModels.Scales;
using WsStorageCore.TableScaleModels.Templates;
using WsStorageCore.TableScaleModels.WorkShops;

namespace BlazorDeviceControl.Pages.Menu.Devices.SectionLines;

/// <summary>
/// Scale item page.
/// </summary>
public sealed partial class ItemLines : RazorComponentItemBase<ScaleModel>
{
	#region Public and private fields, properties, constructor

	private DeviceModel _device;
	private DeviceModel Device { get => _device; set { _device = value; SqlLinkedItems = new() { Device }; } }
	private DeviceScaleFkModel DeviceScaleFk { get; set; }
	private List<TypeModel<string>> ComPorts { get; set; }

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
				ContextManager.ContextList.GetListNotNullable<DeviceModel>(WsSqlCrudConfigUtils.GetCrudConfigComboBox());
				ContextManager.ContextList.GetListNotNullable<DeviceTypeModel>(WsSqlCrudConfigUtils.GetCrudConfigComboBox());
				ContextManager.ContextList.GetListNotNullable<PrinterModel>(WsSqlCrudConfigUtils.GetCrudConfigComboBox());
				ContextManager.ContextList.GetListNotNullable<TemplateModel>(WsSqlCrudConfigUtils.GetCrudConfigComboBox());
				ContextManager.ContextList.GetListNotNullable<WorkShopModel>(WsSqlCrudConfigUtils.GetCrudConfigComboBox());

				SqlItemCast = AccessManager.AccessItem.GetItemNotNullable<ScaleModel>(IdentityId);
				SqlItemCast.PrinterMain ??= AccessManager.AccessItem.GetItemNewEmpty<PrinterModel>();
				SqlItemCast.PrinterShipping ??= AccessManager.AccessItem.GetItemNewEmpty<PrinterModel>();
                SqlItemCast.WorkShop ??= AccessManager.AccessItem.GetItemNewEmpty<WorkShopModel>();
				DeviceScaleFk = ContextManager.ContextItem.GetItemDeviceScaleFkNotNullable(SqlItemCast);
				Device = DeviceScaleFk.Device.IsNotNew ? DeviceScaleFk.Device : AccessManager.AccessItem.GetItemNewEmpty<DeviceModel>();

			    // ComPorts
			    ComPorts = MdSerialPortsUtils.GetListTypeComPorts(Lang.English);
			    // ScaleFactor
			    SqlItemCast.ScaleFactor ??= 1000;
			}
		});
	}

	#endregion
}
