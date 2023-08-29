// ReSharper disable InconsistentNaming

namespace WsStorageCore.Common;

public interface IWsSqlTableBaseRepository<T> where T: WsSqlTableBase, new()
{
    //IList<T> GetList(WsSqlCrudConfigModel sqlCrudConfig);
}