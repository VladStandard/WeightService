namespace WsStorageCore.Entities.SchemaRef1c.Clips;

/// <summary>
/// SQL-контроллер таблицы CLIPS.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlClipRepository : WsSqlTableRepositoryBase<WsSqlClipEntity>
{
    #region Public and private methods

    public WsSqlClipEntity GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlClipEntity>();

    public IEnumerable<WsSqlClipEntity> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<WsSqlClipEntity>(sqlCrudConfig);
    }

    public WsSqlClipEntity GetItemByUid1C(Guid uid1C)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualUid1C(uid1C));
        return SqlCore.GetItemByCrud<WsSqlClipEntity>(sqlCrudConfig);
    }

    #endregion
}