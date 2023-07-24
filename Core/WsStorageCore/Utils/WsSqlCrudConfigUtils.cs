// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Utils;

public static class WsSqlCrudConfigUtils
{
    #region Public and private methods
    
    private static WsSqlCrudConfigModel GetCrudConfig(List<WsSqlFieldFilterModel> filters, List<WsSqlFieldOrderModel> orders,
        WsSqlEnumIsMarked isMarked, bool isShowOnlyTop, bool isOrder) =>
        new(filters, orders, isMarked, isShowOnlyTop, isOrder, false);
    
    public static WsSqlCrudConfigModel GetCrudConfigSection(WsSqlEnumIsMarked isMarked) =>
        new(new(), isMarked, true, true, false);

    public static WsSqlCrudConfigModel GetCrudConfigComboBox() =>
        new(new(), WsSqlEnumIsMarked.ShowAll, false, true, false);
    
    public static WsSqlCrudConfigModel GetCrudConfig(WsSqlTableBase? itemFilter, string className,
        WsSqlEnumIsMarked isMarked, bool isShowOnlyTop, bool isOrder) =>
        GetCrudConfig(WsSqlCrudConfigModel.GetFilters(className, itemFilter), new List<WsSqlFieldOrderModel>(), isMarked, isShowOnlyTop, isOrder);

    public static WsSqlCrudConfigModel GetCrudConfig(string className, object? value,
        WsSqlEnumIsMarked isMarked, bool isShowOnlyTop = true, bool isOrder = false) =>
        GetCrudConfig(WsSqlCrudConfigModel.GetFilters(className, value), new List<WsSqlFieldOrderModel>(), isMarked, isShowOnlyTop, isOrder);

    public static WsSqlCrudConfigModel GetCrudConfig(List<WsSqlFieldFilterModel> filters,
        WsSqlEnumIsMarked isMarked, bool isShowOnlyTop = true, bool isOrder = false) =>
        GetCrudConfig(filters, new List<WsSqlFieldOrderModel>(), isMarked, isShowOnlyTop, isOrder);

    public static WsSqlCrudConfigModel GetCrudConfig(List<WsSqlFieldFilterModel> filters, WsSqlFieldOrderModel order,
        WsSqlEnumIsMarked isMarked, bool isShowOnlyTop = true, bool isOrder = false) =>
        GetCrudConfig(filters, new List<WsSqlFieldOrderModel> { order }, isMarked, isShowOnlyTop, isOrder);

    #endregion
}