// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.ItemComponents.Printers;

public partial class ItemPrinterType : RazorComponentItemBase<PrinterTypeModel>
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
						SqlItemCast = new();
						SqlItemCast.SetDtNow();
						SqlItemCast.IsMarked = false;
						SqlItemCast.Name = "NEW PRINTER";
						break;
					default:
						SqlItemCast = AppSettings.DataAccess.GetItemByIdNotNull<PrinterTypeModel>(IdentityId);
						break;
				}

				if (TableAction == SqlTableActionEnum.New)
				{
					//SqlItemCast.Identity.Id = (long)IdentityId;
					SqlItemCast.Name = "NEW PRINTER_TYPE";
				}

				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

	#endregion
}
