using System.Net;
using NHibernate.Criterion;
using Ws.Database.Core.Entities.Ref.Lines;
using Ws.Database.Core.Entities.Ref.PlusLines;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.Line;

internal partial class LineService(SqlLineRepository lineRepo, SqlPluLineRepository pluLineRepo) : ILineService
{
    #region Get Lines

    [Transactional] public LineEntity GetCurrentLine()
    {
        return lineRepo.GetItemByQuery(
            QueryOver.Of<LineEntity>().Where(i => i.PcName == Dns.GetHostName())
        );
    }
    
    [Transactional] public IEnumerable<LineEntity> GetAll() => lineRepo.GetAll();
    
    [Transactional] public LineEntity GetItemByUid(Guid uid) => lineRepo.GetByUid(uid);

    #endregion

    #region Get Plus

    [Transactional] public IEnumerable<PluEntity> GetLinePlus(LineEntity line) => GetLinePlusFk(line).Select(i => i.Plu);
    [Transactional] public IEnumerable<PluEntity> GetLineWeightPlus(LineEntity line) => GetPluEntitiesByWeightCheck(line, true);
    [Transactional] public IEnumerable<PluEntity> GetLinePiecePlus(LineEntity line) => GetPluEntitiesByWeightCheck(line, false);

    #endregion

    #region Other

    [Transactional] public IEnumerable<PluLineEntity> GetLinePlusFk(LineEntity line)
    {
        return pluLineRepo.GetListByQuery(
            QueryOver.Of<PluLineEntity>().Where(i => i.Line == line)
        );
    }

    #endregion
}