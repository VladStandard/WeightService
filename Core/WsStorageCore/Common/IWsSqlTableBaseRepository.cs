// ReSharper disable InconsistentNaming

namespace WsStorageCore.Common;

public interface IWsSqlTableBaseRepository<T> where T: WsSqlEntityBase, new()
{
    //IList<T> GetList(WsSqlCrudConfigModel sqlCrudConfig);
}