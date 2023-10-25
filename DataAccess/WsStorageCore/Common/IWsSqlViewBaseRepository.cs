// ReSharper disable InconsistentNaming

namespace WsStorageCore.Common;

public interface IWsSqlViewBaseRepository<T> where T : WsSqlEntityBase, new()
{
    public IList<T> GetList(WsSqlCrudConfigModel sqlCrudConfig);
}