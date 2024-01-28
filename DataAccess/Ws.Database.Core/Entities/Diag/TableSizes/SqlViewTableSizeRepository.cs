using Ws.Database.Core.Common.Queries;
using Ws.Domain.Models.Entities.Diag;

namespace Ws.Database.Core.Entities.Diag.TableSizes;

public sealed class SqlViewTableSizeRepository : IGetAll<TableSizeEntity>
{
    public IEnumerable<TableSizeEntity> GetAll()
    {
       return SqlCoreHelper.Instance.GetEnumerable<TableSizeEntity>();
    }
}