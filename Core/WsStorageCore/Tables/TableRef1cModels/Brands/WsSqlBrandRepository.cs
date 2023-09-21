namespace WsStorageCore.Tables.TableRef1cModels.Brands;

/// <summary>
/// SQL-контроллер таблицы брендов.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlBrandRepository : WsSqlTableRepositoryBase<WsSqlBrandModel>
{
    #region Public and private methods

    public WsSqlBrandModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlBrandModel>();

    public IEnumerable<WsSqlBrandModel> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerableNotNullable<WsSqlBrandModel>(sqlCrudConfig);
    }
    
    public WsSqlBrandModel GetItemByUid1C(Guid uid1C)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(
            SqlRestrictions.EqualUid1C(uid1C)
        );
        return SqlCore.GetItemByCrud<WsSqlBrandModel>(sqlCrudConfig);
    }
    
    #endregion
}