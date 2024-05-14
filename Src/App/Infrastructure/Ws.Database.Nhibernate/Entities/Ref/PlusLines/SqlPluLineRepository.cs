using Ws.Database.Nhibernate.Common;
using Ws.Database.Nhibernate.Common.Commands;
using Ws.Database.Nhibernate.Common.Queries.List;
using Ws.Domain.Models.Entities.Devices.Arms;

namespace Ws.Database.Nhibernate.Entities.Ref.PlusLines;

public sealed class SqlPluLineRepository : BaseRepository, IGetListByQuery<ArmLine>, IDelete<ArmLine>,
    ISave<ArmLine>
{
    public IEnumerable<ArmLine> GetListByQuery(QueryOver<ArmLine> query) =>
        query.DetachedCriteria.GetExecutableCriteria(Session).List<ArmLine>().OrderBy(i => i.Plu.Number);

    public IEnumerable<ArmLine> GetListByLine(Arm item) =>
        Session.Query<ArmLine>().Where(i => i.Line == item).ToList();

    public void Delete(ArmLine item) => Session.Delete(item);
    public ArmLine Save(ArmLine armLine) { Session.Save(armLine); return armLine; }
}