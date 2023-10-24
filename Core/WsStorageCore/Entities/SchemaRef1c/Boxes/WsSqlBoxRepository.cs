using WsStorageCore.OrmUtils;
namespace WsStorageCore.Entities.SchemaRef1c.Boxes;

/// <summary>
/// SQL-контроллер таблицы коробок.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlBoxRepository : WsSqlTableRepositoryBase<WsSqlBoxEntity>
{
    #region Public and private methods

    public WsSqlBoxEntity GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlBoxEntity>();

    public IEnumerable<WsSqlBoxEntity> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<WsSqlBoxEntity>(sqlCrudConfig);
    }
    
    /// <summary>
    /// Получить коробку по полю UID_1C.
    /// </summary>
    public WsSqlBoxEntity GetItemByUid1C(Guid uid1C)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualUid1C(uid1C));
        return SqlCore.GetItemByCrud<WsSqlBoxEntity>(sqlCrudConfig);
    }

    #endregion
}