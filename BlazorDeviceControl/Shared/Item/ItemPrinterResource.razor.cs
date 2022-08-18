// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Models;
using DataCore.Sql.TableScaleModels;
using System.Data;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Item;

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
						ItemCast = AppSettings.DataAccess.Crud.GetEntity<PrinterResourceEntity>(
							new(new() { new(DbField.IdentityId, DbComparer.Equal, IdentityId) }));
						break;
				}

				List<PrinterEntity>? printers = AppSettings.DataAccess.Crud.GetEntities<PrinterEntity>(
					new(new() { new(DbField.IsMarked, DbComparer.Equal, false) }),
					new(DbField.Name))?.ToList();
				if (printers is not null)
				{
					Printers = new();
					Printers.AddRange(printers);
				}
				
				List<TemplateResourceEntity>? resources = AppSettings.DataAccess.Crud.GetEntities<TemplateResourceEntity>()?.ToList();
				if (resources is not null)
				{
					Resources = new();
					Resources.AddRange(resources);
				}
				
				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

	#endregion
}
