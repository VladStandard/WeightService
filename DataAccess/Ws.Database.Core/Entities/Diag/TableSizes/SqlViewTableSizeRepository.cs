using Ws.Domain.Models.Entities.Diag;

namespace Ws.Database.Core.Entities.Diag.TableSizes;

public sealed class SqlViewTableSizeRepository :  BaseRepository, IGetAll<TableSizeEntity>
{
    public IEnumerable<TableSizeEntity> GetAll() => SqlCoreHelper.GetEnumerable<TableSizeEntity>();
}