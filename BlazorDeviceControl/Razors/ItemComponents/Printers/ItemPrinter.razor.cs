// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.ItemComponents.Printers;

public partial class ItemPrinter : RazorPageItemBase<PrinterModel>
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
				switch (TableAction)
				{
					case SqlTableActionEnum.New:
						ItemCast = new();
						ItemCast.SetDtNow();
						ItemCast.IsMarked = false;
						ItemCast.Name = "NEW PRINTER";
						break;
					default:
						ItemCast = AppSettings.DataAccess.GetItemByIdNotNull<PrinterModel>(IdentityId);
						break;
				}

				PrinterTypes = AppSettings.DataAccess.GetListPrinterTypes(false, false);
				if (TableAction == SqlTableActionEnum.New)
				{
					ItemCast.Name = "NEW PRINTER";
					ItemCast.Ip = "127.0.0.1";
					ItemCast.MacAddress.Default();
				}

				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

	#endregion
}
