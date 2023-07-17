namespace WsStorageCore.Common;

public interface IViewBaseRepository<T> where T: WsSqlTableBase, new()
{
    List<T> GetList(WsSqlCrudConfigModel sqlCrudConfig);
}