namespace Ws.StorageCore.Entities.SchemaScale.PlusStorageMethodsFks;

/// <summary>
/// SQL-контроллер таблицы записей таблиц PLUS_STORAGE_METHODS, PLUS_STORAGE_METHODS_FK.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class SqlPluStorageMethodFkRepository : SqlTableRepositoryBase<SqlPluStorageMethodFkEntity>
{
    #region Public and private methods

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

    #endregion
}