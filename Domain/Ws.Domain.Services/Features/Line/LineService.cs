using System.Net;
using NHibernate.Criterion;
using Ws.Database.Core.Entities.Ref.Lines;
using Ws.Database.Core.Entities.Ref.PlusLines;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Domain.Services.Features.Line;

internal partial class LineService(SqlLineRepository lineRepo, SqlPluLineRepository pluLineRepo) : ILineService
{
    #region Get Lines

    public LineEntity GetCurrentLine()
    {
        return lineRepo.GetItemByQuery(
        QueryOver.Of<LineEntity>().Where(i => i.PcName == Dns.GetHostName())
        );
    }
    public IEnumerable<LineEntity> GetAll() => lineRepo.GetAll();
    public LineEntity GetItemByUid(Guid uid) => lineRepo.GetByUid(uid);

    #endregion

    #region Get Plus

    public IEnumerable<PluEntity> GetLinePlus(LineEntity line) => GetLinePlusFk(line).Select(i => i.Plu);
    public IEnumerable<PluEntity> GetLineWeightPlus(LineEntity line) => GetPluEntitiesByWeightCheck(line, true);
    public IEnumerable<PluEntity> GetLinePiecePlus(LineEntity line) => GetPluEntitiesByWeightCheck(line, false);

    #endregion

    #region Other

    public IEnumerable<PluLineEntity> GetLinePlusFk(LineEntity line)
    {
        return pluLineRepo.GetListByQuery(
        QueryOver.Of<PluLineEntity>().Where(i => i.Line == line)
        );
    }

    #endregion
}