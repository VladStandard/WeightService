// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Items;

/// <summary>
/// Item PrinterResource page.
/// </summary>
public partial class ItemPrinterResource : RazorPageBase
{
	#region Public and private fields, properties, constructor

	/// <summary>
	/// PrinterResource.
	/// </summary>
	private PrinterResourceModel ItemCast { get => Item is null ? new() : (PrinterResourceModel)Item; set => Item = value; }
	/// <summary>
	/// Printers.
	/// </summary>
	private List<PrinterModel> Printers { get; set; }
	/// <summary>
	/// Printer's resources.
	/// </summary>
	private List<TemplateResourceModel> Resources { get; set; }

	#endregion

	#region Public and private methods

	protected override void OnInitialized()
	{
		base.OnInitialized();

		RunActionsInitialized(new()
		{
			() =>
			{
				Table = new TableScaleModel(SqlTableScaleEnum.PrintersResources);
				ItemCast = new();
				Printers = new();
				Resources = new();
			}
		});
	}

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
