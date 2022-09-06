// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core;

namespace BlazorDeviceControl.Razors.Items;

public partial class ItemPrinter : RazorPageModel
{
	#region Public and private fields, properties, constructor

	private PrinterModel ItemCast { get => Item == null ? new() : (PrinterModel)Item; set => Item = value; }
	private List<PrinterTypeModel> PrinterTypes { get; set; }

	#endregion

	#region Public and private methods

	protected override void OnInitialized()
	{
		base.OnInitialized();

		Table = new TableScaleModel(ProjectsEnums.TableScale.Printers);
		ItemCast = new();
		PrinterTypes = new();
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
						ItemCast.SetDtNow();
						ItemCast.IsMarked = false;
						ItemCast.Name = "NEW PRINTER";
						break;
					default:
						ItemCast = AppSettings.DataAccess.GetItemByIdNotNull<PrinterModel>(IdentityId);
						break;
				}

				PrinterTypes = AppSettings.DataAccess.GetListPrinterTypes(false, false);
				if (TableAction == DbTableAction.New)
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
