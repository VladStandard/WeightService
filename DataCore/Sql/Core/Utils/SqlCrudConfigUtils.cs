// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.Core.Utils;

public static class SqlCrudConfigUtils
{
    #region Public and private methods

    public static SqlCrudConfigModel GetCrudConfigItem(bool isShowMarked) =>
        new(new List<SqlFieldFilterModel>(), isShowMarked, false, false, false, 1);

    public static SqlCrudConfigModel GetCrudConfigSection(bool isShowMarked) =>
        new(new List<SqlFieldFilterModel>(), isShowMarked, true, false, true);

    public static SqlCrudConfigModel GetCrudConfigComboBox() =>
        new(new List<SqlFieldFilterModel>(), false, false, true, true);

    public static SqlCrudConfigModel GetCrudConfig(SqlTableBase? itemFilter, string className, List<SqlFieldOrderModel> orders,
        bool isShowMarked = false, bool isShowOnlyTop = true, bool isAddFieldEmpty = false, bool isOrder = false, int maxCount = 0) =>
        GetCrudConfig(SqlCrudConfigModel.GetFilters(className, itemFilter), orders, isShowMarked, isShowOnlyTop, isAddFieldEmpty, isOrder, maxCount);

    public static SqlCrudConfigModel GetCrudConfig(string className, object? value, List<SqlFieldOrderModel> orders,
        bool isShowMarked = false, bool isShowOnlyTop = true, bool isAddFieldEmpty = false, bool isOrder = false, int maxCount = 0) =>
        GetCrudConfig(SqlCrudConfigModel.GetFilters(className, value), orders, isShowMarked, isShowOnlyTop, isAddFieldEmpty, isOrder, maxCount);

    public static SqlCrudConfigModel GetCrudConfig(SqlTableBase? itemFilter, string className,
        bool isShowMarked = false, bool isShowOnlyTop = true, bool isAddFieldEmpty = false, bool isOrder = false, int maxCount = 0) =>
        GetCrudConfig(itemFilter, className, new(), isShowMarked, isShowOnlyTop, isAddFieldEmpty, isOrder, maxCount);

    public static SqlCrudConfigModel GetCrudConfig(List<SqlFieldFilterModel> filters, List<SqlFieldOrderModel> orders,
        bool isShowMarked, bool isShowOnlyTop = true, bool isAddFieldEmpty = false, bool isOrder = false, int maxCount = 0) =>
        new(filters, orders, isShowMarked, isShowOnlyTop, isAddFieldEmpty, isOrder, maxCount);

    public static SqlCrudConfigModel GetCrudConfig(string className, object? value,
        bool isShowMarked, bool isShowOnlyTop = true, bool isAddFieldEmpty = false, bool isOrder = false, int maxCount = 0) =>
        GetCrudConfig(className, value, new(), isShowMarked, isShowOnlyTop, isAddFieldEmpty, isOrder, maxCount);

    public static SqlCrudConfigModel GetCrudConfig(List<SqlFieldFilterModel> filters,
        bool isShowMarked, bool isShowOnlyTop = true, bool isAddFieldEmpty = false, bool isOrder = false, int maxCount = 0) =>
        GetCrudConfig(filters, new List<SqlFieldOrderModel>(), isShowMarked, isShowOnlyTop, isAddFieldEmpty, isOrder, maxCount);

    public static SqlCrudConfigModel GetCrudConfig(List<SqlFieldFilterModel> filters, SqlFieldOrderModel order,
        bool isShowMarked, bool isShowOnlyTop = true, bool isAddFieldEmpty = false, bool isOrder = false, int maxCount = 0) =>
        GetCrudConfig(filters, new List<SqlFieldOrderModel>() { order }, isShowMarked, isShowOnlyTop, isAddFieldEmpty, isOrder, maxCount);

    public static SqlCrudConfigModel GetCrudConfig(bool isShowMarked, bool isShowOnlyTop, bool isAddFieldEmpty = false, bool isOrder = false, int maxCount = 0) =>
        GetCrudConfig(new(), new List<SqlFieldOrderModel>(), isShowMarked, isShowOnlyTop, isAddFieldEmpty, isOrder, maxCount);

    public static SqlCrudConfigModel GetCrudConfig(List<SqlFieldOrderModel> orders,
        bool isShowMarked, bool isShowOnlyTop = true, bool isAddFieldEmpty = false, bool isOrder = false, int maxCount = 0) =>
        GetCrudConfig(new(), orders, isShowMarked, isShowOnlyTop, isAddFieldEmpty, isOrder, maxCount);

    public static SqlCrudConfigModel GetCrudConfig(SqlFieldOrderModel order,
        bool isShowMarked, bool isShowOnlyTop = true, bool isAddFieldEmpty = false, bool isOrder = false, int maxCount = 0) =>
        GetCrudConfig(new(), new List<SqlFieldOrderModel>() { order }, isShowMarked, isShowOnlyTop, isAddFieldEmpty, isOrder, maxCount);

    #endregion
}
