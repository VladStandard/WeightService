// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Protocols;
using DataCore.Sql.Tables;

namespace DataCore.Sql.Core;

public static class DataAccessHelperExt
{
	#region Public and private methods

	public static AppModel? GetOrCreateNewApp(this DataAccessHelper dataAccess, string? appName)
	{
		AppModel? app = null;
		if (!string.IsNullOrEmpty(appName) && appName is not null)
		{
			SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(new()
				{ new(SqlFieldEnum.Name, SqlFieldComparerEnum.Equal, appName), new(SqlFieldEnum.IsMarked, SqlFieldComparerEnum.Equal, false) },
				null, 0, false, false);
			app = dataAccess.GetItem<AppModel>(sqlCrudConfig);
			if (app is null || app.EqualsDefault())
			{
				app = new()
				{
					Name = appName,
					CreateDt = DateTime.Now,
					ChangeDt = DateTime.Now,
					IsMarked = false,
				};
				dataAccess.Save(app);
			}
		}
		return app;
	}

	public static AppModel? GetItemApp(this DataAccessHelper dataAccess, string? appName)
	{
		AppModel? app = null;
		if (!string.IsNullOrEmpty(appName) && appName is not null)
		{
			SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(new()
				{ new(SqlFieldEnum.Name, SqlFieldComparerEnum.Equal, appName), new(SqlFieldEnum.IsMarked, SqlFieldComparerEnum.Equal, false) },
				null, 0, false, false);
			app = dataAccess.GetItem<AppModel>(sqlCrudConfig);
		}
		return app;
	}

	public static HostModel? GetItemOrCreateNewHost(this DataAccessHelper dataAccess, string? hostName)
	{
		HostModel? host = null;
		if (!string.IsNullOrEmpty(hostName) && hostName is not null)
		{
			SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(new()
				{ new(SqlFieldEnum.HostName, SqlFieldComparerEnum.Equal, hostName), new(SqlFieldEnum.IsMarked, SqlFieldComparerEnum.Equal, false) },
				null, 0, false, false);
			host = dataAccess.GetItem<HostModel>(sqlCrudConfig);
			if (host is null || host.EqualsDefault())
			{
				host = new()
				{
					Name = hostName,
					HostName = hostName,
					CreateDt = DateTime.Now,
					ChangeDt = DateTime.Now,
					IsMarked = false,
					Ip = NetUtils.GetLocalIpAddress(),
					AccessDt = DateTime.Now,
				};
				dataAccess.Save(host);
			}
			else
			{
				host.AccessDt = DateTime.Now;
				dataAccess.Update(host);
			}
		}
		return host;
	}

	public static HostModel? GetItemHost(this DataAccessHelper dataAccess, string? hostName)
	{
		HostModel? host = null;
		if (!string.IsNullOrEmpty(hostName) && hostName is not null)
		{
			SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(new()
				{ new(SqlFieldEnum.HostName, SqlFieldComparerEnum.Equal, hostName), new(SqlFieldEnum.IsMarked, SqlFieldComparerEnum.Equal, false) },
				null, 0, false, false);
			host = dataAccess.GetItem<HostModel>(sqlCrudConfig);
			if (host is not null && !host.EqualsDefault())
			{
				host.AccessDt = DateTime.Now;
				dataAccess.Update(host);
			}
		}
		return host;
	}

	public static List<AccessModel> GetListAcesses(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(SqlFieldEnum.User), 0, isShowMarked, isShowOnlyTop);
		return dataAccess.GetList<AccessModel>(sqlCrudConfig);
	}

	public static List<BarCodeModel> GetListBarCodes(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(SqlFieldEnum.Value), 0, isShowMarked, isShowOnlyTop);
		return dataAccess.GetList<BarCodeModel>(sqlCrudConfig);
	}

	public static List<BarCodeTypeModel> GetListBarCodeTypes(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(SqlFieldEnum.Name), 0, isShowMarked, isShowOnlyTop);
		return dataAccess.GetList<BarCodeTypeModel>(sqlCrudConfig);
	}

	public static List<ContragentModel> GetListContragents(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(SqlFieldEnum.Name), 0, isShowMarked, isShowOnlyTop);
		return dataAccess.GetList<ContragentModel>(sqlCrudConfig);

	}

	public static List<HostModel> GetListHosts(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		HostModel item = new() { Name = LocaleCore.Table.FieldNull };
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(SqlFieldEnum.Name), 0, isShowMarked, isShowOnlyTop);
		List<HostModel> result = new();
		if (isAddFieldNull)
			result.Add(item);
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
		NomenclatureModel item = new() { Name = LocaleCore.Table.FieldNull };
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(SqlFieldEnum.Name), 0, isShowMarked, isShowOnlyTop);
		List<NomenclatureModel> result = new();
		if (isAddFieldNull)
			result.Add(item);
		result.AddRange(dataAccess.GetList<NomenclatureModel>(sqlCrudConfig));
		return result;
	}

	public static List<PluModel> GetListPlus(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		PluModel item = new() { Name = LocaleCore.Table.FieldNull };
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(SqlFieldEnum.Name), 0, isShowMarked, isShowOnlyTop);
		List<PluModel> result = new();
		if (isAddFieldNull)
			result.Add(item);
		result.AddRange(dataAccess.GetList<PluModel>(sqlCrudConfig));
		return result;
	}

	public static List<PluLabelModel> GetListPluLabels(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, null, 0, isShowMarked, isShowOnlyTop);
		return dataAccess.GetList<PluLabelModel>(sqlCrudConfig);
	}

	public static List<PluObsoleteModel> GetListPluObsoletes(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop, Tables.SqlTableBase? itemFilter)
	{
		long? scaleId = null;
		if (itemFilter is ScaleModel scale)
			scaleId = scale.Identity.Id;
		List<SqlFieldFilterModel>? filters = null;
		if (scaleId is not null)
			filters = new() { new($"{nameof(PluObsoleteModel.Scale)}.{SqlFieldEnum.IdentityValueId}", SqlFieldComparerEnum.Equal, scaleId) };
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(filters, new(SqlFieldEnum.GoodsName), 0, isShowMarked, isShowOnlyTop);
		return dataAccess.GetList<PluObsoleteModel>(sqlCrudConfig);
	}

	public static List<PluScaleModel> GetListPluScales(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop, Tables.SqlTableBase? itemFilter)
	{
		long? scaleId = null;
		if (itemFilter is ScaleModel scale)
			scaleId = scale.Identity.Id;
		List<SqlFieldFilterModel>? filters = null;
		if (scaleId is not null)
			filters = new() { new($"{nameof(PluScaleModel.Scale)}.{SqlFieldEnum.IdentityValueId}", SqlFieldComparerEnum.Equal, scaleId) };
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(filters, null, 0, isShowMarked, isShowOnlyTop);
		return dataAccess.GetList<PluScaleModel>(sqlCrudConfig);
	}

	public static List<PluScaleModel> GetListPluScales(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop, long scaleId)
	{
		List<SqlFieldFilterModel> filters = new() { new($"{nameof(PluScaleModel.Scale)}.{SqlFieldEnum.IdentityValueId}", SqlFieldComparerEnum.Equal, scaleId) };
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(filters, null, 0, isShowMarked, isShowOnlyTop);
		return dataAccess.GetList<PluScaleModel>(sqlCrudConfig);
	}

	public static List<PrinterResourceModel> GetListPrinterResources(this DataAccessHelper dataAccess, bool isShowMarked, 
		bool isShowOnlyTop, Tables.SqlTableBase? itemFilter)
	{
		long? printerId = null;
		if (itemFilter is PrinterModel printer)
			printerId = printer.Identity.Id;
		List<SqlFieldFilterModel>? filters = null;
		if (printerId is not null)
			filters = new() { new($"{nameof(PrinterResourceModel.Printer)}.{SqlFieldEnum.IdentityValueId}", SqlFieldComparerEnum.Equal, printerId) };
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(filters, new(SqlFieldEnum.Description), 0, isShowMarked, isShowOnlyTop);
		return dataAccess.GetList<PrinterResourceModel>(sqlCrudConfig);
	}

	public static List<PrinterModel> GetListPrinters(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		PrinterModel item = new() { Name = LocaleCore.Table.FieldNull };
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(SqlFieldEnum.Name), 0, isShowMarked, isShowOnlyTop);
		List<PrinterModel> result = new();
		if (isAddFieldNull)
			result.Add(item);
		result.AddRange(dataAccess.GetList<PrinterModel>(sqlCrudConfig));
		return result;
	}

	public static List<PrinterTypeModel> GetListPrinterTypes(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(SqlFieldEnum.Name), 0, isShowMarked, isShowOnlyTop);
		return dataAccess.GetList<PrinterTypeModel>(sqlCrudConfig);
	}

	public static List<ProductionFacilityModel> GetListProductionFacilities(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		ProductionFacilityModel item = new() { Name = LocaleCore.Table.FieldNull };
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(SqlFieldEnum.Name), 0, isShowMarked, isShowOnlyTop);
		List<ProductionFacilityModel> result = new();
		if (isAddFieldNull)
			result.Add(item);
		result.AddRange(dataAccess.GetList<ProductionFacilityModel>(sqlCrudConfig));
		if (!isAddFieldNull)
			result = result.Where(x => x.Identity.Id > 0).ToList();
		return result;
	}

	public static List<ScaleModel> GetListScales(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		ScaleModel item = new() { Description = LocaleCore.Table.FieldNull };
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(SqlFieldEnum.Description), 0, isShowMarked, isShowOnlyTop);
		List<ScaleModel> result = new();
		if (isAddFieldNull)
			result.Add(item);
		result.AddRange(dataAccess.GetList<ScaleModel>(sqlCrudConfig));
		return result;
	}

	public static List<TemplateResourceModel> GetListTemplateResources(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(SqlFieldEnum.Type), 0, isShowMarked, isShowOnlyTop);
		List<TemplateResourceModel> result = dataAccess.GetList<TemplateResourceModel>(sqlCrudConfig);
		result = result.OrderBy(x => x.Name).ToList();
		result = result.OrderBy(x => x.Type).ToList();
		return result;
	}

	public static List<TemplateModel> GetListTemplates(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		TemplateModel item = new() { Title = LocaleCore.Table.FieldNull };
		List<SqlFieldFilterModel>? filters = null;
		//List<TypeModel<string>>? templateCategories = DataSourceDicsHelper.Instance.GetTemplateCategories();
		//string? templateCategory = templateCategories?.FirstOrDefault()?.Value;
		//if (!string.IsNullOrEmpty(templateCategory))
		//	filters = new() { new(DbField.CategoryId, DbComparer.Equal, templateCategory) };
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(filters, new(SqlFieldEnum.Title), 0, isShowMarked, isShowOnlyTop);
		List<TemplateModel> result = new();
		if (isAddFieldNull)
			result.Add(item);
		result.AddRange(dataAccess.GetList<TemplateModel>(sqlCrudConfig));
		return result;
	}

	public static List<VersionModel> GetListVersions(this DataAccessHelper dataAccess)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
			null, new(SqlFieldEnum.Version, SqlFieldOrderDirectionEnum.Desc), 0, true, false);
		return dataAccess.GetList<VersionModel>(sqlCrudConfig);
	}

	public static List<WorkShopModel> GetListWorkShops(this DataAccessHelper dataAccess, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		WorkShopModel item = new() { Name = LocaleCore.Table.FieldNull };
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(SqlFieldEnum.Name), 0, isShowMarked, isShowOnlyTop);
		List<WorkShopModel> result = new();
		if (isAddFieldNull)
			result.Add(item);
		result.AddRange(dataAccess.GetList<WorkShopModel>(sqlCrudConfig));
		result = result.OrderBy(x => x.ProductionFacility.Name).ToList();
		return result;
	}

	#endregion
}
