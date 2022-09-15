// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.ItemComponents.Printers;

public partial class ItemPrinterType : RazorPageItemBase<PrinterTypeModel>
{
	#region Public and private fields, properties, constructor

	public ItemPrinterType()
	{
		//
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
						ItemCast = AppSettings.DataAccess.GetItemByIdNotNull<PrinterTypeModel>(IdentityId);
						break;
				}

				if (TableAction == SqlTableActionEnum.New)
				{
					//ItemCast.Identity.Id = (long)IdentityId;
					ItemCast.Name = "NEW PRINTER_TYPE";
				}

				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

	#endregion
}
