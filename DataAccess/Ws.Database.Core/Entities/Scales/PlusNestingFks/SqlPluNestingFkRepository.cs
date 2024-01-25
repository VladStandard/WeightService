using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Scale;

namespace Ws.Database.Core.Entities.Scales.PlusNestingFks;

public sealed class SqlPluNestingFkRepository
{
    public IEnumerable<PluNestingEntity> GetEnumerable(SqlCrudConfigModel sqlCrudConfig)
    {
        return SqlCoreHelper.Instance.GetEnumerable<PluNestingEntity>(sqlCrudConfig).OrderBy(x => x.Plu.Number);
    }
    
    public IEnumerable<PluNestingEntity> GetEnumerableByPlu(PluEntity plu)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualFk(nameof(PluNestingEntity.Plu), plu));
        sqlCrudConfig.AddOrder(SqlOrder.Desc(nameof(PluNestingEntity.Uid1C)));
        return SqlCoreHelper.Instance.GetEnumerable<PluNestingEntity>(sqlCrudConfig);
    }

    public PluNestingEntity GetByPluAndUid1C(PluEntity plu, Guid uid1C)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        
        sqlCrudConfig.AddFilters([
            SqlRestrictions.Equal(nameof(PluNestingEntity.Uid1C), uid1C),
            SqlRestrictions.EqualFk(nameof(PluNestingEntity.Plu), plu)
        ]);
        
        return SqlCoreHelper.Instance.GetItemByCrud<PluNestingEntity>(sqlCrudConfig);
    }
    
    public PluNestingEntity GetDefaultByPlu(PluEntity plu)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilters([
            SqlRestrictions.Equal(nameof(PluNestingEntity.Uid1C), Guid.Empty),
            SqlRestrictions.EqualFk(nameof(PluTemplateFkEntity.Plu), plu)
        ]);
        return SqlCoreHelper.Instance.GetItemByCrud<PluNestingEntity>(sqlCrudConfig);
    }
    
    public void DeleteAllPluNestings(PluEntity plu)
    {
        if (plu.IsNew) return;
        List<PluNestingEntity> pluNestingEntities = GetEnumerableByPlu(plu).ToList();
        foreach (PluNestingEntity entity in pluNestingEntities)
            SqlCoreHelper.Instance.Delete(entity);
    }
}