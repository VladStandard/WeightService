using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Scale;
using Ws.Domain.Models.Entities.SchemaScale;
using Ws.StorageCore.OrmUtils;

namespace Ws.StorageCore.Entities.Scales.PlusNestingFks;

public sealed class SqlPluNestingFkRepository : SqlTableRepositoryBase<PluNestingEntity>
{
    public IEnumerable<PluNestingEntity> GetEnumerable(SqlCrudConfigModel sqlCrudConfig)
    {
        return SqlCore.GetEnumerable<PluNestingEntity>(sqlCrudConfig).OrderBy(x => x.Plu.Number);
    }
    
    public IEnumerable<PluNestingEntity> GetEnumerableByPluUidActual(PluEntity plu)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualFk(nameof(PluNestingEntity.Plu), plu));
        sqlCrudConfig.AddOrder(SqlOrder.Desc(nameof(PluNestingEntity.IsDefault)));
        return SqlCore.GetEnumerable<PluNestingEntity>(sqlCrudConfig);
    }

    public PluNestingEntity GetByPluAndUid1C(PluEntity plu, Guid uid1C)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        
        sqlCrudConfig.AddFilters([
            SqlRestrictions.Equal(nameof(PluNestingEntity.Uid1C), uid1C),
            SqlRestrictions.EqualFk(nameof(PluNestingEntity.Plu), plu)
        ]);
        
        return SqlCore.GetItemByCrud<PluNestingEntity>(sqlCrudConfig);
    }
    public PluNestingEntity GetDefaultByPlu(PluEntity plu)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilters([
            SqlRestrictions.Equal(nameof(PluNestingEntity.IsDefault), true),
            SqlRestrictions.EqualFk(nameof(PluTemplateFkEntity.Plu), plu)
        ]);
        return SqlCore.GetItemByCrud<PluNestingEntity>(sqlCrudConfig);
    }
}