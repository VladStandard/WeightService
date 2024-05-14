using Ws.Database.Nhibernate.Common;
using Ws.Database.Nhibernate.Common.Commands;
using Ws.Database.Nhibernate.Common.Queries.Item;
using Ws.Database.Nhibernate.Common.Queries.List;
using Ws.Domain.Models.Entities.Print;

namespace Ws.Database.Nhibernate.Entities.Ref.ZplResources;

public class SqlZplResourceRepository : BaseRepository, IGetItemByUid<ZplResource>, IGetAll<ZplResource>,
    ISave<ZplResource>, IUpdate<ZplResource>, IDelete<ZplResource>
{
    public ZplResource GetByUid(Guid uid) => Session.Get<ZplResource>(uid) ?? new();
    public IEnumerable<ZplResource> GetAll() => Session.Query<ZplResource>().OrderBy(i => i.Name).ToList();
    public ZplResource Save(ZplResource item) { Session.Save(item); return item; }
    public ZplResource Update(ZplResource item) { Session.Update(item); return item; }
    public void Delete(ZplResource item) => Session.Delete(item);
}