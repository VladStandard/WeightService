// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Fields;

namespace BlazorDeviceControl.Razors.Items;

/// <summary>
/// Scale item page.
/// </summary>
public partial class ItemScaleCore : BlazorCore.Models.RazorBase
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
				ItemCast = AppSettings.DataAccess.Crud.GetItemByIdNotNull<ScaleEntity>(IdentityId);
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
				Hosts = new() { new(0, false) { Name = LocaleCore.Table.FieldNull } };
			    HostEntity[]? hostItems = AppSettings.DataAccess.Crud.GetItems<HostEntity>(
					new FieldFilterModel(DbField.IsMarked, false), new(DbField.Name));
				if (hostItems is not null)
					Hosts.AddRange(hostItems);

			    // PrinterItems.
				Printers = new() { new(0, false) { Name = LocaleCore.Table.FieldNull } };
			    PrinterEntity[]? printerItems = AppSettings.DataAccess.Crud.GetItems<PrinterEntity>(
					new FieldFilterModel(DbField.IsMarked, false));
				if (printerItems is not null)
					Printers.AddRange(printerItems);

			    // Templates.
				Templates = new() { new(0, false) { Title = LocaleCore.Table.FieldNull } };
			    TemplateEntity[]? templatesSeriesItems = AppSettings.DataAccess.Crud.GetItems<TemplateEntity>(
					new FieldFilterModel(DbField.IsMarked, false), new(DbField.Title));
				if (templatesSeriesItems is not null)
					Templates.AddRange(templatesSeriesItems);

			    // ProductionFacilities.
				ProductionFacilities = new() { new(0, false) { Name = LocaleCore.Table.FieldNull } };
			    ProductionFacilityEntity[]? productionFacilities =
					AppSettings.DataAccess.Crud.GetItems<ProductionFacilityEntity>(
						new FieldFilterModel(DbField.IsMarked, false));
				if (productionFacilities is not null)
					ProductionFacilities.AddRange(productionFacilities.Where(x => x.IdentityId > 0));

			    // WorkShopItems.
				WorkShops = new() { new(0, false) { Name = LocaleCore.Table.FieldNull } };
			    WorkShopEntity[]? workShopItems = AppSettings.DataAccess.Crud.GetItems<WorkShopEntity>(
					new FieldFilterModel(DbField.IsMarked, false));
				if (workShopItems is not null)
					WorkShops.AddRange(workShopItems);

				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

	#endregion
}
