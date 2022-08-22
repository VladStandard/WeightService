// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Items;

/// <summary>
/// Scale item page.
/// </summary>
public partial class ItemScale : BlazorCore.Models.RazorBase
{
	#region Public and private fields, properties, constructor

	private ScaleEntity ItemCast { get => Item == null ? new() : (ScaleEntity)Item; set => Item = value; }
	private List<PrinterEntity> Printers { get; set; }
	private List<ProductionFacilityEntity> ProductionFacilities { get; set; }
	private List<TemplateEntity> Templates { get; set; }
	private List<WorkShopEntity> WorkShops { get; set; }
	private List<TypeEntity<string>> ComPorts { get; set; }
	private List<HostEntity> Hosts { get; set; }
	[Parameter] public bool IsPluNew { get; set; }

	#endregion

	#region Public and private methods

	protected override void OnInitialized()
	{
		base.OnInitialized();
		
		Table = new TableScaleEntity(ProjectsEnums.TableScale.Scales);
		Printers = new();
		ComPorts = new();
		Hosts = new();
		ProductionFacilities = new();
		WorkShops = new();
		Templates = new();
		ItemCast = new();
	}

	protected override void OnParametersSet()
	{
		base.OnParametersSet();

		RunActions(new()
		{
			() =>
			{
				ItemCast = AppSettings.DataAccess.Crud.GetEntity<ScaleEntity>(
					new(new() { new(DbField.IdentityId, DbComparer.Equal, IdentityId) }));
				if (IdentityId != null && TableAction == DbTableAction.New)
					ItemCast.IdentityId = (long)IdentityId;
				ItemCast.Host ??= new(0, false) { Name = LocaleCore.Table.FieldNull };
				ItemCast.PrinterMain ??= new(0, false) { Name = LocaleCore.Table.FieldNull };
				ItemCast.PrinterShipping ??= new(0, false) { Name = LocaleCore.Table.FieldNull };
				ItemCast.TemplateDefault ??= new(0, false) { Title = LocaleCore.Table.FieldNull };
				ItemCast.TemplateSeries ??= new(0, false) { Title = LocaleCore.Table.FieldNull };
				ItemCast.WorkShop ??= new(0, false) { Name = LocaleCore.Table.FieldNull };

			    // ComPorts
			    ComPorts = SerialPortsUtils.GetListTypeComPorts(Lang.English);
			    // ScaleFactor
			    ItemCast.ScaleFactor ??= 1000;
			    // HostItems.
			    HostEntity[]? hostItems = AppSettings.DataAccess.Crud.GetEntities<HostEntity>(
					new(new() { new(DbField.IsMarked, DbComparer.Equal, false) }),
					new(DbField.Name));
				if (hostItems is not null)
				{
					Hosts = new() { new(0, false) { Name = LocaleCore.Table.FieldNull } };
					Hosts.AddRange(hostItems);
				}

			    // PrinterItems.
			    PrinterEntity[]? printerItems = AppSettings.DataAccess.Crud.GetEntities<PrinterEntity>(
					new(new() { new(DbField.IsMarked, DbComparer.Equal, false) }));
				if (printerItems is not null)
				{
					Printers = new() { new(0, false) { Name = LocaleCore.Table.FieldNull } };
					Printers.AddRange(printerItems);
				}

			    // Templates.
			    TemplateEntity[]? templatesSeriesItems = AppSettings.DataAccess.Crud.GetEntities<TemplateEntity>(
					new(new() { new(DbField.IsMarked, DbComparer.Equal, false) }),
					new(DbField.Title));
				if (templatesSeriesItems is not null)
				{
					Templates = new() { new(0, false) { Title = LocaleCore.Table.FieldNull } };
					Templates.AddRange(templatesSeriesItems);
				}

			    // ProductionFacilities.
			    ProductionFacilityEntity[]? productionFacilities =
					AppSettings.DataAccess.Crud.GetEntities<ProductionFacilityEntity>(
						new(new() { new(DbField.IsMarked, DbComparer.Equal, false) }));
				if (productionFacilities is { })
				{
					ProductionFacilities = new() { new(0, false) { Name = LocaleCore.Table.FieldNull } };
					ProductionFacilities.AddRange(productionFacilities.Where(x => x.IdentityId > 0));
				}

			    // WorkShopItems.
			    WorkShopEntity[]? workShopItems = AppSettings.DataAccess.Crud.GetEntities<WorkShopEntity>(
					new(new() { new(DbField.IsMarked, DbComparer.Equal, false) }));
				if (workShopItems is not null)
				{
					WorkShops = new() { new(0, false) { Name = LocaleCore.Table.FieldNull } };
					WorkShops.AddRange(workShopItems);
				}

				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

	#endregion
}
