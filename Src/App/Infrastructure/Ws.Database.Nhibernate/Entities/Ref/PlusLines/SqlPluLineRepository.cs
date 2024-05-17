using Ws.Database.Nhibernate.Common;
using Ws.Database.Nhibernate.Common.Commands;
using Ws.Database.Nhibernate.Common.Queries.List;
using Ws.Domain.Models.Entities.Devices.Arms;

namespace Ws.Database.Nhibernate.Entities.Ref.PlusLines;

public sealed class SqlPluLineRepository : BaseRepository, IGetListByQuery<ArmPlu>, IDelete<ArmPlu>,
    ISave<ArmPlu>
{
    public IEnumerable<ArmPlu> GetListByQuery(QueryOver<ArmPlu> query) =>
        query.DetachedCriteria.GetExecutableCriteria(Session).List<ArmPlu>().OrderBy(i => i.Plu.Number);

    public IEnumerable<ArmPlu> GetListByLine(Arm item) =>
        Session.Query<ArmPlu>().Where(i => i.Line == item).ToList();

    public void Delete(ArmPlu item) => Session.Delete(item);
    public ArmPlu Save(ArmPlu armPlu) { Session.Save(armPlu); return armPlu; }
}