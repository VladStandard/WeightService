using Ws.Database.Core.Common.Queries;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Database.Core.Entities.Ref.PlusLines;

public sealed class SqlPluLineRepository : IGetListByCriteria<PluLineEntity>
{
    public PluLineEntity GetItemByLinePlu(LineEntity line, PluEntity plu)
    {
        DetachedCriteria criteria = DetachedCriteria.For<PluLineEntity>()
            .Add(SqlRestrictions.EqualFk(nameof(PluLineEntity.Line), line))
            .Add(SqlRestrictions.EqualFk(nameof(PluLineEntity.Plu), plu));
        return SqlCoreHelper.Instance.GetItemByCriteria<PluLineEntity>(criteria);
    }
    
    public IEnumerable<PluLineEntity> GetListByCriteria(DetachedCriteria criteria)
    {
        criteria.CreateAlias(nameof(PluLineEntity.Plu), "plu")
            .AddOrder(SqlOrder.Asc($"plu.{nameof(PluEntity.Number)}"));
        return SqlCoreHelper.Instance.GetEnumerable<PluLineEntity>(criteria);
    }
    
    public IEnumerable<PluLineEntity> GetListByLine(LineEntity line)
    {
        DetachedCriteria criteria = DetachedCriteria.For<PluLineEntity>()
            .Add(SqlRestrictions.EqualFk(nameof(PluLineEntity.Line), line));
        return GetListByCriteria(criteria);
    }
    
    public IEnumerable<PluLineEntity> GetWeightListByLine(LineEntity line)
    {
        IEnumerable<PluLineEntity> items = GetListByLine(line);
        return items.Where(x => x.Plu.IsCheckWeight);
    }
    
    public IEnumerable<PluLineEntity> GetPieceListByLine(LineEntity line)
    {
        IEnumerable<PluLineEntity> items = GetListByLine(line);
        return items.Where(x => !x.Plu.IsCheckWeight);
    }
}