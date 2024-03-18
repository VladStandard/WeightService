using NHibernate.Criterion;
using Ws.Database.Nhibernate.Entities.Ref.PlusLines;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Domain.Services.Features.Line;

internal partial class LineService
{
    private static IEnumerable<PluEntity> GetPluEntitiesByWeightCheck(LineEntity line, bool isCheckWeight)
    {
        QueryOver<PluLineEntity> queryOver =
            QueryOver.Of<PluLineEntity>().Where(i => i.Line == line)
                .JoinQueryOver<PluEntity>(i => i.Plu).Where(plu => plu.IsCheckWeight == isCheckWeight);
        return new SqlPluLineRepository().GetListByQuery(queryOver).Select(pluLine => pluLine.Plu);
    }
}