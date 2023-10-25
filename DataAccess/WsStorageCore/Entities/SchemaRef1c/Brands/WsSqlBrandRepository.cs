using WsStorageCore.OrmUtils;
namespace WsStorageCore.Entities.SchemaRef1c.Brands;

/// <summary>
/// SQL-контроллер таблицы брендов.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlBrandRepository : WsSqlTableRepositoryBase<WsSqlBrandEntity>
{
    #region Public and private methods

    public WsSqlBrandEntity GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlBrandEntity>();

    public IEnumerable<WsSqlBrandEntity> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<WsSqlBrandEntity>(sqlCrudConfig);
    }

    public WsSqlBrandEntity GetItemByUid1C(Guid uid1C)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(
        SqlRestrictions.EqualUid1C(uid1C)
        );
        return SqlCore.GetItemByCrud<WsSqlBrandEntity>(sqlCrudConfig);
    }

    #endregion
}