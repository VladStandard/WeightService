// ReSharper disable InconsistentNaming

namespace WsStorageCore.Common;

public interface IWsSqlViewBaseRepository<T> where T: WsSqlTableBase, new()
{
    public IList<T> GetList(WsSqlCrudConfigModel sqlCrudConfig);
}