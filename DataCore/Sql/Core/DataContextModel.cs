// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using DataCore.Sql.TableScaleModels;
using NHibernate.Criterion;
using static DataCore.Sql.Core.SqlQueries.DbServiceManaging.Tables;

namespace DataCore.Sql.Core;

public class DataContextModel
{
	#region Public and private fields, properties, constructor

	private DataAccessHelper DataAccess { get; } = DataAccessHelper.Instance;
	public List<AccessModel> Accesses { get; set; }
	public List<AppModel> Apps { get; set; }
	public List<BarCodeModel> BarCodes { get; set; }
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

	public List<T> GetListNotNull<T>(SqlTableBase? itemFilter,
		bool isShowMarked = false, bool isShowOnlyTop = false, bool isAddFieldNull = false) where T : SqlTableBase, new()
	{
		List<SqlFieldFilterModel> filters = DataAccess.GetSqlFieldFilterList<T>(itemFilter, nameof(ScaleScreenShotModel.Scale), itemFilter?.IdentityValueId);
		switch (typeof(T))
		{
			case var cls when cls == typeof(AccessModel):
				Accesses = DataAccess.GetListNotNull<AccessModel>(filters, isShowMarked, isShowOnlyTop, isAddFieldNull);
				return Accesses.Cast<T>().ToList();
			case var cls when cls == typeof(AppModel):
				Apps = DataAccess.GetListNotNull<AppModel>(filters, isShowMarked, isShowOnlyTop, isAddFieldNull);
				return Apps.Cast<T>().ToList();
			case var cls when cls == typeof(BarCodeModel):
				BarCodes = DataAccess.GetListNotNull<BarCodeModel>(filters, isShowMarked, isShowOnlyTop, isAddFieldNull);
				return BarCodes.Cast<T>().ToList();
			case var cls when cls == typeof(ContragentModel):
				Contragents = DataAccess.GetListNotNull<ContragentModel>(filters, isShowMarked, isShowOnlyTop, isAddFieldNull);
				return Contragents.Cast<T>().ToList();
			case var cls when cls == typeof(DeviceModel):
				Devices = DataAccess.GetListNotNull<DeviceModel>(filters, isShowMarked, isShowOnlyTop, isAddFieldNull);
				return Devices.Cast<T>().ToList();
			case var cls when cls == typeof(DeviceTypeModel):
				DeviceTypes = DataAccess.GetListNotNull<DeviceTypeModel>(filters, isShowMarked, isShowOnlyTop, isAddFieldNull);
				return DeviceTypes.Cast<T>().ToList();
			case var cls when cls == typeof(DeviceTypeFkModel):
				DeviceTypeFks = DataAccess.GetListNotNull<DeviceTypeFkModel>(filters, isShowMarked, isShowOnlyTop, isAddFieldNull);
				return DeviceTypeFks.Cast<T>().ToList();
			case var cls when cls == typeof(DeviceScaleFkModel):
				DeviceScaleFks = DataAccess.GetListNotNull<DeviceScaleFkModel>(filters, isShowMarked, isShowOnlyTop, isAddFieldNull);
				return DeviceScaleFks.Cast<T>().ToList();
			case var cls when cls == typeof(LogModel):
				Logs = DataAccess.GetListNotNull<LogModel>(filters, isShowMarked, isShowOnlyTop, isAddFieldNull);
				return Logs.Cast<T>().ToList();
			case var cls when cls == typeof(LogTypeModel):
				LogTypes = DataAccess.GetListNotNull<LogTypeModel>(filters, isShowMarked, isShowOnlyTop, isAddFieldNull);
				return LogTypes.Cast<T>().ToList();
			case var cls when cls == typeof(NomenclatureModel):
				Nomenclatures = DataAccess.GetListNotNull<NomenclatureModel>(filters, isShowMarked, isShowOnlyTop, isAddFieldNull);
				return Nomenclatures.Cast<T>().ToList();
			case var cls when cls == typeof(OrderModel):
				Orders = DataAccess.GetListNotNull<OrderModel>(filters, isShowMarked, isShowOnlyTop, isAddFieldNull);
				return Orders.Cast<T>().ToList();
			case var cls when cls == typeof(OrganizationModel):
				Organizations = DataAccess.GetListNotNull<OrganizationModel>(filters, isShowMarked, isShowOnlyTop, isAddFieldNull);
				return Organizations.Cast<T>().ToList();
			case var cls when cls == typeof(PackageModel):
				Packages = DataAccess.GetListNotNull<PackageModel>(filters, isShowMarked, isShowOnlyTop, isAddFieldNull);
				return Packages.Cast<T>().ToList();
			case var cls when cls == typeof(PluLabelModel):
				PluLabels = DataAccess.GetListNotNull<PluLabelModel>(filters, isShowMarked, isShowOnlyTop, isAddFieldNull);
				return PluLabels.Cast<T>().ToList();
			case var cls when cls == typeof(PluModel):
				Plus = DataAccess.GetListNotNull<PluModel>(filters, isShowMarked, isShowOnlyTop, isAddFieldNull);
				return Plus.Cast<T>().ToList();
			case var cls when cls == typeof(PluPackageModel):
				PluPackages = DataAccess.GetListNotNull<PluPackageModel>(filters, isShowMarked, isShowOnlyTop, isAddFieldNull);
				return PluPackages.Cast<T>().ToList();
			case var cls when cls == typeof(PluScaleModel):
				PluScales = DataAccess.GetListNotNull<PluScaleModel>(filters, isShowMarked, isShowOnlyTop, isAddFieldNull);
				return PluScales.Cast<T>().ToList();
			case var cls when cls == typeof(PluWeighingModel):
				PluWeighings = DataAccess.GetListNotNull<PluWeighingModel>(filters, isShowMarked, isShowOnlyTop, isAddFieldNull);
				return PluWeighings.Cast<T>().ToList();
			case var cls when cls == typeof(PrinterModel):
				Printers = DataAccess.GetListNotNull<PrinterModel>(filters, isShowMarked, isShowOnlyTop, isAddFieldNull);
				return Printers.Cast<T>().ToList();
			case var cls when cls == typeof(PrinterResourceModel):
                PrinterResources = DataAccess.GetListNotNull<PrinterResourceModel>(filters, isShowMarked, isShowOnlyTop, isAddFieldNull);
				return PrinterResources.Cast<T>().ToList();
			case var cls when cls == typeof(PrinterTypeModel):
                PrinterTypes = DataAccess.GetListNotNull<PrinterTypeModel>(filters, isShowMarked, isShowOnlyTop, isAddFieldNull);
				return PrinterTypes.Cast<T>().ToList();
			case var cls when cls == typeof(ProductionFacilityModel):
				ProductionFacilities = DataAccess.GetListNotNull<ProductionFacilityModel>(filters, isShowMarked, isShowOnlyTop, isAddFieldNull);
				return ProductionFacilities.Cast<T>().ToList();
			case var cls when cls == typeof(ProductSeriesModel):
				ProductSeries = DataAccess.GetListNotNull<ProductSeriesModel>(filters, isShowMarked, isShowOnlyTop, isAddFieldNull);
				return ProductSeries.Cast<T>().ToList();
			case var cls when cls == typeof(ScaleModel):
				Scales = DataAccess.GetListNotNull<ScaleModel>(filters, isShowMarked, isShowOnlyTop, isAddFieldNull);
				return Scales.Cast<T>().ToList();
			case var cls when cls == typeof(ScaleScreenShotModel):
				ScaleScreenShots = DataAccess.GetListNotNull<ScaleScreenShotModel>(filters, isShowMarked, isShowOnlyTop, isAddFieldNull);
				return ScaleScreenShots.Cast<T>().ToList();
			case var cls when cls == typeof(TaskModel):
				Tasks = DataAccess.GetListNotNull<TaskModel>(filters, isShowMarked, isShowOnlyTop, isAddFieldNull);
				return Tasks.Cast<T>().ToList();
			case var cls when cls == typeof(TaskTypeModel):
				TaskTypes = DataAccess.GetListNotNull<TaskTypeModel>(filters, isShowMarked, isShowOnlyTop, isAddFieldNull);
				return TaskTypes.Cast<T>().ToList();
			case var cls when cls == typeof(TemplateModel):
                Templates = DataAccess.GetListNotNull<TemplateModel>(filters, isShowMarked, isShowOnlyTop, isAddFieldNull);
                return Templates.Cast<T>().ToList();
            case var cls when cls == typeof(TemplateResourceModel):
                TemplateResources = DataAccess.GetListNotNull<TemplateResourceModel>(filters, isShowMarked, isShowOnlyTop, isAddFieldNull);
                return TemplateResources.Cast<T>().ToList();
			case var cls when cls == typeof(VersionModel):
				Versions = DataAccess.GetListNotNull<VersionModel>(filters, isShowMarked, isShowOnlyTop, isAddFieldNull);
				return Versions.Cast<T>().ToList();
			case var cls when cls == typeof(WorkShopModel):
				WorkShops = DataAccess.GetListNotNull<WorkShopModel>(filters, isShowMarked, isShowOnlyTop, isAddFieldNull);
				return WorkShops.Cast<T>().ToList();
		}
		return new();
	}

	public List<T> GetListNotNull<T>(bool isShowMarked = false, bool isShowOnlyTop = false, bool isAddFieldNull = false) where T : SqlTableBase, new() =>
		GetListNotNull<T>(null, isShowMarked, isShowOnlyTop, isAddFieldNull);

	public T? GetItem<T>(Guid? uid) where T : SqlTableBase, new() => DataAccess.GetItem<T>(uid);
	
	public T GetItemNotNull<T>(Guid? uid) where T : SqlTableBase, new() => DataAccess.GetItemNotNull<T>(uid);
	
	public T? GetItem<T>(long? id) where T : SqlTableBase, new() => DataAccess.GetItem<T>(id);

	public T GetItemNotNull<T>(long? id) where T : SqlTableBase, new() => DataAccess.GetItemNotNull<T>(id);

	/// <summary>
	/// List of models SqlTableBase.
	/// </summary>
	/// <returns></returns>
	public List<SqlTableBase> GetSqlTableModels() => new()
	{
		new AccessModel(),
		new AppModel(),
		new BarCodeModel(),
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
	public List<Type> GetSqlTableTypes() => new()
	{
		typeof(AccessModel),
		typeof(AppModel),
		typeof(BarCodeModel),
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
	
	public string GetTableModelName<T>() where T : SqlTableBase, new()
	{
		return typeof(T) switch
		{
			var cls when cls == typeof(AccessModel) => nameof(AccessModel),
			var cls when cls == typeof(AppModel) => nameof(AppModel),
			var cls when cls == typeof(BarCodeModel) => nameof(BarCodeModel),
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
