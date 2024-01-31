using Ws.Database.Core.Common.Queries;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.Claims;

public sealed class SqlClaimRepository : IGetItemByUid<ClaimEntity>, IGetAll<ClaimEntity>
{
    public ClaimEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemById<ClaimEntity>(uid);
    
    public IEnumerable<ClaimEntity> GetAll()
    {
        return SqlCoreHelper.Instance.GetEnumerable<ClaimEntity>(
            DetachedCriteria.For<ClaimEntity>().AddOrder(SqlOrder.NameAsc())
        );
    }
}