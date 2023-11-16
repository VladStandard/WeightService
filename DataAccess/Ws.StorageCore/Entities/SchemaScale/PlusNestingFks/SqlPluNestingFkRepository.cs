namespace Ws.StorageCore.Entities.SchemaScale.PlusNestingFks;

public sealed class SqlPluNestingFkRepository : SqlTableRepositoryBase<SqlPluNestingFkEntity>
{
    public SqlViewPluNestingModel GetNewView() => new();

    public IEnumerable<SqlPluNestingFkEntity> GetEnumerable(SqlCrudConfigModel sqlCrudConfig)
    {
        return SqlCore.GetEnumerable<SqlPluNestingFkEntity>(sqlCrudConfig).OrderBy(x => x.Plu.Number);
    }
    
    public IEnumerable<SqlPluNestingFkEntity> GetEnumerableByPluUidActual(SqlPluEntity plu)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigFactory.GetCrudActual();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualFk(nameof(SqlPluNestingFkEntity.Plu), plu));
        return SqlCore.GetEnumerable<SqlPluNestingFkEntity>(sqlCrudConfig);
    }

    public SqlPluNestingFkEntity GetByPluAndUid1C(SqlPluEntity plu, Guid uid1C)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigFactory.GetCrudAll();
        
        sqlCrudConfig.AddFilters(new() {
            SqlRestrictions.Equal(nameof(SqlPluNestingFkEntity.Uid1C), uid1C),
            SqlRestrictions.EqualFk(nameof(SqlPluNestingFkEntity.Plu), plu)
        });
        
        return SqlCore.GetItemByCrud<SqlPluNestingFkEntity>(sqlCrudConfig);
    }
    public SqlPluNestingFkEntity GetDefaultByPlu(SqlPluEntity plu)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilters(new() {
            SqlRestrictions.Equal(nameof(SqlPluNestingFkEntity.IsDefault), true),
            SqlRestrictions.EqualFk(nameof(SqlPluTemplateFkEntity.Plu), plu)
        });
        return SqlCore.GetItemByCrud<SqlPluNestingFkEntity>(sqlCrudConfig);
    }
}