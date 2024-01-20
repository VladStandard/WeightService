using Ws.Domain.Models.Entities.Diag;

namespace Ws.StorageCore.Entities.Diag.TableSizes;

public sealed class SqlViewTableSizeRepository
{
    private SqlCoreHelper SqlCore => SqlCoreHelper.Instance;
    
    public IEnumerable<TableSizeEntity> GetEnumerable()
    {
       return SqlCore.GetEnumerable<TableSizeEntity>(new());
    }
}