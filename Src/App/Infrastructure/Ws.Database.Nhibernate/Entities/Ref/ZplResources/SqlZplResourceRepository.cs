using Ws.Database.Nhibernate.Common;
using Ws.Database.Nhibernate.Common.Commands;
using Ws.Database.Nhibernate.Common.Queries.Item;
using Ws.Database.Nhibernate.Common.Queries.List;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Nhibernate.Entities.Ref.ZplResources;

public class SqlZplResourceRepository : BaseRepository, IGetItemByUid<ZplResourceEntity>, IGetAll<ZplResourceEntity>,
    ISave<ZplResourceEntity>, IUpdate<ZplResourceEntity>, IDelete<ZplResourceEntity>
{
    public ZplResourceEntity GetByUid(Guid uid) => Session.Get<ZplResourceEntity>(uid) ?? new();
    public IEnumerable<ZplResourceEntity> GetAll() => Session.Query<ZplResourceEntity>().OrderBy(i => i.Name).ToList();
    public ZplResourceEntity Save(ZplResourceEntity item) { Session.Save(item); return item; }
    public ZplResourceEntity Update(ZplResourceEntity item) { Session.Update(item); return item; }
    public void Delete(ZplResourceEntity item) => Session.Delete(item);
}