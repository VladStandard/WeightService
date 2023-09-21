namespace WsStorageCore.Tables.TableScaleModels.Boxes;

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
        return SqlCore.GetEnumerableNotNullable<WsSqlBoxModel>(sqlCrudConfig);
    }
    
    /// <summary>
    /// Получить коробку по полю UID_1C.
    /// </summary>
    /// <param name="uid1C"></param>
    /// <returns></returns>
    public WsSqlBoxModel GetItemByUid1C(Guid uid1C)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualUid1C(uid1C));
        return SqlCore.GetItemByCrud<WsSqlBoxModel>(sqlCrudConfig);
    }

    #endregion
}