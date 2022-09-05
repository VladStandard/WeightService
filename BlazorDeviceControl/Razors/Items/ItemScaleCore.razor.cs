// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core;

namespace BlazorDeviceControl.Razors.Items;

/// <summary>
/// Scale item page.
/// </summary>
public partial class ItemScaleCore : RazorPageModel
{
	#region Public and private fields, properties, constructor

	private ScaleModel ItemCast { get => Item == null ? new() : (ScaleModel)Item; set => Item = value; }
	private PrinterModel PrinterMain { get => ItemCast.PrinterMain ?? new(); set => ItemCast.PrinterMain = value; }
	private PrinterModel PrinterShipping { get => ItemCast.PrinterShipping ?? new(); set => ItemCast.PrinterShipping = value; }
	private TemplateModel TemplateDefault { get => ItemCast.TemplateDefault ?? new(); set => ItemCast.TemplateDefault = value; }
	private TemplateModel TemplateSeries { get => ItemCast.TemplateSeries ?? new(); set => ItemCast.TemplateSeries = value; }
	private WorkShopModel WorkShop { get => ItemCast.WorkShop ?? new(); set => ItemCast.WorkShop = value; }
	private HostModel Host { get => ItemCast.Host ?? new(); set => ItemCast.Host = value; }
	private List<PrinterModel> Printers { get; set; }
	private List<ProductionFacilityModel> ProductionFacilities { get; set; }
	private List<TemplateModel> Templates { get; set; }
	private List<WorkShopModel> WorkShops { get; set; }
	private List<TypeModel<string>> ComPorts { get; set; }
	private List<HostModel> Hosts { get; set; }
	[Parameter] public bool IsPluNew { get; set; }

	#endregion

	#region Public and private methods

	protected override void OnInitialized()
	{
		base.OnInitialized();

		Table = new TableScaleModel(ProjectsEnums.TableScale.Scales);
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

		RunActionsSilent(new()
		{
			() =>
			{
				ItemCast = AppSettings.DataAccess.GetItemByIdNotNull<ScaleModel>(IdentityId);
				ItemCast.Host ??= new() { Name = LocaleCore.Table.FieldNull };
				ItemCast.PrinterMain ??= new() { Name = LocaleCore.Table.FieldNull };
				ItemCast.PrinterShipping ??= new() { Name = LocaleCore.Table.FieldNull };
				ItemCast.TemplateDefault ??= new() { Title = LocaleCore.Table.FieldNull };
				ItemCast.TemplateSeries ??= new() { Title = LocaleCore.Table.FieldNull };
				ItemCast.WorkShop ??= new() { Name = LocaleCore.Table.FieldNull };

			    // ComPorts
			    ComPorts = SerialPortsUtils.GetListTypeComPorts(Lang.English);
			    // ScaleFactor
			    ItemCast.ScaleFactor ??= 1000;
			    Hosts = AppSettings.DataAccess.GetListHosts(false, false, true);
			    Printers = AppSettings.DataAccess.GetListPrinters(false, false, true);
			    Templates = AppSettings.DataAccess.GetListTemplates(false, false, true);
			    ProductionFacilities = AppSettings.DataAccess.GetListProductionFacilities(false, false, true);
			    WorkShops = AppSettings.DataAccess.GetListWorkShops(false, false, true);

				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

	#endregion
}
