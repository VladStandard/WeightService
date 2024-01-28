using Ws.Database.Core.Common.Queries;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Scale;

namespace Ws.Database.Core.Entities.Scales.PlusNestingFks;

public sealed class SqlPluNestingFkRepository : IGetListByCriteria<PluNestingEntity>
{
    public IEnumerable<PluNestingEntity> GetListByCriteria(DetachedCriteria criteria)
    {
        criteria.CreateAlias(nameof(PluNestingEntity.Plu), "plu")
            .AddOrder(SqlOrder.Asc($"plu.{nameof(PluEntity.Number)}"));
        return SqlCoreHelper.Instance.GetEnumerable<PluNestingEntity>(criteria);
    }
    
    public IEnumerable<PluNestingEntity> GetEnumerableByPlu(PluEntity plu)
    {
        DetachedCriteria criteria = DetachedCriteria.For<PluNestingEntity>()
            .Add(SqlRestrictions.EqualFk(nameof(PluNestingEntity.Plu), plu));
        return GetListByCriteria(criteria);
    }

    public PluNestingEntity GetByPluAndUid1C(PluEntity plu, Guid uid1C)
    {
        DetachedCriteria criteria = DetachedCriteria.For<PluNestingEntity>()
            .Add(SqlRestrictions.Equal(nameof(PluNestingEntity.Uid1C), uid1C))
            .Add(SqlRestrictions.Equal(nameof(PluTemplateFkEntity.Plu), plu));
        return SqlCoreHelper.Instance.GetItemByCriteria<PluNestingEntity>(criteria);
    }
    
    public PluNestingEntity GetDefaultByPlu(PluEntity plu)
    {
        DetachedCriteria criteria = DetachedCriteria.For<PluNestingEntity>()
            .Add(SqlRestrictions.Equal(nameof(PluNestingEntity.Uid1C), Guid.Empty))
            .Add(SqlRestrictions.Equal(nameof(PluTemplateFkEntity.Plu), plu));
        return SqlCoreHelper.Instance.GetItemByCriteria<PluNestingEntity>(criteria);
    }
    
    public void DeleteAllPluNestings(PluEntity plu)
    {
        if (plu.IsNew) return;
        List<PluNestingEntity> pluNestingEntities = GetEnumerableByPlu(plu).ToList();
        foreach (PluNestingEntity entity in pluNestingEntities)
            SqlCoreHelper.Instance.Delete(entity);
    }
}