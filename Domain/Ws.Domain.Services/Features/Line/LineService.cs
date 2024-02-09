using System.Net;
using NHibernate.Criterion;
using Ws.Database.Core.Entities.Ref.Lines;
using Ws.Database.Core.Entities.Ref.PlusLines;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Domain.Services.Features.Line;

internal partial class LineService : ILineService
{
    #region Get Lines

    public LineEntity GetCurrentLine()
    {
        return new SqlLineRepository().GetItemByQuery(
        QueryOver.Of<LineEntity>().Where(i => i.PcName == Dns.GetHostName())
        );
    }
    public IEnumerable<LineEntity> GetAll() => new SqlLineRepository().GetAll();
    public LineEntity GetItemByUid(Guid uid) => new SqlLineRepository().GetByUid(uid);

    #endregion

    #region Get Plus

    public IEnumerable<PluEntity> GetLinePlus(LineEntity line) => GetLinePlusFk(line).Select(i => i.Plu);
    public IEnumerable<PluEntity> GetLineWeightPlus(LineEntity line) => GetPluEntitiesByWeightCheck(line, true);
    public IEnumerable<PluEntity> GetLinePiecePlus(LineEntity line) => GetPluEntitiesByWeightCheck(line, false);

    #endregion

    #region Other

    public IEnumerable<PluLineEntity> GetLinePlusFk(LineEntity line)
    {
        return new SqlPluLineRepository().GetListByQuery(
        QueryOver.Of<PluLineEntity>().Where(i => i.Line == line)
        );
    }

    #endregion
}