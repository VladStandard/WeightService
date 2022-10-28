// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.Core;

public static partial class DataAccessHelperExt
{
	#region Public and private methods

	public static List<AccessModel> GetListAcesses(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
            new SqlFieldOrderModel(nameof(AccessModel.Name), SqlFieldOrderEnum.Asc), 0, isShowMarked, isShowOnlyTop);
		return dataAccess.GetList<AccessModel>(sqlCrudConfig);
	}

	public static List<BarCodeModel> GetListBarCodes(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
            new SqlFieldOrderModel(nameof(BarCodeModel.ChangeDt), SqlFieldOrderEnum.Desc), 0, isShowMarked, isShowOnlyTop);
		return dataAccess.GetList<BarCodeModel>(sqlCrudConfig);
	}

	//public static List<BarCodeTypeModel> GetListBarCodeTypes(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop)
	//{
	//	SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
 //           new SqlFieldOrderModel(nameof(BarCodeTypeModel.Name), SqlFieldOrderEnum.Asc), 0, isShowMarked, isShowOnlyTop);
	//	return dataAccess.GetList<BarCodeTypeModel>(sqlCrudConfig);
	//}

	public static List<ContragentModel> GetListContragents(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
            new SqlFieldOrderModel(nameof(ContragentModel.Name), SqlFieldOrderEnum.Asc), 0, isShowMarked, isShowOnlyTop);
		return dataAccess.GetList<ContragentModel>(sqlCrudConfig);

	}

	public static List<DeviceModel> GetListDevices(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(0, isShowMarked, isShowOnlyTop);
		List<DeviceModel> result = new();
		if (isAddFieldNull)
			result.Add(dataAccess.GetNewDevice());
		List<DeviceModel> list = dataAccess.GetList<DeviceModel>(sqlCrudConfig);
		result = result.OrderBy(x => x.Name).ToList();
		result.AddRange(list);
		return result; 
	}

	public static List<DeviceTypeModel> GetListDevicesTypes(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(0, isShowMarked, isShowOnlyTop);
		List<DeviceTypeModel> result = new();
		if (isAddFieldNull)
			result.Add(dataAccess.GetNewDeviceType());
		List<DeviceTypeModel> list = dataAccess.GetList<DeviceTypeModel>(sqlCrudConfig);
		result = result.OrderBy(x => x.Name).ToList();
		result.AddRange(list);
		return result; 
	}

	public static List<DeviceTypeFkModel> GetListDevicesTypesFk(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(0, isShowMarked, isShowOnlyTop);
		List<DeviceTypeFkModel> result = new();
		if (isAddFieldNull)
			result.Add(dataAccess.GetNewDeviceTypeFk());
		List<DeviceTypeFkModel> list = dataAccess.GetList<DeviceTypeFkModel>(sqlCrudConfig);
		result = result.OrderBy(x => x.DeviceType.Name).ToList();
		result = result.OrderBy(x => x.Device.Name).ToList();
		result.AddRange(list);
		return result; 
	}

	public static List<DeviceScaleFkModel> GetListDevicesScalesFk(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(0, isShowMarked, isShowOnlyTop);
		List<DeviceScaleFkModel> result = new();
		if (isAddFieldNull)
			result.Add(dataAccess.GetNewDeviceScaleFk());
		List<DeviceScaleFkModel> list = dataAccess.GetList<DeviceScaleFkModel>(sqlCrudConfig);
		result = result.OrderBy(x => x.Scale.Description).ToList();
		result = result.OrderBy(x => x.Device.Name).ToList();
		result.AddRange(list);
		return result; 
	}

	//public static List<HostModel> GetListHosts(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	//{
	//	SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
	//           new SqlFieldOrderModel(nameof(HostModel.Name), SqlFieldOrderEnum.Asc), 0, isShowMarked, isShowOnlyTop);
	//	List<HostModel> result = new();
	//	if (isAddFieldNull)
	//		result.Add(dataAccess.GetNewHost());
	//	result.AddRange(dataAccess.GetList<HostModel>(sqlCrudConfig));
	//	return result;
	//}

	public static List<DeviceTypeFkModel> GetListDevicesTypesFkFree(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		List<DeviceTypeFkModel> deviceTypeFks = dataAccess.GetListDevicesTypesFk(isShowMarked, isShowOnlyTop, isAddFieldNull);
		List<DeviceModel> devices = dataAccess.GetListDevices(isShowMarked, isShowOnlyTop, isAddFieldNull);
		deviceTypeFks = deviceTypeFks.Where(x => !devices.Contains(x.Device)).ToList();
		return deviceTypeFks;
	}
	//public static List<HostModel> GetListHostsFree(this DataAccessHelper dataAccess, long? id, bool? isMarked)
	//{
	//	object[] entities = dataAccess.GetObjects(SqlQueries.DbScales.Tables.Hosts.GetFreeHosts);
	//	List<HostModel> items = new();
	//	foreach (object? item in entities)
	//	{
	//		if (item is object[] { Length: 10 } obj)
	//		{
	//			HostModel host = new()
	//			{
	//				CreateDt = Convert.ToDateTime(obj[1]),
	//				ChangeDt = Convert.ToDateTime(obj[2]),
	//				LoginDt = Convert.ToDateTime(obj[3]),
	//				Name = Convert.ToString(obj[4]),
	//				Ip = Convert.ToString(obj[5]),
	//				MacAddress = new(Convert.ToString(obj[6])),
	//				IsMarked = Convert.ToBoolean(obj[7]),
	//			};
	//			if ((id == null || Equals(host.IdentityValueId, id)) &&
	//				(isMarked == null || Equals(host.IsMarked, isMarked)))
	//				items.Add(host);
	//		}
	//	}
	//	return items;
	//}

	public static List<DeviceTypeFkModel> GetListDevicesTypesFkBusy(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		List<DeviceTypeFkModel> deviceTypeFks = dataAccess.GetListDevicesTypesFk(isShowMarked, isShowOnlyTop, isAddFieldNull);
		List<DeviceModel> devices = dataAccess.GetListDevices(isShowMarked, isShowOnlyTop, isAddFieldNull);
		deviceTypeFks = deviceTypeFks.Where(x => devices.Contains(x.Device)).ToList();
		return deviceTypeFks;
	}
	//public static List<HostModel> GetListHostsBusy(this DataAccessHelper dataAccess, long? id, bool? isMarked)
	//{
	//	object[] entities = dataAccess.GetObjects(SqlQueries.DbScales.Tables.Hosts.GetBusyHosts);
	//	List<HostModel> items = new();
	//	foreach (object? item in entities)
	//	{
	//		if (item is object[] { Length: 12 } obj)
	//		{
	//			HostModel host = new()
	//			{
	//				CreateDt = Convert.ToDateTime(obj[1]),
	//				ChangeDt = Convert.ToDateTime(obj[2]),
	//				LoginDt = Convert.ToDateTime(obj[3]),
	//				Name = Convert.ToString(obj[4]),
	//				Ip = Convert.ToString(obj[7]),
	//				MacAddress = new(Convert.ToString(obj[8])),
	//				IsMarked = Convert.ToBoolean(obj[9]),
	//			};
	//			if ((id == null || Equals(host.IdentityValueId, id)) &&
	//				(isMarked == null || Equals(host.IsMarked, isMarked)))
	//				items.Add(host);
	//		}
	//	}
	//	return items;
	//}

	public static List<NomenclatureModel> GetListNomenclatures(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
            new SqlFieldOrderModel(nameof(NomenclatureModel.Name), SqlFieldOrderEnum.Asc), 0, isShowMarked, isShowOnlyTop);
		List<NomenclatureModel> result = new();
		if (isAddFieldNull)
			result.Add(dataAccess.GetNewNomenclature());
		result.AddRange(dataAccess.GetList<NomenclatureModel>(sqlCrudConfig));
		return result;
	}

	public static List<PluModel> GetListPlus(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
            new SqlFieldOrderModel(nameof(PluModel.Name), SqlFieldOrderEnum.Asc), 0, isShowMarked, isShowOnlyTop);
		List<PluModel> result = new();
		if (isAddFieldNull)
			result.Add(dataAccess.GetNewPlu());
		result.AddRange(dataAccess.GetList<PluModel>(sqlCrudConfig));
		return result;
	}

	public static List<PluLabelModel> GetListPluLabels(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(0, isShowMarked, isShowOnlyTop);
		sqlCrudConfig.Orders.Add(new SqlFieldOrderModel(nameof(PluWeighingModel.ChangeDt), SqlFieldOrderEnum.Desc));
		return dataAccess.GetList<PluLabelModel>(sqlCrudConfig);
	}

    public static List<PluScaleModel> GetListPluScales(this DataAccessHelper dataAccess, SqlTableBase? itemFilter, 
	    bool isShowMarked, bool isShowOnlyTop, bool isShowAll)
	{
		List<SqlFieldFilterModel> filters = new();
		if (itemFilter is not null && !itemFilter.EqualsDefault() && !itemFilter.IdentityIsNew)
		{
			if (itemFilter is ScaleModel scale)
				filters = new()
				{
					new($"{nameof(PluScaleModel.Scale)}.{nameof(SqlTableBase.IdentityValueId)}", 
						SqlFieldComparerEnum.Equal, scale.IdentityValueId)
				};
			if (!isShowAll)
				filters.Add(new(nameof(PluScaleModel.IsActive), SqlFieldComparerEnum.Equal, true));
		}
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(filters,
			0, isShowMarked, isShowOnlyTop);
		List<PluScaleModel> result = dataAccess.GetList<PluScaleModel>(sqlCrudConfig);
		result = result.OrderBy(x => x.Plu.Name).ToList();
		return result;
	}

    public static List<ScaleScreenShotModel> GetListScalesScreenShots(this DataAccessHelper dataAccess, SqlTableBase? itemFilter, 
	    bool isShowMarked, bool isShowOnlyTop, bool isShowAll)
	{
		List<SqlFieldFilterModel> filters = new();
		if (itemFilter is not null && !itemFilter.EqualsDefault() && !itemFilter.IdentityIsNew)
		{
			if (itemFilter is ScaleModel scale)
				filters = new()
				{
					new($"{nameof(PluScaleModel.Scale)}.{nameof(SqlTableBase.IdentityValueId)}", 
						SqlFieldComparerEnum.Equal, scale.IdentityValueId)
				};
			if (!isShowAll)
				filters.Add(new(nameof(PluScaleModel.IsActive), SqlFieldComparerEnum.Equal, true));
		}
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(filters,
			0, isShowMarked, isShowOnlyTop);
		List<ScaleScreenShotModel> result = dataAccess.GetList<ScaleScreenShotModel>(sqlCrudConfig);
		result = result.OrderByDescending(x => x.CreateDt).ToList();
		return result;
	}

	public static List<PluPackageModel> GetListPluPackages(this DataAccessHelper dataAccess, 
		SqlTableBase? itemFilter, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		List<PluPackageModel> result = new();
        if (isAddFieldNull)
            result.Add(dataAccess.GetNewPluPackage());
		List<SqlFieldFilterModel> filters = new();
		if (itemFilter is null) return result;
        if (itemFilter.EqualsDefault())
            return result;
        if (itemFilter is PluModel plu)
        {
            if (plu.EqualsDefault())
                return result;
            if (plu.IdentityIsNew)
                return result;
            if (plu.Equals(dataAccess.GetNewPlu()))
                return result;
        }

		Guid? pluUid = null;
		if (itemFilter is PluModel plu2)
			pluUid = plu2.IdentityValueUid;
		if (pluUid is not null)
			filters = new() { new($"{nameof(PluPackageModel.Plu)}.{nameof(SqlTableBase.IdentityValueUid)}", SqlFieldComparerEnum.Equal, pluUid) };
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(filters,
			new List<SqlFieldOrderModel> { new (nameof(PluPackageModel.Plu), SqlFieldOrderEnum.Asc), },
			0, isShowMarked, isShowOnlyTop);

		result.AddRange(dataAccess.GetList<PluPackageModel>(sqlCrudConfig));
		result = result.OrderBy(x => x.Package.Name).ToList();
		result = result.OrderBy(x => x.Plu.Number).ToList();
		return result;
	}

    public static List<PluPackageModel> GetListPluPackages(this DataAccessHelper dataAccess,
        bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
    {
        List<PluPackageModel> result = new();
        if (isAddFieldNull)
            result.Add(dataAccess.GetNewPluPackage());
        List<SqlFieldFilterModel> filters = new();

        SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(filters,
            new List<SqlFieldOrderModel> { new(nameof(PluPackageModel.Plu), SqlFieldOrderEnum.Asc), },
            0, isShowMarked, isShowOnlyTop);

        result.AddRange(dataAccess.GetList<PluPackageModel>(sqlCrudConfig));
        result = result.OrderBy(x => x.Package.Name).ToList();
        result = result.OrderBy(x => x.Plu.Number).ToList();
		return result;
    }
    
    public static List<PluWeighingModel> GetListPluWeighings(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(0, isShowMarked, isShowOnlyTop);
		sqlCrudConfig.Orders.Add(new SqlFieldOrderModel(nameof(PluWeighingModel.ChangeDt), SqlFieldOrderEnum.Desc));
		return dataAccess.GetList<PluWeighingModel>(sqlCrudConfig);
	}

	public static List<PrinterResourceModel> GetListPrinterResources(this DataAccessHelper dataAccess, SqlTableBase? itemFilter,
		bool isShowMarked, bool isShowOnlyTop)
	{
		long? printerId = null;
		if (itemFilter is PrinterModel printer)
			printerId = printer.IdentityValueId;
		List<SqlFieldFilterModel> filters = new();
		if (printerId is not null)
			filters = new() { new($"{nameof(PrinterResourceModel.Printer)}.{nameof(SqlTableBase.IdentityValueId)}", SqlFieldComparerEnum.Equal, printerId) };
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(filters,
			new SqlFieldOrderModel(nameof(SqlTableBase.Description), SqlFieldOrderEnum.Asc), 0, isShowMarked, isShowOnlyTop);
		return dataAccess.GetList<PrinterResourceModel>(sqlCrudConfig);
	}

	public static List<PrinterModel> GetListPrinters(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop, 
		bool isAddFieldNull)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
            new SqlFieldOrderModel(nameof(PrinterModel.Name), SqlFieldOrderEnum.Asc), 0, isShowMarked, isShowOnlyTop);
		List<PrinterModel> result = new();
		if (isAddFieldNull)
			result.Add(dataAccess.GetNewPrinter());
		result.AddRange(dataAccess.GetList<PrinterModel>(sqlCrudConfig));
		return result;
	}

	public static List<PrinterTypeModel> GetListPrinterTypes(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
			new SqlFieldOrderModel(nameof(PrinterTypeModel.Name), SqlFieldOrderEnum.Asc), 0, isShowMarked, isShowOnlyTop);
		return dataAccess.GetList<PrinterTypeModel>(sqlCrudConfig);
	}

	//public static List<ProductionFacilityModel> GetListAreas(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop, 
	//	bool isAddFieldNull)
	//{
	//	SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
	//		new SqlFieldOrderModel(nameof(ProductionFacilityModel.Name), SqlFieldOrderEnum.Asc), 
	//		0, isShowMarked, isShowOnlyTop);
	//	List<ProductionFacilityModel> result = new();
	//	if (isAddFieldNull)
	//		result.Add(dataAccess.GetNewProductionFacility());
	//	result.AddRange(dataAccess.GetList<ProductionFacilityModel>(sqlCrudConfig));
	//	return result;
	//}

	public static List<ProductionFacilityModel> GetListProductionFacilities(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
			new SqlFieldOrderModel(nameof(ProductionFacilityModel.Name), SqlFieldOrderEnum.Asc), 
			0, isShowMarked, isShowOnlyTop);
		List<ProductionFacilityModel> result = new();
		if (isAddFieldNull)
			result.Add(dataAccess.GetNewProductionFacility());
		result.AddRange(dataAccess.GetList<ProductionFacilityModel>(sqlCrudConfig));
		//if (!isAddFieldNull)
		//	result = result.Where(x => x.IdentityValueId > 0).ToList();
		return result;
	}

	public static List<ScaleModel> GetListScales(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
			new SqlFieldOrderModel(nameof(SqlTableBase.Description), SqlFieldOrderEnum.Asc), 0, isShowMarked, isShowOnlyTop);
		List<ScaleModel> result = new();
		if (isAddFieldNull)
			result.Add(dataAccess.GetNewScale());
		result.AddRange(dataAccess.GetList<ScaleModel>(sqlCrudConfig));
		return result;
	}

	public static List<TemplateResourceModel> GetListTemplateResources(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
			new SqlFieldOrderModel(nameof(TemplateResourceModel.Type), SqlFieldOrderEnum.Asc), 0, isShowMarked, isShowOnlyTop);
		List<TemplateResourceModel> result = dataAccess.GetList<TemplateResourceModel>(sqlCrudConfig);
		result = result.OrderBy(x => x.Name).ToList();
		result = result.OrderBy(x => x.Type).ToList();
		return result;
	}

	public static List<TemplateModel> GetListTemplates(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
			new SqlFieldOrderModel(nameof(TemplateModel.Title), SqlFieldOrderEnum.Asc), 0, isShowMarked, isShowOnlyTop);
		List<TemplateModel> result = new();
		if (isAddFieldNull)
			result.Add(dataAccess.GetNewTemplate());
		result.AddRange(dataAccess.GetList<TemplateModel>(sqlCrudConfig));
		return result;
	}

	public static List<OrganizationModel> GetListOrganizations(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop, 
		bool isAddFieldNull)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
			new SqlFieldOrderModel(nameof(OrganizationModel.Name), SqlFieldOrderEnum.Asc), 0, isShowMarked, isShowOnlyTop);
		List<OrganizationModel> result = new();
		if (isAddFieldNull)
			result.Add(dataAccess.GetNewOrganization());
		result.AddRange(dataAccess.GetList<OrganizationModel>(sqlCrudConfig));
		return result;
	}

	public static List<PackageModel> GetListPackages(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop, 
		bool isAddFieldNull)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
			new SqlFieldOrderModel(nameof(PackageModel.Name), SqlFieldOrderEnum.Asc), 0, isShowMarked, isShowOnlyTop);
		List<PackageModel> result = new();
		if (isAddFieldNull)
			result.Add(dataAccess.GetNewPackage());
		result.AddRange(dataAccess.GetList<PackageModel>(sqlCrudConfig));
		return result;
	}

	public static List<VersionModel> GetListVersions(this DataAccessHelper dataAccess)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
			new SqlFieldOrderModel(nameof(VersionModel.Version), SqlFieldOrderEnum.Desc), 0, true, false);
		return dataAccess.GetList<VersionModel>(sqlCrudConfig);
	}

	public static List<WorkShopModel> GetListWorkShops(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop, 
		bool isAddFieldNull)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
			new SqlFieldOrderModel(nameof(WorkShopModel.Name), SqlFieldOrderEnum.Asc),
			0, isShowMarked, isShowOnlyTop);
		List<WorkShopModel> result = new();
		if (isAddFieldNull)
			result.Add(dataAccess.GetNewWorkShop());
		result.AddRange(dataAccess.GetList<WorkShopModel>(sqlCrudConfig));
		result = result.OrderBy(x => x.ProductionFacility.Name).ToList();
		return result;
	}

	#endregion
}
