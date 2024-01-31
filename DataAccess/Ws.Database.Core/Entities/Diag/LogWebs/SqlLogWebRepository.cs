using Ws.Database.Core.Common.Queries;
using Ws.Domain.Models.Entities.Diag;

namespace Ws.Database.Core.Entities.Diag.LogWebs;

public class SqlLogWebRepository : IGetItemByUid<LogWebEntity>
{
    public LogWebEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemById<LogWebEntity>(uid);
    
    public IEnumerable<LogWebEntity> GetList()
    {
        return SqlCoreHelper.Instance.GetEnumerable<LogWebEntity>(
            DetachedCriteria.For<LogWebEntity>()
                .AddOrder(SqlOrder.CreateDtDesc()).SetMaxResults(500)
        );
    }
}