using Ws.Domain.Models.Entities.Diag;

namespace Ws.Database.Core.Entities.Diag.LogWebs;

public class SqlLogWebRepository : BaseRepository, IGetItemByUid<LogWebEntity>
{
    public LogWebEntity GetByUid(Guid uid) => SqlCoreHelper.GetItemById<LogWebEntity>(uid);
    
    public IEnumerable<LogWebEntity> GetList()
    {
        return SqlCoreHelper.GetEnumerable(
            QueryOver.Of<LogWebEntity>().OrderBy(log => log.CreateDt).Desc.Take(500)
        );
    }
}