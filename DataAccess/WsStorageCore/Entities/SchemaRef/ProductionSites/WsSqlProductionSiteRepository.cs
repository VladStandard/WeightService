namespace WsStorageCore.Entities.SchemaRef.ProductionSites;

/// <summary>
/// SQL-контроллер таблицы ProductionSite.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlProductionSiteRepository : WsSqlTableRepositoryBase<WsSqlProductionSiteEntity>
{
    #region Public and private methods

    public WsSqlProductionSiteEntity GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlProductionSiteEntity>();

    public IEnumerable<WsSqlProductionSiteEntity> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<WsSqlProductionSiteEntity>(sqlCrudConfig);
    }

    #endregion
}