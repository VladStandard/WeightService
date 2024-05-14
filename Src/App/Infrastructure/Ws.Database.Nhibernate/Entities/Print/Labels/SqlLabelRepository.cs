using Ws.Database.Nhibernate.Common;
using Ws.Database.Nhibernate.Common.Commands;
using Ws.Database.Nhibernate.Common.Queries.Item;
using Ws.Domain.Models.Entities.Print;

namespace Ws.Database.Nhibernate.Entities.Print.Labels;

public sealed class SqlLabelRepository : BaseRepository, IGetItemByUid<Label>, ISave<Label>
{
    public Label GetByUid(Guid uid) => Session.Get<Label>(uid) ?? new();

    public IEnumerable<Label> GetAll() => Session.Query<Label>()
        .OrderByDescending(i => i.CreateDt).ToList();

    public Label Save(Label item) { Session.Merge(item); return item; }
}