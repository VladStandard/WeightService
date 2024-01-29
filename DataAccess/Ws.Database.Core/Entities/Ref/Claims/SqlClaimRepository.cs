using Ws.Database.Core.Common.Queries;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.Claims;

public sealed class SqlClaimRepository : IGetItemByUid<ClaimEntity>, IGetAll<ClaimEntity>
{
    public ClaimEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemById<ClaimEntity>(uid);
    
    public IEnumerable<ClaimEntity> GetAll()
    {
        DetachedCriteria criteria = DetachedCriteria.For<ClaimEntity>()
            .AddOrder(SqlOrder.NameAsc());
        return SqlCoreHelper.Instance.GetEnumerable<ClaimEntity>(criteria);
    }
}