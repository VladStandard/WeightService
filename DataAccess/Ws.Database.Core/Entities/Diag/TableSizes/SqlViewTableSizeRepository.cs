using Ws.Domain.Models.Entities.Diag;

namespace Ws.Database.Core.Entities.Diag.TableSizes;

public sealed class SqlViewTableSizeRepository
{
    private SqlCoreHelper SqlCore => SqlCoreHelper.Instance;
    
    public IEnumerable<TableSizeEntity> GetEnumerable()
    {
       return SqlCore.GetEnumerable<TableSizeEntity>(new());
    }
}