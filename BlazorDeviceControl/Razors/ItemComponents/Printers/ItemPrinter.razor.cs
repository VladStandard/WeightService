// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.ItemComponents.Printers;

public partial class ItemPrinter : RazorComponentItemBase<PrinterModel>
{
	#region Public and private fields, properties, constructor

	private List<PrinterTypeModel> PrinterTypes { get; set; }

	public ItemPrinter()
	{
		PrinterTypes = new();
	}

	#endregion

	#region Public and private methods

	protected override void OnParametersSet()
	{
		RunActionsParametersSet(new()
		{
			() =>
			{
                SqlItemCast = AppSettings.DataAccess.GetItemByIdNotNull<PrinterModel>(IdentityId);
                if (SqlItemCast.IdentityIsNew)
	                SqlItem = SqlItemNew<PrinterModel>();
				PrinterTypes = AppSettings.DataAccess.GetListPrinterTypes(false, false);

				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

	#endregion
}
