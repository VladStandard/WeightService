using WsStorageCore.OrmUtils;
namespace WsStorageCore.Tables.TableRef1cModels.Boxes;

/// <summary>
/// SQL-контроллер таблицы коробок.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlBoxRepository : WsSqlTableRepositoryBase<WsSqlBoxModel>
{
    #region Public and private methods

    public WsSqlBoxModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlBoxModel>();

    public IEnumerable<WsSqlBoxModel> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<WsSqlBoxModel>(sqlCrudConfig);
    }
    
    /// <summary>
    /// Получить коробку по полю UID_1C.
    /// </summary>
    public WsSqlBoxModel GetItemByUid1C(Guid uid1C)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualUid1C(uid1C));
        return SqlCore.GetItemByCrud<WsSqlBoxModel>(sqlCrudConfig);
    }

    #endregion
}