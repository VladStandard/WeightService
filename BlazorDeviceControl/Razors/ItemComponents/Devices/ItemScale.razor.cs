// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;
using DataCore.Sql.TableScaleFkModels.DeviceScalesFks;
using DataCore.Sql.TableScaleModels.Devices;
using DataCore.Sql.TableScaleModels.DeviceTypes;
using DataCore.Sql.TableScaleModels.Printers;
using DataCore.Sql.TableScaleModels.Scales;
using DataCore.Sql.TableScaleModels.Templates;
using DataCore.Sql.TableScaleModels.WorkShops;

namespace BlazorDeviceControl.Razors.ItemComponents.Devices;

/// <summary>
/// Scale item page.
/// </summary>
public partial class ItemScale : RazorComponentItemBase<ScaleModel>
{
	#region Public and private fields, properties, constructor

	private DeviceModel _device;
	private DeviceModel Device { get => _device; set { _device = value; SqlLinkedItems = new() { Device }; } }
	private DeviceScaleFkModel DeviceScaleFk { get; set; }
	private List<TypeModel<string>> ComPorts { get; set; }

	public ItemScale()
	{
		SqlCrudConfigItem.IsGuiShowItemsCount = true;
		SqlCrudConfigItem.IsGuiShowFilterAdditional = true;
		SqlCrudConfigItem.IsGuiShowFilterMarked = true;
        _device = new();
		DeviceScaleFk = new();
		ComPorts = new();
		ButtonSettings = new(false, false, false, false, false, true, true);
	}

	#endregion

	#region Public and private methods

	protected override void OnParametersSet()
	{
		RunActionsParametersSet(new()
		{
			() =>
			{
				DataContext.GetListNotNullable<DeviceModel>(SqlCrudConfigList);
				DataContext.GetListNotNullable<DeviceTypeModel>(SqlCrudConfigList);
				DataContext.GetListNotNullable<PrinterModel>(SqlCrudConfigList);
				DataContext.GetListNotNullable<TemplateModel>(SqlCrudConfigList);
				DataContext.GetListNotNullable<WorkShopModel>(SqlCrudConfigList);

				SqlItemCast = DataContext.GetItemNotNullable<ScaleModel>(IdentityId);
				SqlItemCast.PrinterMain ??= DataAccess.GetItemNewEmpty<PrinterModel>();
				SqlItemCast.PrinterShipping ??= DataAccess.GetItemNewEmpty<PrinterModel>();
                SqlItemCast.WorkShop ??= DataAccess.GetItemNewEmpty<WorkShopModel>();
				DeviceScaleFk = DataAccess.GetItemDeviceScaleFkNotNullable(SqlItemCast);
				Device = DeviceScaleFk.Device.IsNotNew ? DeviceScaleFk.Device : DataAccess.GetItemNewEmpty<DeviceModel>();

			    // ComPorts
			    ComPorts = SerialPortsUtils.GetListTypeComPorts(Lang.English);
			    // ScaleFactor
			    SqlItemCast.ScaleFactor ??= 1000;
			}
		});
	}

	#endregion
}