using WsStorageCore.OrmUtils;
namespace WsStorageCore.Entities.SchemaRef1c.Bundles;

/// <summary>
/// SQL-контроллер таблицы BUNDLES.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlBundleRepository : WsSqlTableRepositoryBase<WsSqlBundleEntity>
{
    #region Public and private methods

    public WsSqlBundleEntity GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlBundleEntity>();
    
    public WsSqlBundleEntity GetItemByUid1C(Guid uid1C)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualUid1C(uid1C));
        return SqlCore.GetItemByCrud<WsSqlBundleEntity>(sqlCrudConfig);
    }
    
    public IEnumerable<WsSqlBundleEntity> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<WsSqlBundleEntity>(sqlCrudConfig);
    }

    #endregion
}