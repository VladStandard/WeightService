namespace WsStorageCore.Entities.SchemaScale.PlusStorageMethodsFks;

/// <summary>
/// SQL-контроллер таблицы записей таблиц PLUS_STORAGE_METHODS, PLUS_STORAGE_METHODS_FK.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlPluStorageMethodFkRepository : WsSqlTableRepositoryBase<WsSqlPluStorageMethodFkEntity>
{
    #region Public and private methods

    public WsSqlPluStorageMethodFkEntity GetItemByPlu(WsSqlPluEntity plu)
    {
        if (plu.IsNew)
            return new();
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualFk(nameof(WsSqlPluStorageMethodFkEntity.Plu), plu));
        return SqlCore.GetItemByCrud<WsSqlPluStorageMethodFkEntity>(sqlCrudConfig);
    }

    public List<WsSqlPluStorageMethodFkEntity> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        IEnumerable<WsSqlPluStorageMethodFkEntity> items = SqlCore.GetEnumerable<WsSqlPluStorageMethodFkEntity>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder)
            items = items.OrderBy(item => item.Plu.Number);
        return items.ToList();
    }

    #endregion
}