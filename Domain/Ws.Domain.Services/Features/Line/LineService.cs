using MDSoft.NetUtils;
using NHibernate.Criterion;
using Ws.Database.Core.Entities.Ref.Lines;
using Ws.Database.Core.Entities.Ref.PlusLines;
using Ws.Database.Core.Helpers;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Domain.Services.Features.Line;

internal class LineService : ILineService
{
    public IEnumerable<LineEntity> GetAll() => new SqlLineRepository().GetAll();
    
    public LineEntity GetItemByUid(Guid uid) => SqlCoreHelper.Instance.GetItemByUid<LineEntity>(uid);
    
    public IEnumerable<PluEntity> GetLinePlus(LineEntity line)
    {
       return new SqlPluLineRepository().GetListByLine(line).Select(i => i.Plu);
    }
    
    [Obsolete("Use GetLinePlus instead")]
    public IEnumerable<PluLineEntity> GetLinePlusFk(LineEntity line)
    {
        return new SqlPluLineRepository().GetListByLine(line);
    }
    
    public IEnumerable<PluEntity> GetLineWeightPlus(LineEntity line)
    {
        return new SqlPluLineRepository().GetWeightListByLine(line).Select(i => i.Plu);
    }
    
    public IEnumerable<PluEntity> GetLinePiecePlus(LineEntity line)
    {
        return new SqlPluLineRepository().GetPieceListByLine(line).Select(i => i.Plu);
    }

    public LineEntity GetCurrentLine()
    {
        DetachedCriteria criteria = DetachedCriteria.For<LineEntity>()
            .Add(Restrictions.Eq(nameof(LineEntity.PcName), MdNetUtils.GetLocalDeviceName(false)));
        return new SqlLineRepository().GetItemByCriteria(criteria);
    }
}




// public IEnumerable<PluEntity> GetLineWeightPlus(LineEntity line)
// {
//     DetachedCriteria criteria = DetachedCriteria.For<PluEntity>()
//         .CreateAlias(nameof(PluLineEntity), "pluLine")
//         .Add(SqlRestrictions.EqualFk($"pluLine.{nameof(PluLineEntity.Line)}", line))
//         .Add(SqlRestrictions.Equal(nameof(PluEntity.IsCheckWeight), true));
//     return new SqlPluRepository().GetListByCriteria(criteria);
// }
