namespace WsStorageCore.Utils;

public static class WsSqlCrudConfigFactory
{
    public static WsSqlCrudConfigModel GetCrudAll() => new()
        { IsMarked = WsSqlEnumIsMarked.ShowAll, SelectTopRowsCount = 0, IsResultOrder = true, IsReadUncommitted = false};
    public static WsSqlCrudConfigModel GetCrudActual() => new()
        { IsMarked = WsSqlEnumIsMarked.ShowOnlyActual, SelectTopRowsCount = 0, IsResultOrder = true, IsReadUncommitted = false};
}