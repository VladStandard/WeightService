using Ws.Database.Nhibernate.Common;
using Ws.Domain.Models.Entities.Print;

namespace Ws.Database.Nhibernate.Entities.Print.Labels;

public sealed class SqlLabelRepository : BaseRepository
{
    public (Label, LabelZpl) Save(Label label, LabelZpl zpl)
    {
        Session.Save(label);

        zpl.Uid = label.Uid;

        Session.Save(zpl);
        return (label, zpl);
    }
}