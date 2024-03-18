using Ws.Database.Nhibernate.Common;
using Ws.Database.Nhibernate.Common.Commands;
using Ws.Database.Nhibernate.Common.Queries.Item;
using Ws.Domain.Models.Entities.Print;

namespace Ws.Database.Nhibernate.Entities.Print.Labels;

public sealed class SqlLabelRepository : BaseRepository, IGetItemByUid<LabelEntity>, ISave<LabelEntity>
{
    public LabelEntity GetByUid(Guid uid) => Session.Get<LabelEntity>(uid) ?? new();

    public ViewLabel GetViewByUid(Guid uid) => Session.Get<ViewLabel>(uid) ?? new();

    public IEnumerable<ViewLabel> GetAllView() => Session.Query<ViewLabel>().ToList();

    public LabelEntity Save(LabelEntity item) { Session.Save(item); return item; }
}