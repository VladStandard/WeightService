// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Utils;

public static class WsSqlCrudConfigUtils
{
    #region Public and private methods

    public static WsSqlCrudConfigModel GetCrudConfigItem(bool isShowMarked) =>
        new(new List<WsSqlFieldFilterModel>(), isShowMarked, false, false, false, false);

    public static WsSqlCrudConfigModel GetCrudConfigSection(bool isShowMarked) =>
        new(new List<WsSqlFieldFilterModel>(), isShowMarked, true, false, true, false);

    public static WsSqlCrudConfigModel GetCrudConfigComboBox() =>
        new(new List<WsSqlFieldFilterModel>(), false, false, true, true, false);

    public static WsSqlCrudConfigModel GetCrudConfig(WsSqlTableBase? itemFilter, string className, List<WsSqlFieldOrderModel> orders,
        bool isShowMarked, bool isShowOnlyTop, bool isAddFieldEmpty, bool isOrder) =>
        GetCrudConfig(WsSqlCrudConfigModel.GetFilters(className, itemFilter), orders, isShowMarked, isShowOnlyTop, isAddFieldEmpty, isOrder);

    public static WsSqlCrudConfigModel GetCrudConfig(string className, object? value, List<WsSqlFieldOrderModel> orders,
        bool isShowMarked, bool isShowOnlyTop, bool isAddFieldEmpty, bool isOrder) =>
        GetCrudConfig(WsSqlCrudConfigModel.GetFilters(className, value), orders, isShowMarked, isShowOnlyTop, isAddFieldEmpty, isOrder);

    public static WsSqlCrudConfigModel GetCrudConfig(WsSqlTableBase? itemFilter, string className,
        bool isShowMarked, bool isShowOnlyTop, bool isAddFieldEmpty, bool isOrder) =>
        GetCrudConfig(itemFilter, className, new(), isShowMarked, isShowOnlyTop, isAddFieldEmpty, isOrder);

    public static WsSqlCrudConfigModel GetCrudConfig(List<WsSqlFieldFilterModel> filters, List<WsSqlFieldOrderModel> orders,
        bool isShowMarked, bool isShowOnlyTop, bool isAddFieldEmpty, bool isOrder) =>
        new(filters, orders, isShowMarked, isShowOnlyTop, isAddFieldEmpty, isOrder, false);

    public static WsSqlCrudConfigModel GetCrudConfig(string className, object? value,
        bool isShowMarked, bool isShowOnlyTop = true, bool isAddFieldEmpty = false, bool isOrder = false) =>
        GetCrudConfig(className, value, new(), isShowMarked, isShowOnlyTop, isAddFieldEmpty, isOrder);

    public static WsSqlCrudConfigModel GetCrudConfig(List<WsSqlFieldFilterModel> filters,
        bool isShowMarked, bool isShowOnlyTop = true, bool isAddFieldEmpty = false, bool isOrder = false) =>
        GetCrudConfig(filters, new List<WsSqlFieldOrderModel>(), isShowMarked, isShowOnlyTop, isAddFieldEmpty, isOrder);

    public static WsSqlCrudConfigModel GetCrudConfig(List<WsSqlFieldFilterModel> filters, WsSqlFieldOrderModel order,
        bool isShowMarked, bool isShowOnlyTop = true, bool isAddFieldEmpty = false, bool isOrder = false) =>
        GetCrudConfig(filters, new List<WsSqlFieldOrderModel>() { order }, isShowMarked, isShowOnlyTop, isAddFieldEmpty, isOrder);

    public static WsSqlCrudConfigModel GetCrudConfig(bool isShowMarked, bool isShowOnlyTop, bool isAddFieldEmpty = false, bool isOrder = false) =>
        GetCrudConfig(new(), new List<WsSqlFieldOrderModel>(), isShowMarked, isShowOnlyTop, isAddFieldEmpty, isOrder);

    public static WsSqlCrudConfigModel GetCrudConfig(List<WsSqlFieldOrderModel> orders,
        bool isShowMarked, bool isShowOnlyTop = true, bool isAddFieldEmpty = false, bool isOrder = false) =>
        GetCrudConfig(new(), orders, isShowMarked, isShowOnlyTop, isAddFieldEmpty, isOrder);

    public static WsSqlCrudConfigModel GetCrudConfig(WsSqlFieldOrderModel order,
        bool isShowMarked, bool isShowOnlyTop = true, bool isAddFieldEmpty = false, bool isOrder = false) =>
        GetCrudConfig(new(), new List<WsSqlFieldOrderModel>() { order }, isShowMarked, isShowOnlyTop, isAddFieldEmpty, isOrder);

    #endregion
}