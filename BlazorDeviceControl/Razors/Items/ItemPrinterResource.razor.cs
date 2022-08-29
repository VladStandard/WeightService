// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Fields;

namespace BlazorDeviceControl.Razors.Items;

/// <summary>
/// Item PrinterResource page.
/// </summary>
public partial class ItemPrinterResource : BlazorCore.Models.RazorBase
{
	#region Public and private fields, properties, constructor

	/// <summary>
	/// PrinterResource.
	/// </summary>
	private PrinterResourceEntity ItemCast { get => Item == null ? new() : (PrinterResourceEntity)Item; set => Item = value; }
	/// <summary>
	/// Printers.
	/// </summary>
	private List<PrinterEntity> Printers { get; set; }
	/// <summary>
	/// Printer's resources.
	/// </summary>
	private List<TemplateResourceEntity> Resources { get; set; }

	#endregion

	#region Public and private methods

	protected override void OnInitialized()
	{
		base.OnInitialized();

		Table = new TableScaleEntity(ProjectsEnums.TableScale.PrintersResources);
		ItemCast = new();
		Printers = new();
		Resources = new();
	}

	protected override void OnParametersSet()
	{
		base.OnParametersSet();

		RunActions(new()
		{
			() =>
			{
				switch (TableAction)
				{
					case DbTableAction.New:
						ItemCast = new();
						ItemCast.ChangeDt = ItemCast.CreateDt = DateTime.Now;
						ItemCast.Description = "NEW RESOURCE";
						break;
					default:
						ItemCast = AppSettings.DataAccess.Crud.GetItemByIdNotNull<PrinterResourceEntity>(IdentityId);
						break;
				}

				PrinterEntity[]? printers = AppSettings.DataAccess.Crud.GetItems<PrinterEntity>(
					new FieldFilterModel(DbField.IsMarked, false), new(DbField.Name));
				Printers = new();
				if (printers is not null)
					Printers.AddRange(printers);

				TemplateResourceEntity[]? resources = AppSettings.DataAccess.Crud.GetItems<TemplateResourceEntity>(null, 0);
				Resources = new();
				if (resources is not null)
					Resources.AddRange(resources);
				
				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

	#endregion
}
