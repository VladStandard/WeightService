namespace WsStorageCore.Common;

public interface ITableBaseRepository<T> where T: WsSqlTableBase, new()
{
    List<T> GetList(WsSqlCrudConfigModel sqlCrudConfig);
}