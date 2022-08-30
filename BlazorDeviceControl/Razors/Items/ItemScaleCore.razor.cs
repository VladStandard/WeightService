﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Items;

/// <summary>
/// Scale item page.
/// </summary>
public partial class ItemScaleCore : RazorBase
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
				SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(DbField.Name), 0, false, false);
				HostEntity[]? hostItems = AppSettings.DataAccess.Crud.GetItems<HostEntity>(sqlCrudConfig);
				if (hostItems is not null)
					Hosts.AddRange(hostItems);

			    // PrinterItems.
				Printers = new() { new(0, false) { Name = LocaleCore.Table.FieldNull } };
				sqlCrudConfig = SqlUtils.GetCrudConfigIsMarked();
				PrinterEntity[]? printerItems = AppSettings.DataAccess.Crud.GetItems<PrinterEntity>(sqlCrudConfig);
				if (printerItems is not null)
					Printers.AddRange(printerItems);

			    // Templates.
				Templates = new() { new(0, false) { Title = LocaleCore.Table.FieldNull } };
				sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(DbField.Title), 0, false, false);
				TemplateEntity[]? templatesSeriesItems = AppSettings.DataAccess.Crud.GetItems<TemplateEntity>(sqlCrudConfig);
				if (templatesSeriesItems is not null)
					Templates.AddRange(templatesSeriesItems);

			    // ProductionFacilities.
				ProductionFacilities = new() { new(0, false) { Name = LocaleCore.Table.FieldNull } };
				sqlCrudConfig = SqlUtils.GetCrudConfigIsMarked();
				ProductionFacilityEntity[]? productionFacilities = AppSettings.DataAccess.Crud.GetItems<ProductionFacilityEntity>(sqlCrudConfig);
				if (productionFacilities is not null)
					ProductionFacilities.AddRange(productionFacilities.Where(x => x.IdentityId > 0));

			    // WorkShopItems.
				WorkShops = new() { new(0, false) { Name = LocaleCore.Table.FieldNull } };
				sqlCrudConfig = SqlUtils.GetCrudConfigIsMarked();
				WorkShopEntity[]? workShopItems = AppSettings.DataAccess.Crud.GetItems<WorkShopEntity>(sqlCrudConfig);
				if (workShopItems is not null)
					WorkShops.AddRange(workShopItems);

				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

	#endregion
}
