using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.Claims;

public sealed class SqlClaimRepository : IUidRepo<ClaimEntity>
{
    public ClaimEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemByUid<ClaimEntity>(uid);
    
    public IEnumerable<ClaimEntity> GetEnumerable()
    {
        SqlCrudConfigModel crud = new();
        crud.AddOrder(SqlOrder.NameAsc());
        return SqlCoreHelper.Instance.GetEnumerable<ClaimEntity>(crud);
    }
}