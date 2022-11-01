// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.Core;

public class DataContextModel
{
	#region Public and private fields, properties, constructor

	private DataAccessHelper DataAccess { get; } = DataAccessHelper.Instance;
	public List<AccessModel> Accesses { get; set; }
	public List<BarCodeModel> BarCodes { get; set; }
	public List<ContragentModel> Contragents { get; set; }
	public List<DeviceModel> Devices { get; set; }
	public List<DeviceTypeModel> DeviceTypes { get; set; }
	public List<DeviceTypeFkModel> DeviceTypeFks { get; set; }
	public List<DeviceScaleFkModel> DeviceScaleFks { get; set; }
	public List<NomenclatureModel> Nomenclatures { get; set; }
	public List<OrganizationModel> Organizations { get; set; }
	public List<PackageModel> Packages { get; set; }
	public List<PluModel> Plus { get; set; }
	public List<PluLabelModel> PluLabels { get; set; }
	public List<PluPackageModel> PluPackages { get; set; }
	public List<PluScaleModel> PluScales { get; set; }
	public List<PrinterModel> Printers { get; set; }
	public List<PrinterTypeModel> PrinterTypes { get; set; }
	public List<PrinterResourceModel> PrinterResources { get; set; }
	public List<ProductionFacilityModel> ProductionFacilities { get; set; }
	public List<ScaleModel> Scales { get; set; }
	public List<ScaleScreenShotModel> ScaleScreenShots { get; set; }
	public List<TemplateModel> Templates { get; set; }
    public List<TemplateResourceModel> TemplateResources { get; set; }
	public List<VersionModel> Versions { get; set; }
	public List<WorkShopModel> WorkShops { get; set; }

	public DataContextModel()
	{
		Accesses = new();
		BarCodes = new();
		Contragents = new();
		Devices = new();
		DeviceTypes = new();
		DeviceTypeFks = new();
		DeviceScaleFks = new();
		Nomenclatures = new();
		Organizations = new();
		Packages = new();
		Plus = new();
		PluLabels = new();
		PluPackages = new();
		PluScales = new();
		Printers = new();
		PrinterTypes = new();
		PrinterResources = new();
		ProductionFacilities = new();
		Scales = new();
		ScaleScreenShots = new();
		Templates = new();
		TemplateResources = new();
		Versions = new();
		WorkShops = new();
	}

	#endregion

	#region Public and private methods

	public List<T> GetListNotNull<T>(SqlTableBase? itemFilter,
		bool isShowMarked = false, bool isShowOnlyTop = false, bool isAddFieldNull = false, bool isAddNoActive = false) where T : SqlTableBase, new()
	{
		switch (typeof(T))
		{
			case var cls when cls == typeof(AccessModel):
				Accesses = DataAccess.GetListNotNull<AccessModel>(new(nameof(BarCodeModel.ChangeDt), SqlFieldOrderEnum.Desc), isShowMarked, isShowOnlyTop, isAddFieldNull);
				return Accesses.Cast<T>().ToList();
			case var cls when cls == typeof(BarCodeModel):
				BarCodes = DataAccess.GetListNotNull<BarCodeModel>(isShowMarked, isShowOnlyTop, isAddFieldNull);
				return BarCodes.Cast<T>().ToList();
			case var cls when cls == typeof(ContragentModel):
				Contragents = DataAccess.GetListNotNull<ContragentModel>(isShowMarked, isShowOnlyTop, isAddFieldNull);
				return Contragents.Cast<T>().ToList();
			case var cls when cls == typeof(DeviceModel):
				Devices = DataAccess.GetListNotNull<DeviceModel>(isShowMarked, isShowOnlyTop, isAddFieldNull);
				return Devices.Cast<T>().ToList();
			case var cls when cls == typeof(DeviceTypeModel):
				DeviceTypes = DataAccess.GetListNotNull<DeviceTypeModel>(isShowMarked, isShowOnlyTop, isAddFieldNull);
				return DeviceTypes.Cast<T>().ToList();
			case var cls when cls == typeof(DeviceTypeFkModel):
				DeviceTypeFks = DataAccess.GetListDevicesTypesFks(isShowMarked, isShowOnlyTop, isAddFieldNull);
				return DeviceTypeFks.Cast<T>().ToList();
			case var cls when cls == typeof(DeviceTypeModel):
				DeviceScaleFks = DataAccess.GetListDevicesScalesFks(isShowMarked, isShowOnlyTop, isAddFieldNull);
				return DeviceScaleFks.Cast<T>().ToList();
			case var cls when cls == typeof(NomenclatureModel):
				Nomenclatures = DataAccess.GetListNotNull<NomenclatureModel>(isShowMarked, isShowOnlyTop, isAddFieldNull);
				return DeviceScaleFks.Cast<T>().ToList();
			case var cls when cls == typeof(OrganizationModel):
				Organizations = DataAccess.GetListNotNull<OrganizationModel>(isShowMarked, isShowOnlyTop, isAddFieldNull);
				return Organizations.Cast<T>().ToList();
			case var cls when cls == typeof(PackageModel):
				Packages = DataAccess.GetListNotNull<PackageModel>(isShowMarked, isShowOnlyTop, isAddFieldNull);
				return Packages.Cast<T>().ToList();
			case var cls when cls == typeof(PluModel):
				Plus = DataAccess.GetListNotNull<PluModel>(isShowMarked, isShowOnlyTop, isAddFieldNull);
				return Plus.Cast<T>().ToList();
			case var cls when cls == typeof(PluLabelModel):
				PluLabels = DataAccess.GetListNotNull<PluLabelModel>(isShowMarked, isShowOnlyTop, isAddFieldNull);
				return PluLabels.Cast<T>().ToList();
			case var cls when cls == typeof(PluPackageModel):
				PluPackages = DataAccess.GetListPluPackages(itemFilter, isShowMarked, isShowOnlyTop, isAddFieldNull);
				return PluPackages.Cast<T>().ToList();
			case var cls when cls == typeof(PluScaleModel):
				PluScales = DataAccess.GetListPluScales(itemFilter, isShowMarked, isShowOnlyTop, isAddFieldNull);
				return PluScales.Cast<T>().ToList();
			case var cls when cls == typeof(PrinterModel):
				Printers = DataAccess.GetListNotNull<PrinterModel>(isShowMarked, isShowOnlyTop, isAddFieldNull);
				return Printers.Cast<T>().ToList();
			case var cls when cls == typeof(PrinterTypeModel):
                PrinterTypes = DataAccess.GetListNotNull<PrinterTypeModel>(isShowMarked, isShowOnlyTop, isAddFieldNull);
				return PrinterTypes.Cast<T>().ToList();
			case var cls when cls == typeof(PrinterResourceModel):
                PrinterResources = DataAccess.GetListNotNull<PrinterResourceModel>(isShowMarked, isShowOnlyTop, isAddFieldNull);
				return PrinterResources.Cast<T>().ToList();
			case var cls when cls == typeof(ProductionFacilityModel):
				ProductionFacilities = DataAccess.GetListNotNull<ProductionFacilityModel>(isShowMarked, isShowOnlyTop, isAddFieldNull);
				return ProductionFacilities.Cast<T>().ToList();
			case var cls when cls == typeof(ScaleModel):
				Scales = DataAccess.GetListNotNull<ScaleModel>(new(nameof(ScaleModel.Description), SqlFieldOrderEnum.Asc),
					isShowMarked, isShowOnlyTop, isAddFieldNull);
				return Scales.Cast<T>().ToList();
			case var cls when cls == typeof(ScaleScreenShotModel):
				ScaleScreenShots = DataAccess.GetListScalesScreenShots(itemFilter, isShowMarked, isShowOnlyTop, isAddFieldNull);
				return ScaleScreenShots.Cast<T>().ToList();
            case var cls when cls == typeof(TemplateModel):
                Templates = DataAccess.GetListNotNull<TemplateModel>(new(nameof(TemplateModel.Title), SqlFieldOrderEnum.Asc), isShowMarked, isShowOnlyTop, isAddFieldNull);
                return Templates.Cast<T>().ToList();
            case var cls when cls == typeof(TemplateResourceModel):
                TemplateResources = DataAccess.GetListNotNull<TemplateResourceModel>(new(nameof(TemplateModel.Title), SqlFieldOrderEnum.Asc), isShowMarked, isShowOnlyTop, isAddFieldNull);
                return TemplateResources.Cast<T>().ToList();
			case var cls when cls == typeof(VersionModel):
				Versions = DataAccess.GetListNotNull<VersionModel>(new(nameof(VersionModel.Version), SqlFieldOrderEnum.Desc), isShowMarked, isShowOnlyTop, isAddFieldNull);
				return Versions.Cast<T>().ToList();
			case var cls when cls == typeof(WorkShopModel):
				WorkShops = DataAccess.GetListNotNull<WorkShopModel>(isShowMarked, isShowOnlyTop, isAddFieldNull);
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

	#endregion
}
