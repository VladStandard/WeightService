using Ws.Database.Nhibernate.Common;
using Ws.Database.Nhibernate.Common.Commands;
using Ws.Database.Nhibernate.Common.Queries.Item;
using Ws.Database.Nhibernate.Common.Queries.List;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Nhibernate.Entities.Ref.Claims;

public sealed class SqlClaimRepository : BaseRepository, IGetItemByUid<ClaimEntity>, IGetAll<ClaimEntity>,
    IUpdate<ClaimEntity>, ISave<ClaimEntity>, IDelete<ClaimEntity>
{
    public ClaimEntity GetByUid(Guid uid) => Session.Get<ClaimEntity>(uid) ?? new();
    public IEnumerable<ClaimEntity> GetAll() => Session.Query<ClaimEntity>().OrderBy(i => i.Name).ToList();
    public ClaimEntity Save(ClaimEntity item) { Session.Save(item); return item; }
    public ClaimEntity Update(ClaimEntity item) { Session.Update(item); return item; }
    public void Delete(ClaimEntity item) => Session.Delete(item);
}