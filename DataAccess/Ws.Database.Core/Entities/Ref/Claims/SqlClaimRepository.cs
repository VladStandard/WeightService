using Ws.Database.Core.Common.Commands;
using Ws.Database.Core.Common.Queries.Item;
using Ws.Database.Core.Common.Queries.List;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.Claims;

public sealed class SqlClaimRepository : BaseRepository, IGetItemByUid<ClaimEntity>, IGetAll<ClaimEntity>,
    IUpdate<ClaimEntity>, ISave<ClaimEntity>, IDelete<ClaimEntity>
{
    public ClaimEntity GetByUid(Guid uid) => Session.Get<ClaimEntity>(uid) ?? new();
    public IEnumerable<ClaimEntity> GetAll() => Session.Query<ClaimEntity>().OrderBy(i => i.Name).ToList();
    public ClaimEntity Save(ClaimEntity item) { Session.Save(item); return item; }
    public ClaimEntity Update(ClaimEntity item) { Session.Update(item); return item; }
    public void Delete(ClaimEntity item) => Session.Delete(item);
}