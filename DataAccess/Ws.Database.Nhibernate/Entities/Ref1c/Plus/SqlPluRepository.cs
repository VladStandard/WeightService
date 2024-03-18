using Ws.Database.Nhibernate.Common;
using Ws.Database.Nhibernate.Common.Queries.Item;
using Ws.Database.Nhibernate.Common.Queries.List;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Database.Nhibernate.Entities.Ref1c.Plus;

public sealed class SqlPluRepository : BaseRepository, IGetItemByUid1C<PluEntity>, IGetItemByUid<PluEntity>,
    IGetAll<PluEntity>
{
    public PluEntity GetByUid(Guid uid) => Session.Get<PluEntity>(uid) ?? new();
    public PluEntity GetByUid1C(Guid uid1C) => Session.Query<PluEntity>().FirstOrDefault(i => i.Uid1C == uid1C) ?? new();
    public IEnumerable<PluEntity> GetAll() => Session.Query<PluEntity>().OrderBy(i => i.Number).ToList();
}