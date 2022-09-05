// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core;

namespace BlazorDeviceControl.Razors.Items;

/// <summary>
/// Item PrinterResource page.
/// </summary>
public partial class ItemPrinterResource : RazorPageModel
{
	#region Public and private fields, properties, constructor

	/// <summary>
	/// PrinterResource.
	/// </summary>
	private PrinterResourceModel ItemCast { get => Item == null ? new() : (PrinterResourceModel)Item; set => Item = value; }
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

		Table = new TableScaleModel(ProjectsEnums.TableScale.PrintersResources);
		ItemCast = new();
		Printers = new();
		Resources = new();
	}

	protected override void OnParametersSet()
	{
		base.OnParametersSet();

		RunActionsSilent(new()
		{
			() =>
			{
				switch (TableAction)
				{
					case DbTableAction.New:
						ItemCast = new();
						ItemCast.SetDt();
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
