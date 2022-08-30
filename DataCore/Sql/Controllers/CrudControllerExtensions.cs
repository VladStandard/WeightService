// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;
using DataCore.Protocols;
using DataCore.Sql.Tables;
using NHibernate.Cfg;
using static DataCore.ShareEnums;

namespace DataCore.Sql.Controllers;

public static class CrudControllerExtensions
{
	#region Public and private methods

	public static AppEntity? GetOrCreateNewApp(this CrudController crud, string? appName)
	{
		AppEntity? app = null;
		if (!string.IsNullOrEmpty(appName) && appName is not null)
		{
			SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(new()
				{ new(DbField.Name, DbComparer.Equal, appName), new(DbField.IsMarked, DbComparer.Equal, false) }, 
				null, 0, false, false);
			app = crud.GetItem<AppEntity>(sqlCrudConfig);
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

	public static AppEntity? GetItemApp(this CrudController crud, string? appName)
	{
		AppEntity? app = null;
		if (!string.IsNullOrEmpty(appName) && appName is not null)
		{
			SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(new()
				{ new(DbField.Name, DbComparer.Equal, appName), new(DbField.IsMarked, DbComparer.Equal, false) },
				null, 0, false, false);
			app = crud.GetItem<AppEntity>(sqlCrudConfig);
		}
		return app;
	}

	public static HostEntity? GetItemOrCreateNewHost(this CrudController crud, string? hostName)
	{
		HostEntity? host = null;
		if (!string.IsNullOrEmpty(hostName) && hostName is not null)
		{
			SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(new()
				{ new(DbField.HostName, DbComparer.Equal, hostName), new(DbField.IsMarked, DbComparer.Equal, false) },
				null, 0, false, false);
			host = crud.GetItem<HostEntity>(sqlCrudConfig);
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

	public static HostEntity? GetItemHost(this CrudController crud, string? hostName)
	{
		HostEntity? host = null;
		if (!string.IsNullOrEmpty(hostName) && hostName is not null)
		{
			SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(new()
				{ new(DbField.HostName, DbComparer.Equal, hostName), new(DbField.IsMarked, DbComparer.Equal, false) }, 
				null, 0, false, false);
			host = crud.GetItem<HostEntity>(sqlCrudConfig);
			if (host is not null && !host.EqualsDefault())
			{
				host.AccessDt = DateTime.Now;
				crud.Update(host);
			}
		}
		return host;
	}

	public static List<AccessEntity> GetListAcesses(this CrudController crud, bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(DbField.User), 0, isShowMarked, isShowOnlyTop);
		return crud.GetList<AccessEntity>(sqlCrudConfig);
	}

	public static List<BarCodeEntity> GetListBarCodes(this CrudController crud, bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(DbField.Value), 0, isShowMarked, isShowOnlyTop);
		return crud.GetList<BarCodeEntity>(sqlCrudConfig);
	}

	public static List<BarCodeTypeEntity> GetListBarCodeTypes(this CrudController crud, bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(DbField.Name), 0, isShowMarked, isShowOnlyTop);
		return crud.GetList<BarCodeTypeEntity>(sqlCrudConfig);
	}

	public static List<ContragentEntity> GetListContragents(this CrudController crud, bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(DbField.Name), 0, isShowMarked, isShowOnlyTop);
		return crud.GetList<ContragentEntity>(sqlCrudConfig);

	}

	public static List<HostEntity> GetListHosts(this CrudController crud, bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(DbField.Name), 0, isShowMarked, isShowOnlyTop);
		return crud.GetList<HostEntity>(sqlCrudConfig);
	}

	public static List<HostEntity> GetListHostsFree(this CrudController crud, long? id, bool? isMarked)
	{
		object[] entities = crud.GetItemsNativeAsObjects(SqlQueries.DbScales.Tables.Hosts.GetFreeHosts);
		List<HostEntity> items = new();
		foreach (object? item in entities)
		{
			if (item is object[] { Length: 10 } obj)
			{
				if (long.TryParse(Convert.ToString(obj[0]), out long idOut))
				{
					HostEntity host = new()
					{
						IdentityId = idOut,
						CreateDt = Convert.ToDateTime(obj[1]),
						ChangeDt = Convert.ToDateTime(obj[2]),
						AccessDt = Convert.ToDateTime(obj[3]),
						Name = Convert.ToString(obj[4]),
						Ip = Convert.ToString(obj[5]),
						MacAddress = new(Convert.ToString(obj[6])),
						IsMarked = Convert.ToBoolean(obj[7]),
					};
					if ((id == null || Equals(host.IdentityId, id)) && (isMarked == null || Equals(host.IsMarked, isMarked)))
						items.Add(host);
				}
			}
		}
		return items;
	}

	public static List<HostEntity> GetListHostsBusy(this CrudController crud, long? id, bool? isMarked)
	{
		object[] entities = crud.GetItemsNativeAsObjects(SqlQueries.DbScales.Tables.Hosts.GetBusyHosts);
		List<HostEntity> items = new();
		foreach (object? item in entities)
		{
			if (item is object[] { Length: 12 } obj)
			{
				if (long.TryParse(Convert.ToString(obj[0]), out long idOut))
				{
					HostEntity host = new()
					{
						IdentityId = idOut,
						CreateDt = Convert.ToDateTime(obj[1]),
						ChangeDt = Convert.ToDateTime(obj[2]),
						AccessDt = Convert.ToDateTime(obj[3]),
						Name = Convert.ToString(obj[4]),
						Ip = Convert.ToString(obj[7]),
						MacAddress = new(Convert.ToString(obj[8])),
						IsMarked = Convert.ToBoolean(obj[9]),
					};
					if ((id == null || Equals(host.IdentityId, id)) && (isMarked == null || Equals(host.IsMarked, isMarked)))
						items.Add(host);
				}
			}
		}
		return items;
	}

	public static List<NomenclatureEntity> GetListNomenclatures(this CrudController crud, bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(DbField.Name), 0, isShowMarked, isShowOnlyTop);
		return crud.GetList<NomenclatureEntity>(sqlCrudConfig);
	}

	public static List<PluEntity> GetListPlus(this CrudController crud, bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(DbField.Name), 0, isShowMarked, isShowOnlyTop);
		return crud.GetList<PluEntity>(sqlCrudConfig);
	}

	public static List<PluLabelEntity> GetListPluLabels(this CrudController crud, bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, null, 0, isShowMarked, isShowOnlyTop);
		return crud.GetList<PluLabelEntity>(sqlCrudConfig);
	}

	public static List<PluObsoleteEntity> GetListPluObsoletes(this CrudController crud, bool isShowMarked, bool isShowOnlyTop, TableModel? itemFilter)
	{
		long? scaleId = null;
		if (itemFilter is ScaleEntity scale)
			scaleId = scale.IdentityId;
		List<FieldFilterModel>? filters = null;
		if (scaleId is not null)
			filters = new() { new($"{nameof(PluObsoleteEntity.Scale)}.{DbField.IdentityId}", DbComparer.Equal, scaleId) };
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(filters, new(DbField.GoodsName), 0, isShowMarked, isShowOnlyTop);
		return crud.GetList<PluObsoleteEntity>(sqlCrudConfig);
	}

	public static List<PluScaleEntity> GetListPluScales(this CrudController crud, bool isShowMarked, bool isShowOnlyTop, TableModel? itemFilter)
	{
		long? scaleId = null;
		if (itemFilter is ScaleEntity scale)
			scaleId = scale.IdentityId;
		List<FieldFilterModel>? filters = null;
		if (scaleId is not null)
			filters = new() { new($"{nameof(PluScaleEntity.Scale)}.{DbField.IdentityId}", DbComparer.Equal, scaleId) };
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(filters, null, 0, isShowMarked, isShowOnlyTop);
		return crud.GetList<PluScaleEntity>(sqlCrudConfig);
	}

	public static List<PrinterResourceEntity> GetListPrinterResources(this CrudController crud, bool isShowMarked, bool isShowOnlyTop, TableModel? itemFilter)
	{
		long? printerId = null;
		if (itemFilter is PrinterEntity printer)
			printerId = printer.IdentityId;
		List<FieldFilterModel>? filters = null;
		if (printerId is not null)
			filters = new() { new($"{nameof(PrinterResourceEntity.Printer)}.{DbField.IdentityId}", DbComparer.Equal, printerId) };
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(filters, new(DbField.Description), 0, isShowMarked, isShowOnlyTop);
		return crud.GetList<PrinterResourceEntity>(sqlCrudConfig);
	}

	public static List<PrinterEntity> GetListPrinters(this CrudController crud, bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(DbField.Name), 0, isShowMarked, isShowOnlyTop);
		return crud.GetList<PrinterEntity>(sqlCrudConfig);
	}

	public static List<PrinterTypeEntity> GetListPrinterTypes(this CrudController crud, bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(DbField.Name), 0, isShowMarked, isShowOnlyTop);
		return crud.GetList<PrinterTypeEntity>(sqlCrudConfig);
	}

	public static List<ProductionFacilityEntity> GetListProductionFacilities(this CrudController crud, bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(DbField.Name), 0, isShowMarked, isShowOnlyTop);
		List<ProductionFacilityEntity> result = crud.GetList<ProductionFacilityEntity>(sqlCrudConfig);
		result = result.Where(x => x.IdentityId > 0).ToList();
		return result;
	}

	public static List<ScaleEntity> GetListScales(this CrudController crud, bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(DbField.Description), 0, isShowMarked, isShowOnlyTop);
		return crud.GetList<ScaleEntity>(sqlCrudConfig);
	}

	public static List<TemplateResourceEntity> GetListTemplateResources(this CrudController crud, bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(DbField.Type), 0, isShowMarked, isShowOnlyTop);
		List<TemplateResourceEntity> result = crud.GetList<TemplateResourceEntity>(sqlCrudConfig);
		result = result.OrderBy(x => x.Name).ToList();
		result = result.OrderBy(x => x.Type).ToList();
		return result;
	}

	public static List<TemplateEntity> GetListTemplates(this CrudController crud, bool isShowMarked, bool isShowOnlyTop)
	{
		List<FieldFilterModel>? filters = null;
		//List<TypeEntity<string>>? templateCategories = DataSourceDicsHelper.Instance.GetTemplateCategories();
		//string? templateCategory = templateCategories?.FirstOrDefault()?.Value;
		//if (!string.IsNullOrEmpty(templateCategory))
		//	filters = new() { new(DbField.CategoryId, DbComparer.Equal, templateCategory) };
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(filters, null, 0, isShowMarked, isShowOnlyTop);
		return crud.GetList<TemplateEntity>(sqlCrudConfig);
	}

	public static List<VersionEntity> GetListVersions(this CrudController crud, bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(DbField.Version, DbOrderDirection.Desc), 0, isShowMarked, isShowOnlyTop);
		return crud.GetList<VersionEntity>(sqlCrudConfig);
	}

	public static List<WorkShopEntity> GetListWorkShops(this CrudController crud, bool isShowMarked, bool isShowOnlyTop)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(DbField.Name), 0, isShowMarked, isShowOnlyTop);
		List<WorkShopEntity> result = crud.GetList<WorkShopEntity>(sqlCrudConfig);
		result = result.OrderBy(x => x.ProductionFacility.Name).ToList();
		return result;
	}

	#endregion
}
