using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.Claims;

public sealed class SqlClaimRepository : BaseRepository, IGetItemByUid<ClaimEntity>, IGetAll<ClaimEntity>
{
    public ClaimEntity GetByUid(Guid uid) => Session.Get<ClaimEntity>(uid) ?? new();
    
    public IEnumerable<ClaimEntity> GetAll() => Session.Query<ClaimEntity>().OrderBy(i => i.Name).ToList();
}