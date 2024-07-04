using Ws.Database.Nhibernate.Common;
using Ws.Database.Nhibernate.Common.Queries.List;
using Ws.Domain.Models.Entities.Devices.Arms;
using Ws.Domain.Models.Entities.Ref1c.Plus;

namespace Ws.Database.Nhibernate.Entities.Ref.PlusLines;

public sealed class SqlPluLineRepository : BaseRepository, IGetListByQuery<ArmPlu>
{
    public IList<ArmPlu> GetListByQuery(QueryOver<ArmPlu> query) =>
        query.DetachedCriteria.GetExecutableCriteria(Session).List<ArmPlu>().OrderBy(i => i.Plu.Number).ToList();

    public IList<ArmPlu> GetListByLine(Arm item) =>
        Session.Query<ArmPlu>().Where(i => i.Line == item).OrderBy(i => i.Plu.Number).ToList();

    private ArmPlu GetItemByLineAndPlu(Arm item, Plu plu) =>
        Session.Query<ArmPlu>()
            .FirstOrDefault(i => i.Line.Uid == item.Uid && i.Plu.Uid == plu.Uid) ?? new ArmPlu();


    public void Delete(Arm arm, Plu plu)
    {
        ArmPlu data = GetItemByLineAndPlu(arm, plu);
        if (data.IsExists)
            Session.Delete(data);
    }

    public ArmPlu Save(Arm arm, Plu plu)
    {
        ArmPlu data = GetItemByLineAndPlu(arm, plu);
        if (data.IsNew)
            Session.Save(new ArmPlu() { Line = arm, Plu = plu });
        return data;
    }
}