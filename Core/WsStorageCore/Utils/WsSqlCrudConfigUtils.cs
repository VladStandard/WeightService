// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Utils;

public static class WsSqlCrudConfigUtils
{
    #region Public and private methods

    public static WsSqlCrudConfigModel GetCrudConfigItem(WsSqlEnumIsMarked isMarked) =>
        new(new List<WsSqlFieldFilterModel>(), isMarked, false, false, false);

    public static WsSqlCrudConfigModel GetCrudConfigSection(WsSqlEnumIsMarked isMarked) =>
        new(new List<WsSqlFieldFilterModel>(), isMarked, true, true, false);

    public static WsSqlCrudConfigModel GetCrudConfigComboBox() =>
        new(new List<WsSqlFieldFilterModel>(), WsSqlEnumIsMarked.ShowAll, false, true, false);

    public static WsSqlCrudConfigModel GetCrudConfig(WsSqlTableBase? itemFilter, string className, List<WsSqlFieldOrderModel> orders,
        WsSqlEnumIsMarked isMarked, bool isShowOnlyTop, bool isAddFieldEmpty, bool isOrder) =>
        GetCrudConfig(WsSqlCrudConfigModel.GetFilters(className, itemFilter), orders, isMarked, isShowOnlyTop, isAddFieldEmpty, isOrder);

    public static WsSqlCrudConfigModel GetCrudConfig(string className, object? value, List<WsSqlFieldOrderModel> orders,
        WsSqlEnumIsMarked isMarked, bool isShowOnlyTop, bool isAddFieldEmpty, bool isOrder) =>
        GetCrudConfig(WsSqlCrudConfigModel.GetFilters(className, value), orders, isMarked, isShowOnlyTop, isAddFieldEmpty, isOrder);

    public static WsSqlCrudConfigModel GetCrudConfig(WsSqlTableBase? itemFilter, string className,
        WsSqlEnumIsMarked isMarked, bool isShowOnlyTop, bool isAddFieldEmpty, bool isOrder) =>
        GetCrudConfig(itemFilter, className, new(), isMarked, isShowOnlyTop, isAddFieldEmpty, isOrder);

    public static WsSqlCrudConfigModel GetCrudConfig(List<WsSqlFieldFilterModel> filters, List<WsSqlFieldOrderModel> orders,
        WsSqlEnumIsMarked isMarked, bool isShowOnlyTop, bool isAddFieldEmpty, bool isOrder) =>
        new(filters, orders, isMarked, isShowOnlyTop, isOrder, false);

    public static WsSqlCrudConfigModel GetCrudConfig(string className, object? value,
        WsSqlEnumIsMarked isMarked, bool isShowOnlyTop = true, bool isAddFieldEmpty = false, bool isOrder = false) =>
        GetCrudConfig(className, value, new(), isMarked, isShowOnlyTop, isAddFieldEmpty, isOrder);

    public static WsSqlCrudConfigModel GetCrudConfig(List<WsSqlFieldFilterModel> filters,
        WsSqlEnumIsMarked isMarked, bool isShowOnlyTop = true, bool isAddFieldEmpty = false, bool isOrder = false) =>
        GetCrudConfig(filters, new List<WsSqlFieldOrderModel>(), isMarked, isShowOnlyTop, isAddFieldEmpty, isOrder);

    public static WsSqlCrudConfigModel GetCrudConfig(List<WsSqlFieldFilterModel> filters, WsSqlFieldOrderModel order,
        WsSqlEnumIsMarked isMarked, bool isShowOnlyTop = true, bool isAddFieldEmpty = false, bool isOrder = false) =>
        GetCrudConfig(filters, new List<WsSqlFieldOrderModel> { order }, isMarked, isShowOnlyTop, isAddFieldEmpty, isOrder);

    public static WsSqlCrudConfigModel GetCrudConfig(WsSqlEnumIsMarked isMarked, bool isShowOnlyTop, bool isAddFieldEmpty = false, bool isOrder = false) =>
        GetCrudConfig(new(), new List<WsSqlFieldOrderModel>(), isMarked, isShowOnlyTop, isAddFieldEmpty, isOrder);

    public static WsSqlCrudConfigModel GetCrudConfig(List<WsSqlFieldOrderModel> orders,
        WsSqlEnumIsMarked isMarked, bool isShowOnlyTop = true, bool isAddFieldEmpty = false, bool isOrder = false) =>
        GetCrudConfig(new(), orders, isMarked, isShowOnlyTop, isAddFieldEmpty, isOrder);

    public static WsSqlCrudConfigModel GetCrudConfig(WsSqlFieldOrderModel order,
        WsSqlEnumIsMarked isMarked, bool isShowOnlyTop = true, bool isAddFieldEmpty = false, bool isOrder = false) =>
        GetCrudConfig(new(), new List<WsSqlFieldOrderModel> { order }, isMarked, isShowOnlyTop, isAddFieldEmpty, isOrder);

    #endregion
}