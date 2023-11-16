using Ws.StorageCore.Models;
namespace Ws.StorageCore.Common;

public interface ISqlViewBaseRepository<T> where T : SqlEntityBase, new()
{
    public IList<T> GetList(SqlCrudConfigModel sqlCrudConfig);
}