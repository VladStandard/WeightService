namespace WsStorageCore.Tables.TableScaleModels.Bundles;

/// <summary>
/// SQL-контроллер таблицы BUNDLES.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlBundleRepository : WsSqlTableRepositoryBase<WsSqlBundleModel>
{
    #region Public and private methods

    public WsSqlBundleModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlBundleModel>();
    
    public WsSqlBundleModel GetItemByUid1C(Guid uid1C)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(new() { Name = nameof(WsSqlTable1CBase.Uid1C), Value = uid1C });
        return SqlCore.GetItemByCrud<WsSqlBundleModel>(sqlCrudConfig);
    }
    
    public IEnumerable<WsSqlBundleModel> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(nameof(WsSqlTableBase.Name));
        return SqlCore.GetEnumerableNotNullable<WsSqlBundleModel>(sqlCrudConfig);
    }

    #endregion
}