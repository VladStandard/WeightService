using NHibernate.Criterion;
using Ws.Database.Nhibernate.Entities.Ref.PlusLines;
using Ws.Domain.Models.Entities.Devices.Arms;
using Ws.Domain.Models.Entities.Ref1c.Plu;

namespace Ws.Domain.Services.Features.Arms;

internal partial class ArmService
{
    private static IEnumerable<Plu> GetPluEntitiesByWeightCheck(Arm line, bool isCheckWeight)
    {
        QueryOver<ArmPlu> queryOver =
            QueryOver.Of<ArmPlu>().Where(i => i.Line == line)
                .JoinQueryOver<Plu>(i => i.Plu).Where(plu => plu.IsCheckWeight == isCheckWeight);
        return new SqlPluLineRepository().GetListByQuery(queryOver).Select(pluLine => pluLine.Plu);
    }
}