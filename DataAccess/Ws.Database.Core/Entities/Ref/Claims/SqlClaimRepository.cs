using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.Claims;

public sealed class SqlClaimRepository : BaseRepository, IGetItemByUid<ClaimEntity>, IGetAll<ClaimEntity>
{
    public ClaimEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemById<ClaimEntity>(uid);
    
    public IEnumerable<ClaimEntity> GetAll()
    {
        return SqlCoreHelper.Instance.GetEnumerable(
            QueryOver.Of<ClaimEntity>().OrderBy(i => i.Name).Asc
        );
    }
}