using Ws.Database.Nhibernate.Common;
using Ws.Database.Nhibernate.Common.Commands;
using Ws.Database.Nhibernate.Common.Queries.Item;
using Ws.Database.Nhibernate.Common.Queries.List;
using Ws.Domain.Models.Entities.Users;

namespace Ws.Database.Nhibernate.Entities.Ref.Claims;

public sealed class SqlClaimRepository : BaseRepository, IGetItemByUid<Claim>, IGetAll<Claim>,
    IUpdate<Claim>, ISave<Claim>, IDelete<Claim>
{
    public Claim GetByUid(Guid uid) => Session.Get<Claim>(uid) ?? new();
    public IList<Claim> GetAll() => Session.Query<Claim>().OrderBy(i => i.Name).ToList();
    public Claim Save(Claim item) { Session.Save(item); return item; }
    public Claim Update(Claim item) { Session.Update(item); return item; }
    public void Delete(Claim item) => Session.Delete(item);
}