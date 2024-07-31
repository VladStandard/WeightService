using NHibernate.Criterion;
using ProjectionTools.Specifications;
using Ws.Database.Nhibernate.Common;
using Ws.Domain.Models.Entities.Devices.Arms;
using Ws.Domain.Models.Entities.Ref1c.Plus;
using Ws.Domain.Services.Features.Plus.Specs;

namespace Ws.Domain.Services.Features;

public class ArmService : BaseRepository
{
    [Transactional]
    public Arm GetItemByUid(Guid uid) => Session.Get<Arm>(uid) ?? new();

    [Transactional]
    public IList<Plu> GetArmPiecePlus(Guid uid)  => GetPluListByArmAndSpec(uid, PluSpecs.GetPiece());

    [Transactional]
    public Arm Update(Arm line) { Session.Update(line); return line; }

    private IList<Plu> GetPluListByArmAndSpec(Guid uid, Specification<Plu> spec)
    {
        QueryOver<ArmPlu> queryOver =
            QueryOver.Of<ArmPlu>().Where(i => i.Line.Uid == uid)
                .JoinQueryOver<Plu>(i => i.Plu).Where(spec);
        return queryOver.DetachedCriteria.GetExecutableCriteria(Session).List<ArmPlu>().OrderBy(i => i.Plu.Number).Select(pluLine => pluLine.Plu).ToList();
    }
}