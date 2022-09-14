// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.Items;

/// <summary>
/// Item PrinterResource page.
/// </summary>
public partial class ItemPrinterResource : RazorPageItemBase<PrinterResourceModel>
{
	#region Public and private fields, properties, constructor

	private List<PrinterModel> Printers { get; set; }
	private List<TemplateResourceModel> Resources { get; set; }

	public ItemPrinterResource()
	{
		Printers = new();
		Resources = new();
	}

	#endregion

	#region Public and private methods

	protected override void OnParametersSet()
	{
		base.OnParametersSet();

		RunActionsParametersSet(new()
		{
			() =>
			{
				switch (TableAction)
				{
					case SqlTableActionEnum.New:
						ItemCast = new();
						ItemCast.SetDtNow();
						ItemCast.Description = "NEW RESOURCE";
						break;
					default:
						ItemCast = AppSettings.DataAccess.GetItemByIdNotNull<PrinterResourceModel>(IdentityId);
						break;
				}
				Printers = AppSettings.DataAccess.GetListPrinters(false, false, false);
				Resources = AppSettings.DataAccess.GetListTemplateResources(false, false);
				
				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

	#endregion
}
