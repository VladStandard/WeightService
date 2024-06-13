using NHibernate.Criterion;
using ProjectionTools.Specifications;
using Ws.Database.Nhibernate.Entities.Ref.PlusLines;
using Ws.Domain.Models.Entities.Devices.Arms;
using Ws.Domain.Models.Entities.Ref1c.Plu;

namespace Ws.Domain.Services.Features.Arms;

internal partial class ArmService
{
    private static IList<Plu> GetPluListByArmAndSpec(Guid uid, Specification<Plu> spec)
    {
        QueryOver<ArmPlu> queryOver =
            QueryOver.Of<ArmPlu>().Where(i => i.Line.Uid == uid)
                .JoinQueryOver<Plu>(i => i.Plu).Where(spec);
        return new SqlPluLineRepository().GetListByQuery(queryOver).Select(pluLine => pluLine.Plu).ToList();
    }
}