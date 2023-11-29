namespace Ws.StorageCore.Entities.SchemaScale.PlusFks;

/// <summary>
/// SQL-контроллер таблицы PLUS_BUNDLES_FK.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class SqlPluFkRepository : SqlTableRepositoryBase<SqlPluFkEntity>
{
    public IEnumerable<SqlPluFkEntity> GetEnumerable(SqlCrudConfigModel sqlCrudConfig)
    {
        IEnumerable<SqlPluFkEntity> items = SqlCore.GetEnumerable<SqlPluFkEntity>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder)
            items = items.OrderBy(item => item.Plu.Number);
        return items.ToList();
    }
    
    public SqlPluFkEntity GetByPlu(SqlPluEntity plu)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualFk(nameof(SqlPluStorageMethodFkEntity.Plu), plu));
        return SqlCore.GetItemByCrud<SqlPluFkEntity>(sqlCrudConfig);
    }
}