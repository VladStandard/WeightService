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
				DataContext.GetListNotNullable<DeviceModel>(WsSqlCrudConfigUtils.GetCrudConfigComboBox());
				DataContext.GetListNotNullable<DeviceTypeModel>(WsSqlCrudConfigUtils.GetCrudConfigComboBox());
				DataContext.GetListNotNullable<PrinterModel>(WsSqlCrudConfigUtils.GetCrudConfigComboBox());
				DataContext.GetListNotNullable<TemplateModel>(WsSqlCrudConfigUtils.GetCrudConfigComboBox());
				DataContext.GetListNotNullable<WorkShopModel>(WsSqlCrudConfigUtils.GetCrudConfigComboBox());

				SqlItemCast = DataContext.GetItemNotNullable<ScaleModel>(IdentityId);
				SqlItemCast.PrinterMain ??= DataAccess.GetItemNewEmpty<PrinterModel>();
				SqlItemCast.PrinterShipping ??= DataAccess.GetItemNewEmpty<PrinterModel>();
                SqlItemCast.WorkShop ??= DataAccess.GetItemNewEmpty<WorkShopModel>();
				DeviceScaleFk = DataAccess.GetItemDeviceScaleFkNotNullable(SqlItemCast);
				Device = DeviceScaleFk.Device.IsNotNew ? DeviceScaleFk.Device : DataAccess.GetItemNewEmpty<DeviceModel>();

			    // ComPorts
			    ComPorts = MdSerialPortsUtils.GetListTypeComPorts(Lang.English);
			    // ScaleFactor
			    SqlItemCast.ScaleFactor ??= 1000;
			}
		});
	}

	#endregion
}
