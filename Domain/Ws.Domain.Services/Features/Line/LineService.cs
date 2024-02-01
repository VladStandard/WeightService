using System.Net;
using NHibernate.Criterion;
using Ws.Database.Core.Entities.Ref.Lines;
using Ws.Database.Core.Entities.Ref.PlusLines;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Domain.Services.Features.Line;

internal class LineService : ILineService
{
    public LineEntity GetCurrentLine()
    {
        return new SqlLineRepository().GetItemByQuery(
            QueryOver.Of<LineEntity>().Where(i => i.PcName == Dns.GetHostName())
        );
    }
    
    public IEnumerable<LineEntity> GetAll() => new SqlLineRepository().GetAll();

    public LineEntity GetItemByUid(Guid uid) => new SqlLineRepository().GetByUid(uid);
    
    public IEnumerable<PluEntity> GetLinePlus(LineEntity line) => GetLinePlusFk(line).Select(i => i.Plu);
    
    public IEnumerable<PluLineEntity> GetLinePlusFk(LineEntity line)
    {
        return new SqlPluLineRepository().GetListByQuery(
            QueryOver.Of<PluLineEntity>().Where(i => i.Line == line)
        );
    }
    
    public IEnumerable<PluEntity> GetLineWeightPlus(LineEntity line) => GetPluEntitiesByWeightCheck(line, true);

    public IEnumerable<PluEntity> GetLinePiecePlus(LineEntity line) => GetPluEntitiesByWeightCheck(line, false);

    private static IEnumerable<PluEntity> GetPluEntitiesByWeightCheck(LineEntity line, bool isCheckWeight)
    {
        QueryOver<PluLineEntity> queryOver = 
            QueryOver.Of<PluLineEntity>().Where(i => i.Line == line)
                .JoinQueryOver<PluEntity>(i => i.Plu).Where(plu => plu.IsCheckWeight == isCheckWeight);
        return new SqlPluLineRepository().GetListByQuery(queryOver).Select(pluLine => pluLine.Plu);
    }
}