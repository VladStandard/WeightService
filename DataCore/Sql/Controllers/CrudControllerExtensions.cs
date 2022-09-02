// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Protocols;
using DataCore.Sql.Core;
using DataCore.Sql.Tables;
using static DataCore.ShareEnums;

namespace DataCore.Sql.Controllers;

public static class CrudControllerExtensions
{
	#region Public and private methods

	public static AppModel? GetOrCreateNewApp(this CrudController crud, string? appName)
	{
		AppModel? app = null;
		if (!string.IsNullOrEmpty(appName) && appName is not null)
		{
			SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(new()
				{ new(DbField.Name, DbComparer.Equal, appName), new(DbField.IsMarked, DbComparer.Equal, false) },
				null, 0, false, false);
			app = crud.GetItem<AppModel>(sqlCrudConfig);
			if (app is null || app.EqualsDefault())
			{
				app = new()
				{
					Name = appName,
					CreateDt = DateTime.Now,
					ChangeDt = DateTime.Now,
					IsMarked = false,
				};
				crud.Save(app);
			}
		}
		return app;
	}

	public static AppModel? GetItemApp(this CrudController crud, string? appName)
	{
		AppModel? app = null;
		if (!string.IsNullOrEmpty(appName) && appName is not null)
		{
			SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(new()
				{ new(DbField.Name, DbComparer.Equal, appName), new(DbField.IsMarked, DbComparer.Equal, false) },
				null, 0, false, false);
			app = crud.GetItem<AppModel>(sqlCrudConfig);
		}
		return app;
	}

	public static HostModel? GetItemOrCreateNewHost(this CrudController crud, string? hostName)
	{
		HostModel? host = null;
		if (!string.IsNullOrEmpty(hostName) && hostName is not null)
		{
			SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(new()
				{ new(DbField.HostName, DbComparer.Equal, hostName), new(DbField.IsMarked, DbComparer.Equal, false) },
				null, 0, false, false);
			host = crud.GetItem<HostModel>(sqlCrudConfig);
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
				crud.Save(host);
			}
			else
			{
				host.AccessDt = DateTime.Now;
				crud.Update(host);
			}
		}
		return host;
	}

	public static HostModel? GetItemHost(this CrudController crud, string? hostName)
	{
		HostModel? host = null;
		if (!string.IsNullOrEmpty(hostName) && hostName is not null)
		{
			SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(new()
				{ new(DbField.HostName, DbComparer.Equal, hostName), new(DbField.IsMarked, DbComparer.Equal, false) },
				null, 0, false, false);
			host = crud.GetItem<HostModel>(sqlCrudConfig);
			if (host is not null && !host.EqualsDefault())
			{
				host.AccessDt = DateTime.Now;
				crud.Update(host);
			}
		}
		return host;
	}

	public static List<AccessModel> GetListAcesses(this CrudController crud, bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(DbField.User), 0, isShowMarked, isShowOnlyTop);
		return crud.GetList<AccessModel>(sqlCrudConfig);
	}

	public static List<BarCodeModel> GetListBarCodes(this CrudController crud, bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(DbField.Value), 0, isShowMarked, isShowOnlyTop);
		return crud.GetList<BarCodeModel>(sqlCrudConfig);
	}

	public static List<BarCodeTypeModel> GetListBarCodeTypes(this CrudController crud, bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(DbField.Name), 0, isShowMarked, isShowOnlyTop);
		return crud.GetList<BarCodeTypeModel>(sqlCrudConfig);
	}

	public static List<ContragentModel> GetListContragents(this CrudController crud, bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(DbField.Name), 0, isShowMarked, isShowOnlyTop);
		return crud.GetList<ContragentModel>(sqlCrudConfig);

	}

	public static List<HostModel> GetListHosts(this CrudController crud, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		HostModel item = new() { Name = LocaleCore.Table.FieldNull };
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(DbField.Name), 0, isShowMarked, isShowOnlyTop);
		List<HostModel> result = new();
		if (isAddFieldNull)
			result.Add(item);
		result.AddRange(crud.GetList<HostModel>(sqlCrudConfig));
		return result;
	}

	public static List<HostModel> GetListHostsFree(this CrudController crud, long? id, bool? isMarked)
	{
		object[] entities = crud.GetObjects(SqlQueries.DbScales.Tables.Hosts.GetFreeHosts);
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

	public static List<HostModel> GetListHostsBusy(this CrudController crud, long? id, bool? isMarked)
	{
		object[] entities = crud.GetObjects(SqlQueries.DbScales.Tables.Hosts.GetBusyHosts);
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

	public static List<NomenclatureModel> GetListNomenclatures(this CrudController crud, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		NomenclatureModel item = new() { Name = LocaleCore.Table.FieldNull };
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(DbField.Name), 0, isShowMarked, isShowOnlyTop);
		List<NomenclatureModel> result = new();
		if (isAddFieldNull)
			result.Add(item);
		result.AddRange(crud.GetList<NomenclatureModel>(sqlCrudConfig));
		return result;
	}

	public static List<PluModel> GetListPlus(this CrudController crud, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		PluModel item = new() { Name = LocaleCore.Table.FieldNull };
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(DbField.Name), 0, isShowMarked, isShowOnlyTop);
		List<PluModel> result = new();
		if (isAddFieldNull)
			result.Add(item);
		result.AddRange(crud.GetList<PluModel>(sqlCrudConfig));
		return result;
	}

	public static List<PluLabelModel> GetListPluLabels(this CrudController crud, bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, null, 0, isShowMarked, isShowOnlyTop);
		return crud.GetList<PluLabelModel>(sqlCrudConfig);
	}

	public static List<PluObsoleteModel> GetListPluObsoletes(this CrudController crud, bool isShowMarked, bool isShowOnlyTop, TableModel? itemFilter)
	{
		long? scaleId = null;
		if (itemFilter is ScaleModel scale)
			scaleId = scale.Identity.Id;
		List<FieldFilterModel>? filters = null;
		if (scaleId is not null)
			filters = new() { new($"{nameof(PluObsoleteModel.Scale)}.{DbField.IdentityValueId}", DbComparer.Equal, scaleId) };
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(filters, new(DbField.GoodsName), 0, isShowMarked, isShowOnlyTop);
		return crud.GetList<PluObsoleteModel>(sqlCrudConfig);
	}

	public static List<PluScaleModel> GetListPluScales(this CrudController crud, bool isShowMarked, bool isShowOnlyTop, TableModel? itemFilter)
	{
		long? scaleId = null;
		if (itemFilter is ScaleModel scale)
			scaleId = scale.Identity.Id;
		List<FieldFilterModel>? filters = null;
		if (scaleId is not null)
			filters = new() { new($"{nameof(PluScaleModel.Scale)}.{DbField.IdentityValueId}", DbComparer.Equal, scaleId) };
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(filters, null, 0, isShowMarked, isShowOnlyTop);
		return crud.GetList<PluScaleModel>(sqlCrudConfig);
	}

	public static List<PluScaleModel> GetListPluScales(this CrudController crud, bool isShowMarked, bool isShowOnlyTop, long scaleId)
	{
		List<FieldFilterModel> filters = new() { new($"{nameof(PluScaleModel.Scale)}.{DbField.IdentityValueId}", DbComparer.Equal, scaleId) };
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(filters, null, 0, isShowMarked, isShowOnlyTop);
		return crud.GetList<PluScaleModel>(sqlCrudConfig);
	}

	public static List<PrinterResourceModel> GetListPrinterResources(this CrudController crud, bool isShowMarked, bool isShowOnlyTop, TableModel? itemFilter)
	{
		long? printerId = null;
		if (itemFilter is PrinterModel printer)
			printerId = printer.Identity.Id;
		List<FieldFilterModel>? filters = null;
		if (printerId is not null)
			filters = new() { new($"{nameof(PrinterResourceModel.Printer)}.{DbField.IdentityValueId}", DbComparer.Equal, printerId) };
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(filters, new(DbField.Description), 0, isShowMarked, isShowOnlyTop);
		return crud.GetList<PrinterResourceModel>(sqlCrudConfig);
	}

	public static List<PrinterModel> GetListPrinters(this CrudController crud, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		PrinterModel item = new() { Name = LocaleCore.Table.FieldNull };
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(DbField.Name), 0, isShowMarked, isShowOnlyTop);
		List<PrinterModel> result = new();
		if (isAddFieldNull)
			result.Add(item);
		result.AddRange(crud.GetList<PrinterModel>(sqlCrudConfig));
		return result;
	}

	public static List<PrinterTypeModel> GetListPrinterTypes(this CrudController crud, bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(DbField.Name), 0, isShowMarked, isShowOnlyTop);
		return crud.GetList<PrinterTypeModel>(sqlCrudConfig);
	}

	public static List<ProductionFacilityModel> GetListProductionFacilities(this CrudController crud, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		ProductionFacilityModel item = new() { Name = LocaleCore.Table.FieldNull };
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(DbField.Name), 0, isShowMarked, isShowOnlyTop);
		List<ProductionFacilityModel> result = new();
		if (isAddFieldNull)
			result.Add(item);
		result.AddRange(crud.GetList<ProductionFacilityModel>(sqlCrudConfig));
		if (!isAddFieldNull)
			result = result.Where(x => x.Identity.Id > 0).ToList();
		return result;
	}

	public static List<ScaleModel> GetListScales(this CrudController crud, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		ScaleModel item = new() { Description = LocaleCore.Table.FieldNull };
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(DbField.Description), 0, isShowMarked, isShowOnlyTop);
		List<ScaleModel> result = new();
		if (isAddFieldNull)
			result.Add(item);
		result.AddRange(crud.GetList<ScaleModel>(sqlCrudConfig));
		return result;
	}

	public static List<TemplateResourceModel> GetListTemplateResources(this CrudController crud, bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(DbField.Type), 0, isShowMarked, isShowOnlyTop);
		List<TemplateResourceModel> result = crud.GetList<TemplateResourceModel>(sqlCrudConfig);
		result = result.OrderBy(x => x.Name).ToList();
		result = result.OrderBy(x => x.Type).ToList();
		return result;
	}

	public static List<TemplateModel> GetListTemplates(this CrudController crud, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		TemplateModel item = new() { Title = LocaleCore.Table.FieldNull };
		List<FieldFilterModel>? filters = null;
		//List<TypeModel<string>>? templateCategories = DataSourceDicsHelper.Instance.GetTemplateCategories();
		//string? templateCategory = templateCategories?.FirstOrDefault()?.Value;
		//if (!string.IsNullOrEmpty(templateCategory))
		//	filters = new() { new(DbField.CategoryId, DbComparer.Equal, templateCategory) };
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(filters, new(DbField.Title), 0, isShowMarked, isShowOnlyTop);
		List<TemplateModel> result = new();
		if (isAddFieldNull)
			result.Add(item);
		result.AddRange(crud.GetList<TemplateModel>(sqlCrudConfig));
		return result;
	}

	public static List<VersionModel> GetListVersions(this CrudController crud)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
			null, new(DbField.Version, DbOrderDirection.Desc), 0, true, false);
		return crud.GetList<VersionModel>(sqlCrudConfig);
	}

	public static List<WorkShopModel> GetListWorkShops(this CrudController crud, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
	{
		WorkShopModel item = new() { Name = LocaleCore.Table.FieldNull };
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(DbField.Name), 0, isShowMarked, isShowOnlyTop);
		List<WorkShopModel> result = new();
		if (isAddFieldNull)
			result.Add(item);
		result.AddRange(crud.GetList<WorkShopModel>(sqlCrudConfig));
		result = result.OrderBy(x => x.ProductionFacility.Name).ToList();
		return result;
	}

	#endregion
}
