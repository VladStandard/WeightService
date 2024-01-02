namespace Ws.StorageCore.Entities.SchemaScale.PlusNestingFks;

public sealed class SqlPluNestingFkRepository : SqlTableRepositoryBase<SqlPluNestingFkEntity>
{
    public IEnumerable<SqlPluNestingFkEntity> GetEnumerable(SqlCrudConfigModel sqlCrudConfig)
    {
        return SqlCore.GetEnumerable<SqlPluNestingFkEntity>(sqlCrudConfig).OrderBy(x => x.Plu.Number);
    }
    
    public IEnumerable<SqlPluNestingFkEntity> GetEnumerableByPluUidActual(SqlPluEntity plu)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualFk(nameof(SqlPluNestingFkEntity.Plu), plu));
        sqlCrudConfig.AddOrder(SqlOrder.Desc(nameof(SqlPluNestingFkEntity.IsDefault)));
        return SqlCore.GetEnumerable<SqlPluNestingFkEntity>(sqlCrudConfig);
    }

    public SqlPluNestingFkEntity GetByPluAndUid1C(SqlPluEntity plu, Guid uid1C)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        
        sqlCrudConfig.AddFilters(new() {
            SqlRestrictions.Equal(nameof(SqlPluNestingFkEntity.Uid1C), uid1C),
            SqlRestrictions.EqualFk(nameof(SqlPluNestingFkEntity.Plu), plu)
        });
        
        return SqlCore.GetItemByCrud<SqlPluNestingFkEntity>(sqlCrudConfig);
    }
    public SqlPluNestingFkEntity GetDefaultByPlu(SqlPluEntity plu)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilters(new() {
            SqlRestrictions.Equal(nameof(SqlPluNestingFkEntity.IsDefault), true),
            SqlRestrictions.EqualFk(nameof(SqlPluTemplateFkEntity.Plu), plu)
        });
        return SqlCore.GetItemByCrud<SqlPluNestingFkEntity>(sqlCrudConfig);
    }
}