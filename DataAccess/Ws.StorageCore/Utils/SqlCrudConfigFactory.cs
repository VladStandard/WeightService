using Ws.StorageCore.Common;
using Ws.StorageCore.Models;
namespace Ws.StorageCore.Utils;

public static class SqlCrudConfigFactory
{
    public static SqlCrudConfigModel GetCrudAll() => new()
        { IsMarked = SqlEnumIsMarked.ShowAll, SelectTopRowsCount = 0, IsResultOrder = true, IsReadUncommitted = false};
    public static SqlCrudConfigModel GetCrudActual() => new()
        { IsMarked = SqlEnumIsMarked.ShowOnlyActual, SelectTopRowsCount = 0, IsResultOrder = true, IsReadUncommitted = false};
}