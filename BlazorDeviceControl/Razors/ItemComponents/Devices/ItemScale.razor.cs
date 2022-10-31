// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.ItemComponents.Devices;

/// <summary>
/// Scale item page.
/// </summary>
public partial class ItemScale : RazorComponentItemBase<ScaleModel>
{
	#region Public and private fields, properties, constructor

	private List<TypeModel<string>> ComPorts { get; set; }
	private DeviceModel Device { get; set; }
	
	public ItemScale()
	{
		RazorComponentConfig.IsShowItemsCount = true;
		RazorComponentConfig.IsShowFilterAdditional = true;
		RazorComponentConfig.IsShowFilterMarked = true;
		ComPorts = new();
		Device = new();
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
				DataContext.GetListNotNull<DeviceModel>(false, false, true);
				DataContext.GetListNotNull<DeviceTypeModel>(false, false, true);
				DataContext.GetListNotNull<PrinterModel>(false, false, true);
				DataContext.GetListNotNull<TemplateModel>(false, false, true);
				DataContext.GetListNotNull<WorkShopModel>(false, false, true);

				SqlItemCast = DataContext.GetItemNotNull<ScaleModel>(IdentityId);
				SqlItemCast.PrinterMain ??= BlazorAppSettings.DataAccess.GetNewItem<PrinterModel>();
				SqlItemCast.PrinterShipping ??= BlazorAppSettings.DataAccess.GetNewItem<PrinterModel>();
				SqlItemCast.TemplateDefault ??= BlazorAppSettings.DataAccess.GetNewItem<TemplateModel>();
				SqlItemCast.TemplateSeries ??= BlazorAppSettings.DataAccess.GetNewItem<TemplateModel>();
				SqlItemCast.WorkShop ??= BlazorAppSettings.DataAccess.GetNewItem<WorkShopModel>();

			    // ComPorts
			    ComPorts = SerialPortsUtils.GetListTypeComPorts(LangEnum.English);
			    // ScaleFactor
			    SqlItemCast.ScaleFactor ??= 1000;
				Device = BlazorAppSettings.DataAccess.GetItemDeviceNotNull(SqlItemCast);
			}
		});
	}

	#endregion
}
