// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Common;

namespace WsStorageCore.Utils;

public static class WsSqlCrudConfigUtils
{
    #region Public and private methods

    public static WsSqlCrudConfigModel GetCrudConfigItem(WsSqlIsMarked isMarked) =>
        new(new List<WsSqlFieldFilterModel>(), isMarked, false, false, false, false);

    public static WsSqlCrudConfigModel GetCrudConfigSection(WsSqlIsMarked isMarked) =>
        new(new List<WsSqlFieldFilterModel>(), isMarked, true, false, true, false);

    public static WsSqlCrudConfigModel GetCrudConfigComboBox() =>
        new(new List<WsSqlFieldFilterModel>(), WsSqlIsMarked.ShowAll, false, true, true, false);

    public static WsSqlCrudConfigModel GetCrudConfig(WsSqlTableBase? itemFilter, string className, List<WsSqlFieldOrderModel> orders,
        WsSqlIsMarked isMarked, bool isShowOnlyTop, bool isAddFieldEmpty, bool isOrder) =>
        GetCrudConfig(WsSqlCrudConfigModel.GetFilters(className, itemFilter), orders, isMarked, isShowOnlyTop, isAddFieldEmpty, isOrder);

    public static WsSqlCrudConfigModel GetCrudConfig(string className, object? value, List<WsSqlFieldOrderModel> orders,
        WsSqlIsMarked isMarked, bool isShowOnlyTop, bool isAddFieldEmpty, bool isOrder) =>
        GetCrudConfig(WsSqlCrudConfigModel.GetFilters(className, value), orders, isMarked, isShowOnlyTop, isAddFieldEmpty, isOrder);

    public static WsSqlCrudConfigModel GetCrudConfig(WsSqlTableBase? itemFilter, string className,
        WsSqlIsMarked isMarked, bool isShowOnlyTop, bool isAddFieldEmpty, bool isOrder) =>
        GetCrudConfig(itemFilter, className, new(), isMarked, isShowOnlyTop, isAddFieldEmpty, isOrder);

    public static WsSqlCrudConfigModel GetCrudConfig(List<WsSqlFieldFilterModel> filters, List<WsSqlFieldOrderModel> orders,
        WsSqlIsMarked isMarked, bool isShowOnlyTop, bool isAddFieldEmpty, bool isOrder) =>
        new(filters, orders, isMarked, isShowOnlyTop, isAddFieldEmpty, isOrder, false);

    public static WsSqlCrudConfigModel GetCrudConfig(string className, object? value,
        WsSqlIsMarked isMarked, bool isShowOnlyTop = true, bool isAddFieldEmpty = false, bool isOrder = false) =>
        GetCrudConfig(className, value, new(), isMarked, isShowOnlyTop, isAddFieldEmpty, isOrder);

    public static WsSqlCrudConfigModel GetCrudConfig(List<WsSqlFieldFilterModel> filters,
        WsSqlIsMarked isMarked, bool isShowOnlyTop = true, bool isAddFieldEmpty = false, bool isOrder = false) =>
        GetCrudConfig(filters, new List<WsSqlFieldOrderModel>(), isMarked, isShowOnlyTop, isAddFieldEmpty, isOrder);

    public static WsSqlCrudConfigModel GetCrudConfig(List<WsSqlFieldFilterModel> filters, WsSqlFieldOrderModel order,
        WsSqlIsMarked isMarked, bool isShowOnlyTop = true, bool isAddFieldEmpty = false, bool isOrder = false) =>
        GetCrudConfig(filters, new List<WsSqlFieldOrderModel> { order }, isMarked, isShowOnlyTop, isAddFieldEmpty, isOrder);

    public static WsSqlCrudConfigModel GetCrudConfig(WsSqlIsMarked isMarked, bool isShowOnlyTop, bool isAddFieldEmpty = false, bool isOrder = false) =>
        GetCrudConfig(new(), new List<WsSqlFieldOrderModel>(), isMarked, isShowOnlyTop, isAddFieldEmpty, isOrder);

    public static WsSqlCrudConfigModel GetCrudConfig(List<WsSqlFieldOrderModel> orders,
        WsSqlIsMarked isMarked, bool isShowOnlyTop = true, bool isAddFieldEmpty = false, bool isOrder = false) =>
        GetCrudConfig(new(), orders, isMarked, isShowOnlyTop, isAddFieldEmpty, isOrder);

    public static WsSqlCrudConfigModel GetCrudConfig(WsSqlFieldOrderModel order,
        WsSqlIsMarked isMarked, bool isShowOnlyTop = true, bool isAddFieldEmpty = false, bool isOrder = false) =>
        GetCrudConfig(new(), new List<WsSqlFieldOrderModel> { order }, isMarked, isShowOnlyTop, isAddFieldEmpty, isOrder);

    #endregion
}