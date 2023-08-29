namespace WsStorageCore.Tables.TableRefModels.ProductionSites;

/// <summary>
/// SQL-контроллер таблицы ProductionSite.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlProductionSiteRepository : WsSqlTableRepositoryBase<WsSqlProductionSiteModel>
{
    #region Public and private methods

    public WsSqlProductionSiteModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlProductionSiteModel>();

    public IEnumerable<WsSqlProductionSiteModel> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(nameof(WsSqlTableBase.Name));
        return SqlCore.GetEnumerableNotNullable<WsSqlProductionSiteModel>(sqlCrudConfig);
    }

    public async Task<IEnumerable<WsSqlProductionSiteModel>> GetEnumerableAsync(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(nameof(WsSqlTableBase.Name));
        return await SqlCore.GetEnumerableNotNullableAsync<WsSqlProductionSiteModel>(sqlCrudConfig);
    }

    #endregion
}