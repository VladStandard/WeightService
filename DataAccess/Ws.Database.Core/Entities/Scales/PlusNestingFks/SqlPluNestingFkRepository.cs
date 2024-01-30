using Ws.Database.Core.Common.Queries;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Scale;

namespace Ws.Database.Core.Entities.Scales.PlusNestingFks;

public sealed class SqlPluNestingFkRepository : IGetListByCriteria<PluNestingEntity>
{
    public IEnumerable<PluNestingEntity> GetListByCriteria(DetachedCriteria criteria) => 
        SqlCoreHelper.Instance.GetEnumerable<PluNestingEntity>(criteria).OrderBy(i => i.Plu.Number);
    
    public IEnumerable<PluNestingEntity> GetEnumerableByPlu(PluEntity plu)
    {
        return GetListByCriteria(
            DetachedCriteria.For<PluNestingEntity>()
                .Add(Restrictions.Eq(nameof(PluNestingEntity.Plu), plu))
        );
    }

    public PluNestingEntity GetByPluAndUid1C(PluEntity plu, Guid uid1C)
    {
        return SqlCoreHelper.Instance.GetItemByCriteria<PluNestingEntity>(
            DetachedCriteria.For<PluNestingEntity>()
                .Add(SqlRestrictions.Equal(nameof(PluNestingEntity.Uid1C), uid1C))
                .Add(SqlRestrictions.Equal(nameof(PluTemplateFkEntity.Plu), plu))
            );
    }
    
    public PluNestingEntity GetDefaultByPlu(PluEntity plu)
    {
        return SqlCoreHelper.Instance.GetItemByCriteria<PluNestingEntity>(
            DetachedCriteria.For<PluNestingEntity>()
                .Add(SqlRestrictions.EqualUid1C(Guid.Empty))
                .Add(SqlRestrictions.Equal(nameof(PluTemplateFkEntity.Plu), plu))
            );
    }
    
    public void DeleteAllPluNestings(PluEntity plu)
    {
        if (plu.IsNew) return;
        List<PluNestingEntity> pluNestingEntities = GetEnumerableByPlu(plu).ToList();
        foreach (PluNestingEntity entity in pluNestingEntities)
            SqlCoreHelper.Instance.Delete(entity);
    }
}