// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.Core;

public static class SqlCrudConfigUtils
{
	#region Public and private fields, properties, constructor

	public static DataAccessHelper DataAccess { get; } = DataAccessHelper.Instance;

	#endregion

	#region Public and private methods

	public static SqlCrudConfigModel GetCrudConfigItem()
	{
		return new(new(), new(), true, false, false, false, 0);
	}

	public static SqlCrudConfigModel GetCrudConfigList(bool isShowMarked = false)
	{
		return new(new(), new(), isShowMarked, true, true, true, 0);
	}

	public static SqlCrudConfigModel GetCrudConfig(SqlTableBase? itemFilter, string className, List<SqlFieldOrderModel> orders,
		bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull, bool isOrder, int maxResults)
	{
		List<SqlFieldFilterModel> filters = itemFilter is null 
			? new()
			: DataAccess.GetFiltersIdentity(className, itemFilter.Identity.Name == SqlFieldIdentityEnum.Uid 
				? itemFilter.IdentityValueUid : itemFilter.IdentityValueId);
		return GetCrudConfig(filters, orders, isShowMarked, isShowOnlyTop, isAddFieldNull, isOrder, maxResults);
	}

	public static SqlCrudConfigModel GetCrudConfig(List<SqlFieldFilterModel> filters, List<SqlFieldOrderModel> orders,
		bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull, bool isOrder, int maxResults)
	{
		maxResults = isShowOnlyTop ? DataAccess.JsonSettingsLocal.SelectTopRowsCount : maxResults;

		switch (isShowMarked)
		{
			case false:
				switch (filters.Any())
				{
					case false:
						filters = DataAccess.GetFilters(false);
						break;
					default:
						filters.AddRange(DataAccess.GetFilters(false));
						break;
				}
				break;
		}

		return new(filters, orders, isShowMarked, isShowOnlyTop, isAddFieldNull, isOrder, maxResults);
	}

	public static SqlCrudConfigModel GetCrudConfig(List<SqlFieldFilterModel> filters,
		bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull = false, bool isOrder = false, int maxResults = 0) =>
		GetCrudConfig(filters, new List<SqlFieldOrderModel>(), isShowMarked, isShowOnlyTop, isAddFieldNull, isOrder, maxResults);

	public static SqlCrudConfigModel GetCrudConfig(List<SqlFieldFilterModel> filters, SqlFieldOrderModel order,
		bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull = false, bool isOrder = false, int maxResults = 0) =>
		GetCrudConfig(filters, new List<SqlFieldOrderModel>() { order }, isShowMarked, isShowOnlyTop, isAddFieldNull, isOrder, maxResults);

	public static SqlCrudConfigModel GetCrudConfig(bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull = false, bool isOrder = false, int maxResults = 0) =>
	    GetCrudConfig(new(), new List<SqlFieldOrderModel>(), isShowMarked, isShowOnlyTop, isAddFieldNull, isOrder, maxResults);

	public static SqlCrudConfigModel GetCrudConfig(List<SqlFieldOrderModel> orders, 
		bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull = false, bool isOrder = false, int maxResults = 0) =>
	    GetCrudConfig(new(), orders, isShowMarked, isShowOnlyTop, isAddFieldNull, isOrder, maxResults);

	public static SqlCrudConfigModel GetCrudConfig(SqlFieldOrderModel order, 
		bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull = false, bool isOrder = false, int maxResults = 0) =>
	    GetCrudConfig(new(), new List<SqlFieldOrderModel>() { order }, isShowMarked, isShowOnlyTop, isAddFieldNull, isOrder, maxResults);

	#endregion
}
