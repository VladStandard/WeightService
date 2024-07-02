using Ws.Database.Nhibernate.Common;
using Ws.Database.Nhibernate.Common.Queries.Item;
using Ws.Domain.Models.Entities.Print;

namespace Ws.Database.Nhibernate.Entities.Print.Labels;

public sealed class SqlLabelRepository : BaseRepository, IGetItemByUid<Label>
{
    public Label GetByUid(Guid uid) => Session.Get<Label>(uid) ?? new();

    public IList<Label> GetAll() => Session.Query<Label>()
        .OrderByDescending(i => i.CreateDt).ToList();

    public (Label, LabelZpl) Save(Label label, LabelZpl zpl)
    {
        Session.Save(label);

        zpl.Uid = label.Uid;

        Session.Save(zpl);
        return (label, zpl);
    }

    public LabelZpl GetZplByUid(Guid uid) =>
        Session.Get<LabelZpl>(uid) ?? new();
}