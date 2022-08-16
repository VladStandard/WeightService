// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Localizations;
using DataCore.Models;
using DataCore.Protocols;
using DataCore.Sql.Models;
using DataCore.Sql.TableScaleModels;
using Microsoft.AspNetCore.Components;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Item;

/// <summary>
/// Scale item page.
/// </summary>
public partial class ItemScale
{
	#region Public and private fields, properties, constructor

	private ScaleEntity ItemCast { get => Item == null ? new() : (ScaleEntity)Item; set => Item = value; }
	private List<PrinterEntity> PrinterItems { get; set; }
	private List<ProductionFacilityEntity> ProductionFacilityItems { get; set; }
	private List<PrinterEntity> ShippingPrinterItems { get; set; }
	private List<TemplateEntity> TemplatesDefaultItems { get; set; }
	private List<TemplateEntity> TemplatesSeriesItems { get; set; }
	private List<WorkShopEntity> WorkShopItems { get; set; }
	private List<TypeEntity<string>> ComPorts { get; set; }
	private List<HostEntity> HostItems { get; set; }
	[Parameter] public bool IsPluNew { get; set; }

	#endregion

	#region Public and private methods

	protected override void OnInitialized()
	{
		base.OnInitialized();
		
		Table = new TableScaleEntity(ProjectsEnums.TableScale.Scales);
		PrinterItems = new();
		ComPorts = new();
		HostItems = new();
		ProductionFacilityItems = new();
		ShippingPrinterItems = new();
		WorkShopItems = new();
		TemplatesDefaultItems = new();
		TemplatesSeriesItems = new();
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
					new FilterListEntity(new() { new(DbField.IdentityId, DbComparer.Equal, IdentityId) }));
				if (IdentityId != null && TableAction == DbTableAction.New)
					ItemCast.IdentityId = (long)IdentityId;
				if (ItemCast.Host == null)
					ItemCast.Host = new(0, false) { Name = LocaleCore.Table.FieldNull };
				if (ItemCast.PrinterMain == null)
					ItemCast.PrinterMain = new(0, false) { Name = LocaleCore.Table.FieldNull };
				if (ItemCast.PrinterShipping == null)
					ItemCast.PrinterShipping = new(0, false) { Name = LocaleCore.Table.FieldNull };
				if (ItemCast.TemplateDefault == null)
					ItemCast.TemplateDefault = new(0, false) { Title = LocaleCore.Table.FieldNull };
				if (ItemCast.TemplateSeries == null)
					ItemCast.TemplateSeries = new(0, false) { Title = LocaleCore.Table.FieldNull };
				if (ItemCast.WorkShop == null)
					ItemCast.WorkShop = new(0, false) { Name = LocaleCore.Table.FieldNull };

			    // ComPorts
			    ComPorts = SerialPortsUtils.GetListTypeComPorts(Lang.English);
			    // ScaleFactor
			    ItemCast.ScaleFactor ??= 1000;
			    // HostItems.
			    HostEntity[]? hostItems = AppSettings.DataAccess.Crud.GetEntities<HostEntity>(
					new(new() { new(DbField.IsMarked, DbComparer.Equal, false) }),
					new(DbField.Name));
				if (hostItems is { })
				{
					HostItems.Add(new(0, false) { Name = LocaleCore.Table.FieldNull });
					HostItems.AddRange(hostItems);
				}

			    // PrinterItems.
			    PrinterEntity[]? printerItems = AppSettings.DataAccess.Crud.GetEntities<PrinterEntity>(
					new(new() { new(DbField.IsMarked, DbComparer.Equal, false) }));
				if (printerItems is { })
				{
					PrinterItems.Add(new(0, false) { Name = LocaleCore.Table.FieldNull });
					PrinterItems.AddRange(printerItems);
				}

			    // ShippingPrinterItems.
			    PrinterEntity[]? shippingPrinterItems = AppSettings.DataAccess.Crud.GetEntities<PrinterEntity>(
					new(new() { new(DbField.IsMarked, DbComparer.Equal, false) }));
				if (shippingPrinterItems is { })
				{
					ShippingPrinterItems.Add(new(0, false) { Name = LocaleCore.Table.FieldNull });
					ShippingPrinterItems.AddRange(shippingPrinterItems);
				}

			    // TemplatesDefaultItems.
			    TemplateEntity[]? templatesDefaultItems = AppSettings.DataAccess.Crud.GetEntities<TemplateEntity>(
					new(new() { new(DbField.IsMarked, DbComparer.Equal, false) }),
					new(DbField.Title));
				if (templatesDefaultItems is { })
				{
					TemplatesDefaultItems.Add(new(0, false) { Title = LocaleCore.Table.FieldNull });
					TemplatesDefaultItems.AddRange(templatesDefaultItems);
				}

			    // TemplatesSeriesItems.
			    TemplateEntity[]? templatesSeriesItems = AppSettings.DataAccess.Crud.GetEntities<TemplateEntity>(
					new(new() { new(DbField.IsMarked, DbComparer.Equal, false) }),
					new(DbField.Title));
				if (templatesSeriesItems is { })
				{
					TemplatesSeriesItems.Add(new(0, false) { Title = LocaleCore.Table.FieldNull });
					TemplatesSeriesItems.AddRange(templatesSeriesItems);
				}

			    // ProductionFacilityItems.
			    ProductionFacilityEntity[]? productionFacilities =
					AppSettings.DataAccess.Crud.GetEntities<ProductionFacilityEntity>(
						new(new() { new(DbField.IsMarked, DbComparer.Equal, false) }));
				if (productionFacilities is { })
				{
					ProductionFacilityItems.Add(new(0, false) { Name = LocaleCore.Table.FieldNull });
					ProductionFacilityItems.AddRange(productionFacilities.Where(x => x.IdentityId > 0));
				}

			    // WorkShopItems.
			    WorkShopEntity[]? workShopItems = AppSettings.DataAccess.Crud.GetEntities<WorkShopEntity>(
					new(new() { new(DbField.IsMarked, DbComparer.Equal, false) }));
				if (workShopItems is { })
				{
					WorkShopItems.Add(new(0, false) { Name = LocaleCore.Table.FieldNull });
					WorkShopItems.AddRange(workShopItems);
				}

				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

	#endregion
}
