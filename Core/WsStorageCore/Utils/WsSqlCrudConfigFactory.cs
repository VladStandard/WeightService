// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Utils;

public static class WsSqlCrudConfigFactory
{
    #region Public and private methods

    public static WsSqlCrudConfigModel GetCrudAll() => new()
        { IsMarked = WsSqlEnumIsMarked.ShowAll, SelectTopRowsCount = 0, IsResultOrder = true, IsReadUncommitted = false};
    public static WsSqlCrudConfigModel GetCrudActual() => new()
        { IsMarked = WsSqlEnumIsMarked.ShowOnlyActual, SelectTopRowsCount = 0, IsResultOrder = true, IsReadUncommitted = false};
    
    #endregion
}