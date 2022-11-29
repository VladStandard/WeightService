// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;
using DataCore.Sql.Tables;

namespace DataCore.Sql.Core;

public partial class DataContextModel
{
	#region Public and private fields, properties, constructor

	public DataAccessHelper DataAccess { get; } = DataAccessHelper.Instance;
	public List<AccessModel> Accesses { get; set; }
	public List<AppModel> Apps { get; set; }
	public List<BarCodeModel> BarCodes { get; set; }
	public List<BrandModel> Brands { get; set; }
	public List<ContragentModel> Contragents { get; set; }
	public List<DeviceModel> Devices { get; set; }
	public List<DeviceTypeModel> DeviceTypes { get; set; }
	public List<DeviceTypeFkModel> DeviceTypeFks { get; set; }
	public List<DeviceScaleFkModel> DeviceScaleFks { get; set; }
	public List<LogModel> Logs { get; set; }
	public List<LogTypeModel> LogTypes { get; set; }
	public List<NomenclatureModel> Nomenclatures { get; set; }
	public List<OrderModel> Orders { get; set; }
	public List<OrganizationModel> Organizations { get; set; }
	public List<PackageModel> Packages { get; set; }
	public List<PluLabelModel> PluLabels { get; set; }
	public List<PluModel> Plus { get; set; }
	public List<PluPackageModel> PluPackages { get; set; }
	public List<PluScaleModel> PluScales { get; set; }
	public List<PluWeighingModel> PluWeighings { get; set; }
	public List<PrinterModel> Printers { get; set; }
	public List<PrinterResourceModel> PrinterResources { get; set; }
	public List<PrinterTypeModel> PrinterTypes { get; set; }
	public List<ProductionFacilityModel> ProductionFacilities { get; set; }
	public List<ProductSeriesModel> ProductSeries { get; set; }
	public List<ScaleModel> Scales { get; set; }
	public List<ScaleScreenShotModel> ScaleScreenShots { get; set; }
	public List<TaskModel> Tasks { get; set; }
	public List<TaskTypeModel> TaskTypes { get; set; }
	public List<TemplateModel> Templates { get; set; }
	public List<TemplateResourceModel> TemplateResources { get; set; }
	public List<VersionModel> Versions { get; set; }
	public List<WorkShopModel> WorkShops { get; set; }

	public DataContextModel()
	{
		Accesses = new();
		Apps = new();
		BarCodes = new();
		Brands = new();
		Contragents = new();
		Devices = new();
		DeviceTypes = new();
		DeviceTypeFks = new();
		DeviceScaleFks = new();
		Logs = new();
		LogTypes = new();
		Nomenclatures = new();
		Orders = new();
		Organizations = new();
		Packages = new();
		PluLabels = new();
		Plus = new();
		PluPackages = new();
		PluScales = new();
		PluWeighings = new();
		Printers = new();
		PrinterResources = new();
		PrinterTypes = new();
		ProductionFacilities = new();
		ProductSeries = new();
		Scales = new();
		ScaleScreenShots = new();
		Tasks = new();
		TaskTypes = new();
		Templates = new();
		TemplateResources = new();
		Versions = new();
		WorkShops = new();
	}

	#endregion

	#region Public and private methods

	public List<T> GetListNotNullable<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
	{
		switch (typeof(T))
		{
			case var cls when cls == typeof(AccessModel):
				Accesses = DataAccess.GetListNotNullable<AccessModel>(sqlCrudConfig);
				if (sqlCrudConfig.IsResultOrder)
					Accesses = Accesses.OrderBy(item => item.Name).ToList();
				return Accesses.Cast<T>().ToList();
			case var cls when cls == typeof(AppModel):
				Apps = DataAccess.GetListNotNullable<AppModel>(sqlCrudConfig);
				if (sqlCrudConfig.IsResultOrder)
					Apps = Apps.OrderBy(item => item.Name).ToList();
				return Apps.Cast<T>().ToList();
			case var cls when cls == typeof(BarCodeModel):
				BarCodes = DataAccess.GetListNotNullable<BarCodeModel>(sqlCrudConfig);
				if (sqlCrudConfig.IsResultOrder)
					BarCodes = BarCodes.OrderByDescending(item => item.ChangeDt).ToList();
				return BarCodes.Cast<T>().ToList();
			case var cls when cls == typeof(BrandModel):
				Brands = DataAccess.GetListNotNullable<BrandModel>(sqlCrudConfig);
				if (sqlCrudConfig.IsResultOrder)
                    Brands = Brands.OrderBy(item => item.Name).ToList();
				return Brands.Cast<T>().ToList();
			case var cls when cls == typeof(ContragentModel):
				Contragents = DataAccess.GetListNotNullable<ContragentModel>(sqlCrudConfig);
				if (sqlCrudConfig.IsResultOrder)
					Contragents = Contragents.OrderBy(item => item.Name).ToList();
				return Contragents.Cast<T>().ToList();
			case var cls when cls == typeof(DeviceModel):
				Devices = DataAccess.GetListNotNullable<DeviceModel>(sqlCrudConfig);
				if (sqlCrudConfig.IsResultOrder)
					Devices = Devices.OrderBy(item => item.Name).ToList();
				return Devices.Cast<T>().ToList();
			case var cls when cls == typeof(DeviceTypeModel):
				DeviceTypes = DataAccess.GetListNotNullable<DeviceTypeModel>(sqlCrudConfig);
				if (sqlCrudConfig.IsResultOrder)
					DeviceTypes = DeviceTypes.OrderBy(item => item.Name).ToList();
				return DeviceTypes.Cast<T>().ToList();
			case var cls when cls == typeof(DeviceTypeFkModel):
				DeviceTypeFks = DataAccess.GetListNotNullable<DeviceTypeFkModel>(sqlCrudConfig);
				if (sqlCrudConfig.IsResultOrder)
				{
					DeviceTypeFks = DeviceTypeFks.OrderBy(item => item.Type.Name).ToList();
					DeviceTypeFks = DeviceTypeFks.OrderBy(item => item.Device.Name).ToList();
				}
				return DeviceTypeFks.Cast<T>().ToList();
			case var cls when cls == typeof(DeviceScaleFkModel):
				DeviceScaleFks = DataAccess.GetListNotNullable<DeviceScaleFkModel>(sqlCrudConfig);
				if (sqlCrudConfig.IsResultOrder)
				{
					DeviceScaleFks = DeviceScaleFks.OrderBy(item => item.Device.Name).ToList();
					DeviceScaleFks = DeviceScaleFks.OrderBy(item => item.Scale.Name).ToList();
				}
				return DeviceScaleFks.Cast<T>().ToList();
			case var cls when cls == typeof(LogModel):
				Logs = DataAccess.GetListNotNullable<LogModel>(sqlCrudConfig);
				if (sqlCrudConfig.IsResultOrder)
					Logs = Logs.OrderByDescending(item => item.ChangeDt).ToList();
				return Logs.Cast<T>().ToList();
			case var cls when cls == typeof(LogTypeModel):
				LogTypes = DataAccess.GetListNotNullable<LogTypeModel>(sqlCrudConfig);
				if (sqlCrudConfig.IsResultOrder)
					LogTypes = LogTypes.OrderBy(item => item.Name).ToList();
				return LogTypes.Cast<T>().ToList();
			case var cls when cls == typeof(NomenclatureModel):
				Nomenclatures = DataAccess.GetListNotNullable<NomenclatureModel>(sqlCrudConfig);
				if (sqlCrudConfig.IsResultOrder)
					Nomenclatures = Nomenclatures.OrderBy(item => item.Name).ToList();
				return Nomenclatures.Cast<T>().ToList();
			case var cls when cls == typeof(OrderModel):
				Orders = DataAccess.GetListNotNullable<OrderModel>(sqlCrudConfig);
				if (sqlCrudConfig.IsResultOrder)
					Orders = Orders.OrderByDescending(item => item.ChangeDt).ToList();
				return Orders.Cast<T>().ToList();
			case var cls when cls == typeof(OrganizationModel):
				Organizations = DataAccess.GetListNotNullable<OrganizationModel>(sqlCrudConfig);
				if (sqlCrudConfig.IsResultOrder)
					Organizations = Organizations.OrderBy(item => item.Name).ToList();
				return Organizations.Cast<T>().ToList();
			case var cls when cls == typeof(PackageModel):
				Packages = DataAccess.GetListNotNullable<PackageModel>(sqlCrudConfig);
				if (sqlCrudConfig.IsResultOrder)
					Packages = Packages.OrderBy(item => item.Name).ToList();
				return Packages.Cast<T>().ToList();
			case var cls when cls == typeof(PluLabelModel):
				PluLabels = DataAccess.GetListNotNullable<PluLabelModel>(sqlCrudConfig);
				if (sqlCrudConfig.IsResultOrder)
					PluLabels = PluLabels.OrderByDescending(item => item.ChangeDt).ToList();
				return PluLabels.Cast<T>().ToList();
			case var cls when cls == typeof(PluModel):
				Plus = DataAccess.GetListNotNullable<PluModel>(sqlCrudConfig);
				if (sqlCrudConfig.IsResultOrder)
					Plus = Plus.OrderBy(item => item.Name).ToList();
				return Plus.Cast<T>().ToList();
			case var cls when cls == typeof(PluPackageModel):
				PluPackages = DataAccess.GetListNotNullable<PluPackageModel>(sqlCrudConfig);
				if (sqlCrudConfig.IsResultOrder)
				{
					PluPackages = PluPackages.OrderBy(item => item.Package.Name).ToList();
					PluPackages = PluPackages.OrderBy(item => item.Plu.Name).ToList();
				}
				return PluPackages.Cast<T>().ToList();
			case var cls when cls == typeof(PluScaleModel):
				PluScales = DataAccess.GetListNotNullable<PluScaleModel>(sqlCrudConfig);
				if (sqlCrudConfig.IsResultOrder)
				{
					PluScales = PluScales.OrderBy(item => item.Scale.Name).ToList();
					PluScales = PluScales.OrderBy(item => item.Plu.Name).ToList();
				}
				return PluScales.Cast<T>().ToList();
			case var cls when cls == typeof(PluWeighingModel):
				PluWeighings = DataAccess.GetListNotNullable<PluWeighingModel>(sqlCrudConfig);
				if (sqlCrudConfig.IsResultOrder)
					PluWeighings = PluWeighings.OrderByDescending(item => item.ChangeDt).ToList();
				return PluWeighings.Cast<T>().ToList();
			case var cls when cls == typeof(PrinterModel):
				Printers = DataAccess.GetListNotNullable<PrinterModel>(sqlCrudConfig);
				if (sqlCrudConfig.IsResultOrder)
					Printers = Printers.OrderBy(item => item.Name).ToList();
				return Printers.Cast<T>().ToList();
			case var cls when cls == typeof(PrinterResourceModel):
				PrinterResources = DataAccess.GetListNotNullable<PrinterResourceModel>(sqlCrudConfig);
				if (sqlCrudConfig.IsResultOrder)
				{
					PrinterResources = PrinterResources.OrderBy(item => item.Printer.Name).ToList();
					PrinterResources = PrinterResources.OrderBy(item => item.TemplateResource.Name).ToList();
				}
				return PrinterResources.Cast<T>().ToList();
			case var cls when cls == typeof(PrinterTypeModel):
				PrinterTypes = DataAccess.GetListNotNullable<PrinterTypeModel>(sqlCrudConfig);
				if (sqlCrudConfig.IsResultOrder)
					PrinterTypes = PrinterTypes.OrderBy(item => item.Name).ToList();
				return PrinterTypes.Cast<T>().ToList();
			case var cls when cls == typeof(ProductionFacilityModel):
				ProductionFacilities = DataAccess.GetListNotNullable<ProductionFacilityModel>(sqlCrudConfig);
				if (sqlCrudConfig.IsResultOrder)
					ProductionFacilities = ProductionFacilities.OrderBy(item => item.Name).ToList();
				return ProductionFacilities.Cast<T>().ToList();
			case var cls when cls == typeof(ProductSeriesModel):
				ProductSeries = DataAccess.GetListNotNullable<ProductSeriesModel>(sqlCrudConfig);
				if (sqlCrudConfig.IsResultOrder)
					ProductSeries = ProductSeries.OrderByDescending(item => item.ChangeDt).ToList();
				return ProductSeries.Cast<T>().ToList();
			case var cls when cls == typeof(ScaleModel):
				Scales = DataAccess.GetListNotNullable<ScaleModel>(sqlCrudConfig);
				if (sqlCrudConfig.IsResultOrder)
					Scales = Scales.OrderBy(item => item.Name).ToList();
				return Scales.Cast<T>().ToList();
			case var cls when cls == typeof(ScaleScreenShotModel):
				ScaleScreenShots = DataAccess.GetListNotNullable<ScaleScreenShotModel>(sqlCrudConfig);
				if (sqlCrudConfig.IsResultOrder)
					ScaleScreenShots = ScaleScreenShots.OrderByDescending(item => item.ChangeDt).ToList();
				return ScaleScreenShots.Cast<T>().ToList();
			case var cls when cls == typeof(TaskModel):
				Tasks = DataAccess.GetListNotNullable<TaskModel>(sqlCrudConfig);
				if (sqlCrudConfig.IsResultOrder)
					Tasks = Tasks.OrderBy(item => item.Name).ToList();
				return Tasks.Cast<T>().ToList();
			case var cls when cls == typeof(TaskTypeModel):
				TaskTypes = DataAccess.GetListNotNullable<TaskTypeModel>(sqlCrudConfig);
				if (sqlCrudConfig.IsResultOrder)
					TaskTypes = TaskTypes.OrderBy(item => item.Name).ToList();
				return TaskTypes.Cast<T>().ToList();
			case var cls when cls == typeof(TemplateModel):
				Templates = DataAccess.GetListNotNullable<TemplateModel>(sqlCrudConfig);
				if (sqlCrudConfig.IsResultOrder)
					Templates = Templates.OrderBy(item => item.Name).ToList();
				return Templates.Cast<T>().ToList();
			case var cls when cls == typeof(TemplateResourceModel):
				TemplateResources = DataAccess.GetListNotNullable<TemplateResourceModel>(sqlCrudConfig);
				if (sqlCrudConfig.IsResultOrder)
					TemplateResources = TemplateResources.OrderBy(item => item.Name).ToList();
				return TemplateResources.Cast<T>().ToList();
			case var cls when cls == typeof(VersionModel):
				Versions = DataAccess.GetListNotNullable<VersionModel>(sqlCrudConfig);
				if (sqlCrudConfig.IsResultOrder)
					Versions = Versions.OrderByDescending(item => item.Version).ToList();
				return Versions.Cast<T>().ToList();
			case var cls when cls == typeof(WorkShopModel):
				WorkShops = DataAccess.GetListNotNullable<WorkShopModel>(sqlCrudConfig);
				if (sqlCrudConfig.IsResultOrder)
					WorkShops = WorkShops.OrderBy(item => item.Name).ToList();
				return WorkShops.Cast<T>().ToList();
		}
		return new();
	}

	public T? GetItemNullable<T>(object? value) where T : class, new() => DataAccess.GetItemNullable<T>(value);

	public T GetItemNotNullable<T>(object? value) where T : class, new() => DataAccess.GetItemNotNullable<T>(value);

	/// <summary>
	/// List of models SqlTableBase.
	/// </summary>
	/// <returns></returns>
	public List<SqlTableBase> GetTableModels() => new()
	{
		// Data models.
		new ParseResultModel(),
		// Sql models.
		new AccessModel(),
		new AppModel(),
		new BarCodeModel(),
        new BrandModel(),
        new ContragentModel(),
		new DeviceModel(),
		new DeviceTypeModel(),
		new DeviceTypeFkModel(),
		new DeviceScaleFkModel(),
		new LogModel(),
		new LogTypeModel(),
		new NomenclatureModel(),
		new OrderModel(),
		new OrganizationModel(),
		new PackageModel(),
		new PluLabelModel(),
		new PluModel(),
		new PluPackageModel(),
		new PluScaleModel(),
		new PluWeighingModel(),
		new PrinterModel(),
		new PrinterResourceModel(),
		new PrinterTypeModel(),
		new ProductionFacilityModel(),
		new ProductSeriesModel(),
		new ScaleModel(),
		new ScaleScreenShotModel(),
		new TaskModel(),
		new TaskTypeModel(),
		new TemplateModel(),
		new TemplateResourceModel(),
		new VersionModel(),
		new WorkShopModel(),
	};

	/// <summary>
	/// List of types SqlTableBase.
	/// </summary>
	/// <returns></returns>
	public List<Type> GetTableTypes() => new()
	{
		typeof(AccessModel),
		typeof(AppModel),
		typeof(BarCodeModel),
		typeof(BrandModel),
		typeof(ContragentModel),
		typeof(DeviceModel),
		typeof(DeviceTypeModel),
		typeof(DeviceTypeFkModel),
		typeof(DeviceScaleFkModel),
		typeof(LogModel),
		typeof(LogTypeModel),
		typeof(NomenclatureModel),
		typeof(OrderModel),
		typeof(OrganizationModel),
		typeof(PackageModel),
		typeof(PluLabelModel),
		typeof(PluModel),
		typeof(PluPackageModel),
		typeof(PluScaleModel),
		typeof(PluWeighingModel),
		typeof(PrinterModel),
		typeof(PrinterResourceModel),
		typeof(PrinterTypeModel),
		typeof(ProductionFacilityModel),
		typeof(ProductSeriesModel),
		typeof(ScaleModel),
		typeof(ScaleScreenShotModel),
		typeof(TaskModel),
		typeof(TaskTypeModel),
		typeof(TemplateModel),
		typeof(TemplateResourceModel),
		typeof(VersionModel),
		typeof(WorkShopModel),
	};

	public string GetTableModelName<T>() where T : class, new()
	{
		return typeof(T) switch
		{
			var cls when cls == typeof(AccessModel) => nameof(AccessModel),
			var cls when cls == typeof(AppModel) => nameof(AppModel),
			var cls when cls == typeof(BarCodeModel) => nameof(BarCodeModel),
			var cls when cls == typeof(BrandModel) => nameof(BrandModel),
			var cls when cls == typeof(ContragentModel) => nameof(ContragentModel),
			var cls when cls == typeof(DeviceModel) => nameof(DeviceModel),
			var cls when cls == typeof(DeviceTypeModel) => nameof(DeviceTypeModel),
			var cls when cls == typeof(DeviceTypeFkModel) => nameof(DeviceTypeFkModel),
			var cls when cls == typeof(DeviceScaleFkModel) => nameof(DeviceScaleFkModel),
			var cls when cls == typeof(LogModel) => nameof(LogModel),
			var cls when cls == typeof(LogTypeModel) => nameof(LogTypeModel),
			var cls when cls == typeof(NomenclatureModel) => nameof(NomenclatureModel),
			var cls when cls == typeof(OrderModel) => nameof(OrderModel),
			var cls when cls == typeof(OrganizationModel) => nameof(OrganizationModel),
			var cls when cls == typeof(PackageModel) => nameof(PackageModel),
			var cls when cls == typeof(PluLabelModel) => nameof(PluLabelModel),
			var cls when cls == typeof(PluModel) => nameof(PluModel),
			var cls when cls == typeof(PluPackageModel) => nameof(PluPackageModel),
			var cls when cls == typeof(PluScaleModel) => nameof(PluScaleModel),
			var cls when cls == typeof(PluWeighingModel) => nameof(PluWeighingModel),
			var cls when cls == typeof(PrinterModel) => nameof(PrinterModel),
			var cls when cls == typeof(PrinterResourceModel) => nameof(PrinterResourceModel),
			var cls when cls == typeof(PrinterTypeModel) => nameof(PrinterTypeModel),
			var cls when cls == typeof(ProductionFacilityModel) => nameof(ProductionFacilityModel),
			var cls when cls == typeof(ProductSeriesModel) => nameof(ProductSeriesModel),
			var cls when cls == typeof(ScaleModel) => nameof(ScaleModel),
			var cls when cls == typeof(ScaleScreenShotModel) => nameof(ScaleScreenShotModel),
			var cls when cls == typeof(TaskModel) => nameof(TaskModel),
			var cls when cls == typeof(TaskTypeModel) => nameof(TaskTypeModel),
			var cls when cls == typeof(TemplateModel) => nameof(TemplateModel),
			var cls when cls == typeof(TemplateResourceModel) => nameof(TemplateResourceModel),
			var cls when cls == typeof(VersionModel) => nameof(VersionModel),
			var cls when cls == typeof(WorkShopModel) => nameof(WorkShopModel),
			_ => string.Empty
		};
	}

	#endregion
}
