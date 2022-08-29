// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Items;

public partial class ItemPrinter : BlazorCore.Models.RazorBase
{
	#region Public and private fields, properties, constructor

	private PrinterEntity ItemCast { get => Item == null ? new() : (PrinterEntity)Item; set => Item = value; }
	private List<PrinterTypeEntity> PrinterTypes { get; set; }

	#endregion

	#region Public and private methods

	protected override void OnInitialized()
	{
		base.OnInitialized();

		Table = new TableScaleEntity(ProjectsEnums.TableScale.Printers);
		ItemCast = new();
		PrinterTypes = new();
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
						ItemCast.IsMarked = false;
						ItemCast.Name = "NEW PRINTER";
						break;
					default:
						ItemCast = AppSettings.DataAccess.Crud.GetItemByIdNotNull<PrinterEntity>(IdentityId);
						break;
				}

				PrinterTypes = AppSettings.DataAccess.Crud.GetItemsListNotNull<PrinterTypeEntity>(false, false);

				if (IdentityId != null && TableAction == DbTableAction.New)
				{
					ItemCast.IdentityId = (long)IdentityId;
					ItemCast.Name = "NEW PRINTER";
					ItemCast.Ip = "127.0.0.1";
					ItemCast.MacAddress.Default();
				}

				ButtonSettings = new(false, false, false, false, false, true, true);
				//await ItemCast.SetHttpStatusAsync().ConfigureAwait(true);
			}
		});
	}

	#endregion
}
