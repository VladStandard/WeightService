// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.Core;

public static class SqlCrudConfigUtils
{
	#region Public and private methods

	public static SqlCrudConfigModel GetCrudConfigItem(bool isResultShowMarked) => 
		new(new List<SqlFieldFilterModel>(), isResultShowMarked, false, false, false, 1);

	public static SqlCrudConfigModel GetCrudConfigSection(bool isResultShowMarked) =>
		new(new List<SqlFieldFilterModel>(), isResultShowMarked, true, false, true);

	public static SqlCrudConfigModel GetCrudConfigComboBox() =>
		new(new List<SqlFieldFilterModel>(), false, false, true, true);

	public static SqlCrudConfigModel GetCrudConfig(SqlTableBase? itemFilter, string className, List<SqlFieldOrderModel> orders,
		bool isResultShowMarked = false, bool isResultShowOnlyTop = true, bool isResultAddFieldEmpty = false, bool isOrder = false, int resultMaxCount = 0) =>
		GetCrudConfig(SqlCrudConfigModel.GetFilters(className, itemFilter), orders, isResultShowMarked, isResultShowOnlyTop, isResultAddFieldEmpty, isOrder, resultMaxCount);

	public static SqlCrudConfigModel GetCrudConfig(string className, object? value, List<SqlFieldOrderModel> orders,
		bool isResultShowMarked = false, bool isResultShowOnlyTop = true, bool isResultAddFieldEmpty = false, bool isOrder = false, int resultMaxCount = 0) =>
		GetCrudConfig(SqlCrudConfigModel.GetFilters(className, value), orders, isResultShowMarked, isResultShowOnlyTop, isResultAddFieldEmpty, isOrder, resultMaxCount);

	public static SqlCrudConfigModel GetCrudConfig(SqlTableBase? itemFilter, string className, 
		bool isResultShowMarked = false, bool isResultShowOnlyTop = true, bool isResultAddFieldEmpty = false, bool isOrder = false, int resultMaxCount = 0) =>
		GetCrudConfig(itemFilter, className, new(), isResultShowMarked, isResultShowOnlyTop, isResultAddFieldEmpty, isOrder, resultMaxCount);

	public static SqlCrudConfigModel GetCrudConfig(List<SqlFieldFilterModel> filters, List<SqlFieldOrderModel> orders,
		bool isResultShowMarked, bool isResultShowOnlyTop = true, bool isResultAddFieldEmpty = false, bool isOrder = false, int resultMaxCount = 0) =>
		new(filters, orders, isResultShowMarked, isResultShowOnlyTop, isResultAddFieldEmpty, isOrder, resultMaxCount);

	public static SqlCrudConfigModel GetCrudConfig(string className, object? value,
		bool isResultShowMarked, bool isResultShowOnlyTop = true, bool isResultAddFieldEmpty = false, bool isOrder = false, int resultMaxCount = 0) =>
		GetCrudConfig(className, value, new(), isResultShowMarked, isResultShowOnlyTop, isResultAddFieldEmpty, isOrder, resultMaxCount);

	public static SqlCrudConfigModel GetCrudConfig(List<SqlFieldFilterModel> filters,
		bool isResultShowMarked, bool isResultShowOnlyTop = true, bool isResultAddFieldEmpty = false, bool isOrder = false, int resultMaxCount = 0) =>
		GetCrudConfig(filters, new List<SqlFieldOrderModel>(), isResultShowMarked, isResultShowOnlyTop, isResultAddFieldEmpty, isOrder, resultMaxCount);

	public static SqlCrudConfigModel GetCrudConfig(List<SqlFieldFilterModel> filters, SqlFieldOrderModel order,
		bool isResultShowMarked, bool isResultShowOnlyTop = true, bool isResultAddFieldEmpty = false, bool isOrder = false, int resultMaxCount = 0) =>
		GetCrudConfig(filters, new List<SqlFieldOrderModel>() { order }, isResultShowMarked, isResultShowOnlyTop, isResultAddFieldEmpty, isOrder, resultMaxCount);

	public static SqlCrudConfigModel GetCrudConfig(bool isResultShowMarked, bool isResultShowOnlyTop, bool isResultAddFieldEmpty = false, bool isOrder = false, int resultMaxCount = 0) =>
	    GetCrudConfig(new(), new List<SqlFieldOrderModel>(), isResultShowMarked, isResultShowOnlyTop, isResultAddFieldEmpty, isOrder, resultMaxCount);

	public static SqlCrudConfigModel GetCrudConfig(List<SqlFieldOrderModel> orders, 
		bool isResultShowMarked, bool isResultShowOnlyTop = true, bool isResultAddFieldEmpty = false, bool isOrder = false, int resultMaxCount = 0) =>
	    GetCrudConfig(new(), orders, isResultShowMarked, isResultShowOnlyTop, isResultAddFieldEmpty, isOrder, resultMaxCount);

	public static SqlCrudConfigModel GetCrudConfig(SqlFieldOrderModel order, 
		bool isResultShowMarked, bool isResultShowOnlyTop = true, bool isResultAddFieldEmpty = false, bool isOrder = false, int resultMaxCount = 0) =>
	    GetCrudConfig(new(), new List<SqlFieldOrderModel>() { order }, isResultShowMarked, isResultShowOnlyTop, isResultAddFieldEmpty, isOrder, resultMaxCount);

	#endregion
}
