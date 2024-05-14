using NHibernate.Criterion;
using Ws.Database.Nhibernate.Entities.Ref.PlusLines;
using Ws.Domain.Models.Entities.Devices.Arms;
using Ws.Domain.Models.Entities.Ref1c.Plu;

namespace Ws.Domain.Services.Features.Line;

internal partial class LineService
{
    private static IEnumerable<PluEntity> GetPluEntitiesByWeightCheck(Arm line, bool isCheckWeight)
    {
        QueryOver<ArmLine> queryOver =
            QueryOver.Of<ArmLine>().Where(i => i.Line == line)
                .JoinQueryOver<PluEntity>(i => i.Plu).Where(plu => plu.IsCheckWeight == isCheckWeight);
        return new SqlPluLineRepository().GetListByQuery(queryOver).Select(pluLine => pluLine.Plu);
    }
}