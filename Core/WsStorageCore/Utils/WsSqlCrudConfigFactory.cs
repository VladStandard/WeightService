// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Utils;

public static class WsSqlCrudConfigFactory
{
    #region Public and private methods
    public static WsSqlCrudConfigModel GetCrudAll() => new(new(), WsSqlEnumIsMarked.ShowAll, false, true, false);
    public static WsSqlCrudConfigModel GetCrudActual() => new(new(), WsSqlEnumIsMarked.ShowOnlyActual, false, true, false);
    #endregion
}