using WsStorageCore.OrmUtils;

namespace WsStorageCore.Tables.TableScaleModels.Clips;

/// <summary>
/// SQL-контроллер таблицы CLIPS.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlClipRepository : WsSqlTableRepositoryBase<WsSqlClipModel>
{
    #region Public and private methods

    public WsSqlClipModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlClipModel>();

    public IEnumerable<WsSqlClipModel> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerableNotNullable<WsSqlClipModel>(sqlCrudConfig);
    }

    public WsSqlClipModel GetItemByUid1C(Guid uid1C)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualUid1C(uid1C));
        return SqlCore.GetItemByCrud<WsSqlClipModel>(sqlCrudConfig);
    }

    #endregion
}