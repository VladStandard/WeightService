using Ws.Database.Nhibernate.Common;
using Ws.Database.Nhibernate.Common.Queries.Item;
using Ws.Database.Nhibernate.Common.Queries.List;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Database.Nhibernate.Entities.Ref1c.Bundles;

public sealed class SqlBundleRepository : BaseRepository, IGetItemByUid<Bundle>, IGetAll<Bundle>
{
    public Bundle GetByUid(Guid uid) => Session.Get<Bundle>(uid);

    public IList<Bundle> GetAll() =>
        Session.Query<Bundle>().OrderBy(i => i.Weight).ThenBy(i => i.Name).ToList();
}