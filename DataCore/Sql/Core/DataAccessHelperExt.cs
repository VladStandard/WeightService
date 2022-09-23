// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;
using DataCore.Protocols;
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
            new SqlFieldOrderModel(nameof(BarCodeModel.Value), SqlFieldOrderEnum.Asc), 0, isShowMarked, isShowOnlyTop);
		return dataAccess.GetList<BarCodeModel>(sqlCrudConfig);
	}

	public static List<BarCodeTypeModel> GetListBarCodeTypes(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
            new SqlFieldOrderModel(nameof(BarCodeTypeModel.Name), SqlFieldOrderEnum.Asc), 0, isShowMarked, isShowOnlyTop);
		return dataAccess.GetList<BarCodeTypeModel>(sqlCrudConfig);
	}

	public static List<ContragentModel> GetListContragents(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
            new SqlFieldOrderModel(nameof(ContragentModel.Name), SqlFieldOrderEnum.Asc), 0, isShowMarked, isShowOnlyTop);
		return dataAccess.GetList<ContragentModel>(sqlCrudConfig);

	}

	public static List<HostModel> GetListHosts(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
            new SqlFieldOrderModel(nameof(HostModel.Name), SqlFieldOrderEnum.Asc), 0, isShowMarked, isShowOnlyTop);
		List<HostModel> result = new();
		if (isAddFieldNull)
			result.Add(new() { Name = LocaleCore.Table.FieldNull });
		result.AddRange(dataAccess.GetList<HostModel>(sqlCrudConfig));
		return result;
	}

	public static List<HostModel> GetListHostsFree(this DataAccessHelper dataAccess, long? id, bool? isMarked)
	{
		object[] entities = dataAccess.GetObjects(SqlQueries.DbScales.Tables.Hosts.GetFreeHosts);
		List<HostModel> items = new();
		foreach (object? item in entities)
		{
			if (item is object[] { Length: 10 } obj)
			{
				HostModel host = new()
				{
					CreateDt = Convert.ToDateTime(obj[1]),
					ChangeDt = Convert.ToDateTime(obj[2]),
					AccessDt = Convert.ToDateTime(obj[3]),
					Name = Convert.ToString(obj[4]),
					Ip = Convert.ToString(obj[5]),
					MacAddress = new(Convert.ToString(obj[6])),
					IsMarked = Convert.ToBoolean(obj[7]),
				};
				if ((id == null || Equals(host.Identity.Id, id)) &&
					(isMarked == null || Equals(host.IsMarked, isMarked)))
					items.Add(host);
			}
		}
		return items;
	}

	public static List<HostModel> GetListHostsBusy(this DataAccessHelper dataAccess, long? id, bool? isMarked)
	{
		object[] entities = dataAccess.GetObjects(SqlQueries.DbScales.Tables.Hosts.GetBusyHosts);
		List<HostModel> items = new();
		foreach (object? item in entities)
		{
			if (item is object[] { Length: 12 } obj)
			{
				HostModel host = new()
				{
					CreateDt = Convert.ToDateTime(obj[1]),
					ChangeDt = Convert.ToDateTime(obj[2]),
					AccessDt = Convert.ToDateTime(obj[3]),
					Name = Convert.ToString(obj[4]),
					Ip = Convert.ToString(obj[7]),
					MacAddress = new(Convert.ToString(obj[8])),
					IsMarked = Convert.ToBoolean(obj[9]),
				};
				if ((id == null || Equals(host.Identity.Id, id)) &&
					(isMarked == null || Equals(host.IsMarked, isMarked)))
					items.Add(host);
			}
		}
		return items;
	}

	public static List<NomenclatureModel> GetListNomenclatures(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
            new SqlFieldOrderModel(nameof(NomenclatureModel.Name), SqlFieldOrderEnum.Asc), 0, isShowMarked, isShowOnlyTop);
		List<NomenclatureModel> result = new();
		if (isAddFieldNull)
			result.Add(new() { Name = LocaleCore.Table.FieldNull });
		result.AddRange(dataAccess.GetList<NomenclatureModel>(sqlCrudConfig));
		return result;
	}

	public static List<PluModel> GetListPlus(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
            new SqlFieldOrderModel(nameof(PluModel.Name), SqlFieldOrderEnum.Asc), 0, isShowMarked, isShowOnlyTop);
		List<PluModel> result = new();
		if (isAddFieldNull)
			result.Add(new() { Name = LocaleCore.Table.FieldNull });
		result.AddRange(dataAccess.GetList<PluModel>(sqlCrudConfig));
		return result;
	}

	public static List<PluLabelModel> GetListPluLabels(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(0, isShowMarked, isShowOnlyTop);
		return dataAccess.GetList<PluLabelModel>(sqlCrudConfig);
	}

    public static List<PluScaleModel> GetListPluScales(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop,
		SqlTableBase? itemFilter)
	{
		long? scaleId = null;
		if (itemFilter is ScaleModel scale)
			scaleId = scale.Identity.Id;
		List<SqlFieldFilterModel> filters = new();
		if (scaleId is not null)
			filters = new() { new($"{nameof(PluScaleModel.Scale)}.{nameof(SqlTableBase.IdentityValueId)}", SqlFieldComparerEnum.Equal, scaleId) };
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(filters,
			new List<SqlFieldOrderModel> { new (nameof(PluScaleModel.Plu), SqlFieldOrderEnum.Asc), },
			0, isShowMarked, isShowOnlyTop);
		return dataAccess.GetList<PluScaleModel>(sqlCrudConfig);
	}

	public static List<PluScaleModel> GetListPluScales(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop, long scaleId)
	{
		List<SqlFieldFilterModel> filters = new()
		{
			new($"{nameof(PluScaleModel.Scale)}.{nameof(SqlTableBase.IdentityValueId)}", SqlFieldComparerEnum.Equal, scaleId),
			new(nameof(PluScaleModel.IsActive), SqlFieldComparerEnum.Equal, true),
		};
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(filters, new SqlFieldOrderModel(), 0, isShowMarked, isShowOnlyTop);
		return dataAccess.GetList<PluScaleModel>(sqlCrudConfig);
	}

	public static List<PluPackageModel> GetListPluPackages(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop,
		SqlTableBase? itemFilter)
	{
		Guid? pluUid = null;
		if (itemFilter is PluModel plu)
			pluUid = plu.Identity.Uid;
		List<SqlFieldFilterModel> filters = new();
		if (pluUid is not null)
			filters = new() { new($"{nameof(PluPackageModel.Plu)}.{nameof(SqlTableBase.IdentityValueUid)}", SqlFieldComparerEnum.Equal, pluUid) };
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(filters,
			new List<SqlFieldOrderModel> { new (nameof(PluPackageModel.Plu), SqlFieldOrderEnum.Asc), },
			0, isShowMarked, isShowOnlyTop);
		return dataAccess.GetList<PluPackageModel>(sqlCrudConfig);
	}

	public static List<PrinterResourceModel> GetListPrinterResources(this DataAccessHelper dataAccess, bool isShowMarked,
		bool isShowOnlyTop, SqlTableBase? itemFilter)
	{
		long? printerId = null;
		if (itemFilter is PrinterModel printer)
			printerId = printer.Identity.Id;
		List<SqlFieldFilterModel> filters = new();
		if (printerId is not null)
			filters = new() { new($"{nameof(PrinterResourceModel.Printer)}.{nameof(SqlTableBase.IdentityValueId)}", SqlFieldComparerEnum.Equal, printerId) };
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(filters,
			new SqlFieldOrderModel(nameof(SqlTableBase.Description), SqlFieldOrderEnum.Asc), 0, isShowMarked, isShowOnlyTop);
		return dataAccess.GetList<PrinterResourceModel>(sqlCrudConfig);
	}

	public static List<PrinterModel> GetListPrinters(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
            new SqlFieldOrderModel(nameof(PrinterModel.Name), SqlFieldOrderEnum.Asc), 0, isShowMarked, isShowOnlyTop);
		List<PrinterModel> result = new();
		if (isAddFieldNull)
			result.Add(new() { Name = LocaleCore.Table.FieldNull });
		result.AddRange(dataAccess.GetList<PrinterModel>(sqlCrudConfig));
		return result;
	}

	public static List<PrinterTypeModel> GetListPrinterTypes(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
			new SqlFieldOrderModel(nameof(PrinterTypeModel.Name), SqlFieldOrderEnum.Asc), 0, isShowMarked, isShowOnlyTop);
		return dataAccess.GetList<PrinterTypeModel>(sqlCrudConfig);
	}

	public static List<ProductionFacilityModel> GetListProductionFacilities(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
			new SqlFieldOrderModel(nameof(ProductionFacilityModel.Name), SqlFieldOrderEnum.Asc), 0, isShowMarked, isShowOnlyTop);
		List<ProductionFacilityModel> result = new();
		if (isAddFieldNull)
			result.Add(new() { Name = LocaleCore.Table.FieldNull });
		result.AddRange(dataAccess.GetList<ProductionFacilityModel>(sqlCrudConfig));
		if (!isAddFieldNull)
			result = result.Where(x => x.Identity.Id > 0).ToList();
		return result;
	}

	public static List<ScaleModel> GetListScales(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
			new SqlFieldOrderModel(nameof(SqlTableBase.Description), SqlFieldOrderEnum.Asc), 0, isShowMarked, isShowOnlyTop);
		List<ScaleModel> result = new();
		if (isAddFieldNull)
			result.Add(new() { Description = LocaleCore.Table.FieldNull });
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
			result.Add(new() { Title = LocaleCore.Table.FieldNull });
		result.AddRange(dataAccess.GetList<TemplateModel>(sqlCrudConfig));
		return result;
	}

	public static List<OrganizationModel> GetListOrganizations(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
			new SqlFieldOrderModel(nameof(OrganizationModel.Name), SqlFieldOrderEnum.Asc), 0, isShowMarked, isShowOnlyTop);
		List<OrganizationModel> result = new();
		if (isAddFieldNull)
			result.Add(new() { Name = LocaleCore.Table.FieldNull });
		result.AddRange(dataAccess.GetList<OrganizationModel>(sqlCrudConfig));
		return result;
	}

	public static List<PackageModel> GetListPackages(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
			new SqlFieldOrderModel(nameof(PackageModel.Name), SqlFieldOrderEnum.Asc), 0, isShowMarked, isShowOnlyTop);
		List<PackageModel> result = new();
		if (isAddFieldNull)
			result.Add(new() { Name = LocaleCore.Table.FieldNull });
		result.AddRange(dataAccess.GetList<PackageModel>(sqlCrudConfig));
		return result;
	}

	public static List<VersionModel> GetListVersions(this DataAccessHelper dataAccess)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
			new SqlFieldOrderModel(nameof(VersionModel.Version), SqlFieldOrderEnum.Desc), 0, true, false);
		return dataAccess.GetList<VersionModel>(sqlCrudConfig);
	}

	public static List<WorkShopModel> GetListWorkShops(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		WorkShopModel item = new() { Name = LocaleCore.Table.FieldNull };
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
			new SqlFieldOrderModel(nameof(WorkShopModel.Name), SqlFieldOrderEnum.Asc),
			0, isShowMarked, isShowOnlyTop);
		List<WorkShopModel> result = new();
		if (isAddFieldNull)
			result.Add(item);
		result.AddRange(dataAccess.GetList<WorkShopModel>(sqlCrudConfig));
		result = result.OrderBy(x => x.ProductionFacility.Name).ToList();
		return result;
	}

	#endregion
}
