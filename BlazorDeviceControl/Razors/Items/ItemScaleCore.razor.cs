// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
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
			    Hosts = AppSettings.DataAccess.Crud.GetListHosts(false, false, true);
			    Printers = AppSettings.DataAccess.Crud.GetListPrinters(false, false, true);
			    Templates = AppSettings.DataAccess.Crud.GetListTemplates(false, false, true);
			    ProductionFacilities = AppSettings.DataAccess.Crud.GetListProductionFacilities(false, false, true);
			    WorkShops = AppSettings.DataAccess.Crud.GetListWorkShops(false, false, true);

				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

	#endregion
}
