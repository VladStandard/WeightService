namespace Ws.StorageCore.Entities.SchemaScale.PlusStorageMethodsFks;

/// <summary>
/// SQL-контроллер таблицы записей таблиц PLUS_STORAGE_METHODS, PLUS_STORAGE_METHODS_FK.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class SqlPluStorageMethodFkRepository : SqlTableRepositoryBase<SqlPluStorageMethodFkEntity>
{
    public SqlPluStorageMethodFkEntity GetItemByPlu(SqlPluEntity plu)
    {
        if (plu.IsNew)
            return new();
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualFk(nameof(SqlPluStorageMethodFkEntity.Plu), plu));
        return SqlCore.GetItemByCrud<SqlPluStorageMethodFkEntity>(sqlCrudConfig);
    }

    public List<SqlPluStorageMethodFkEntity> GetList(SqlCrudConfigModel sqlCrudConfig)
    {
        IEnumerable<SqlPluStorageMethodFkEntity> items = SqlCore.GetEnumerable<SqlPluStorageMethodFkEntity>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder)
            items = items.OrderBy(item => item.Plu.Number);
        return items.ToList();
    }
    
    public SqlPluStorageMethodFkEntity GetItemByPluNumber(int pluNumber)
    {
        SqlPluEntity plu = new SqlPluRepository().GetEnumerableByNumber((short)pluNumber).FirstOrDefault() ?? new();
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualFk(nameof(SqlPluStorageMethodFkEntity.Plu), plu));
        return SqlCore.GetItemByCrud<SqlPluStorageMethodFkEntity>(sqlCrudConfig);
    }
}