// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.Core.Utils;

public static class WsSqlCrudConfigUtils
{
    #region Public and private methods

    public static SqlCrudConfigModel GetCrudConfigItem(bool isShowMarked) =>
        new(new List<SqlFieldFilterModel>(), isShowMarked, false, false, false, false);

    public static SqlCrudConfigModel GetCrudConfigSection(bool isShowMarked) =>
        new(new List<SqlFieldFilterModel>(), isShowMarked, true, false, true, false);

    public static SqlCrudConfigModel GetCrudConfigComboBox() =>
        new(new List<SqlFieldFilterModel>(), false, false, true, true, false);

    public static SqlCrudConfigModel GetCrudConfig(SqlTableBase? itemFilter, string className, List<SqlFieldOrderModel> orders,
        bool isShowMarked, bool isShowOnlyTop, bool isAddFieldEmpty, bool isOrder) =>
        GetCrudConfig(SqlCrudConfigModel.GetFilters(className, itemFilter), orders, isShowMarked, isShowOnlyTop, isAddFieldEmpty, isOrder);

    public static SqlCrudConfigModel GetCrudConfig(string className, object? value, List<SqlFieldOrderModel> orders,
        bool isShowMarked, bool isShowOnlyTop, bool isAddFieldEmpty, bool isOrder) =>
        GetCrudConfig(SqlCrudConfigModel.GetFilters(className, value), orders, isShowMarked, isShowOnlyTop, isAddFieldEmpty, isOrder);

    public static SqlCrudConfigModel GetCrudConfig(SqlTableBase? itemFilter, string className,
        bool isShowMarked, bool isShowOnlyTop, bool isAddFieldEmpty, bool isOrder) =>
        GetCrudConfig(itemFilter, className, new(), isShowMarked, isShowOnlyTop, isAddFieldEmpty, isOrder);

    public static SqlCrudConfigModel GetCrudConfig(List<SqlFieldFilterModel> filters, List<SqlFieldOrderModel> orders,
        bool isShowMarked, bool isShowOnlyTop, bool isAddFieldEmpty, bool isOrder) =>
        new(filters, orders, isShowMarked, isShowOnlyTop, isAddFieldEmpty, isOrder, false);

    public static SqlCrudConfigModel GetCrudConfig(string className, object? value,
        bool isShowMarked, bool isShowOnlyTop = true, bool isAddFieldEmpty = false, bool isOrder = false) =>
        GetCrudConfig(className, value, new(), isShowMarked, isShowOnlyTop, isAddFieldEmpty, isOrder);

    public static SqlCrudConfigModel GetCrudConfig(List<SqlFieldFilterModel> filters,
        bool isShowMarked, bool isShowOnlyTop = true, bool isAddFieldEmpty = false, bool isOrder = false) =>
        GetCrudConfig(filters, new List<SqlFieldOrderModel>(), isShowMarked, isShowOnlyTop, isAddFieldEmpty, isOrder);

    public static SqlCrudConfigModel GetCrudConfig(List<SqlFieldFilterModel> filters, SqlFieldOrderModel order,
        bool isShowMarked, bool isShowOnlyTop = true, bool isAddFieldEmpty = false, bool isOrder = false) =>
        GetCrudConfig(filters, new List<SqlFieldOrderModel>() { order }, isShowMarked, isShowOnlyTop, isAddFieldEmpty, isOrder);

    public static SqlCrudConfigModel GetCrudConfig(bool isShowMarked, bool isShowOnlyTop, bool isAddFieldEmpty = false, bool isOrder = false) =>
        GetCrudConfig(new(), new List<SqlFieldOrderModel>(), isShowMarked, isShowOnlyTop, isAddFieldEmpty, isOrder);

    public static SqlCrudConfigModel GetCrudConfig(List<SqlFieldOrderModel> orders,
        bool isShowMarked, bool isShowOnlyTop = true, bool isAddFieldEmpty = false, bool isOrder = false) =>
        GetCrudConfig(new(), orders, isShowMarked, isShowOnlyTop, isAddFieldEmpty, isOrder);

    public static SqlCrudConfigModel GetCrudConfig(SqlFieldOrderModel order,
        bool isShowMarked, bool isShowOnlyTop = true, bool isAddFieldEmpty = false, bool isOrder = false) =>
        GetCrudConfig(new(), new List<SqlFieldOrderModel>() { order }, isShowMarked, isShowOnlyTop, isAddFieldEmpty, isOrder);

    #endregion
}