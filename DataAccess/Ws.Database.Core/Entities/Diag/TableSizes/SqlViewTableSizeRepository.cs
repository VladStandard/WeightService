using Ws.Domain.Models.Entities.Diag;

namespace Ws.Database.Core.Entities.Diag.TableSizes;

public sealed class SqlViewTableSizeRepository
{
    public IEnumerable<TableSizeEntity> GetEnumerable()
    {
       return SqlCoreHelper.Instance.GetEnumerable<TableSizeEntity>(new());
    }
}