namespace Ws.StorageCore.Common;

public interface ISqlTableBaseRepository<T> where T : SqlEntityBase, new()
{
    //IList<T> GetList(SqlCrudConfigModel sqlCrudConfig);
}