namespace Ws.StorageCore.Entities.SchemaRef1c.Bundles;

/// <summary>
/// SQL-контроллер таблицы BUNDLES.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class SqlBundleRepository : SqlTableRepositoryBase<SqlBundleEntity>
{
    #region Public and private methods

    public SqlBundleEntity GetNewItem() => SqlCore.GetItemNewEmpty<SqlBundleEntity>();
    
    public SqlBundleEntity GetItemByUid1C(Guid uid1C)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualUid1C(uid1C));
        return SqlCore.GetItemByCrud<SqlBundleEntity>(sqlCrudConfig);
    }
    
    public IEnumerable<SqlBundleEntity> GetEnumerable(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<SqlBundleEntity>(sqlCrudConfig);
    }

    #endregion
}