using Ws.Database.Nhibernate.Common;
using Ws.Database.Nhibernate.Common.Commands;
using Ws.Database.Nhibernate.Common.Queries.Item;
using Ws.Database.Nhibernate.Common.Queries.List;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Database.Nhibernate.Entities.Ref1c.Bundles;

public sealed class SqlBundleRepository : BaseRepository,
    IGetItemByUid1C<BundleEntity>, IGetItemByUid<BundleEntity>, IGetAll<BundleEntity>, ISave<BundleEntity>
{
    public BundleEntity GetByUid(Guid uid) => Session.Get<BundleEntity>(uid);

    public BundleEntity GetByUid1C(Guid uid1C) =>
        Session.Query<BundleEntity>().FirstOrDefault(i => i.Uid1C == uid1C) ?? new();

    public IEnumerable<BundleEntity> GetAll() =>
        Session.Query<BundleEntity>().OrderBy(i => i.Weight).ThenBy(i => i.Name).ToList();
    
    public BundleEntity Save(BundleEntity item) { Session.Save(item); return item; }
}