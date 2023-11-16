namespace Ws.StorageCore.Entities.SchemaRef.ProductionSites;

/// <summary>
/// SQL-контроллер таблицы ProductionSite.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class SqlProductionSiteRepository : SqlTableRepositoryBase<SqlProductionSiteEntity>
{
    #region Public and private methods

    public SqlProductionSiteEntity GetNewItem() => SqlCore.GetItemNewEmpty<SqlProductionSiteEntity>();

    public IEnumerable<SqlProductionSiteEntity> GetEnumerable(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<SqlProductionSiteEntity>(sqlCrudConfig);
    }

    #endregion
}