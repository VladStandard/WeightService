namespace WsStorageCore.Common;

public interface ISqlViewBaseRepository<T> where T : SqlEntityBase, new()
{
    public IList<T> GetList(SqlCrudConfigModel sqlCrudConfig);
}