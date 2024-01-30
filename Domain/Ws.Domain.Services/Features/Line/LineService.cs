using MDSoft.NetUtils;
using NHibernate.Criterion;
using Ws.Database.Core.Entities.Ref.Lines;
using Ws.Database.Core.Entities.Ref.PlusLines;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Domain.Services.Features.Line;

internal class LineService : ILineService
{
    public IEnumerable<LineEntity> GetAll() => new SqlLineRepository().GetAll();

    public LineEntity GetItemByUid(Guid uid) => new SqlLineRepository().GetByUid(uid);
    
    public IEnumerable<PluEntity> GetLinePlus(LineEntity line) => GetLinePlusFk(line).Select(i => i.Plu);
    
    public IEnumerable<PluLineEntity> GetLinePlusFk(LineEntity line)
    {
        DetachedCriteria criteria = DetachedCriteria.For<PluLineEntity>()
            .Add(Restrictions.Eq(nameof(PluLineEntity.Line), line));
        return new SqlPluLineRepository().GetListByCriteria(criteria);
    }
    
    public LineEntity GetCurrentLine()
    {
        DetachedCriteria criteria = DetachedCriteria.For<LineEntity>()
            .Add(Restrictions.Eq(nameof(LineEntity.PcName), MdNetUtils.GetLocalDeviceName(false)));
        return new SqlLineRepository().GetItemByCriteria(criteria);
    }
    
    public IEnumerable<PluEntity> GetLineWeightPlus(LineEntity line) => GetPluEntitiesByWeightCheck(line, true);

    public IEnumerable<PluEntity> GetLinePiecePlus(LineEntity line) => GetPluEntitiesByWeightCheck(line, false);

    private IEnumerable<PluEntity> GetPluEntitiesByWeightCheck(LineEntity line, bool isCheckWeight)
    {
        DetachedCriteria criteria = DetachedCriteria.For<PluLineEntity>()
            .CreateAlias(nameof(PluLineEntity.Plu), "plu")
            .Add(Restrictions.Eq(nameof(PluLineEntity.Line), line))
            .Add(Restrictions.Eq($"plu.{nameof(PluEntity.IsCheckWeight)}", isCheckWeight));
        
        return new SqlPluLineRepository().GetListByCriteria(criteria).Select(pluLine => pluLine.Plu);
    }
}