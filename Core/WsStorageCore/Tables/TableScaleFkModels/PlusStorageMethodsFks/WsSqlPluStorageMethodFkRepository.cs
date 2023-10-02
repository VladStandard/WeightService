using WsStorageCore.OrmUtils;
namespace WsStorageCore.Tables.TableScaleFkModels.PlusStorageMethodsFks;

/// <summary>
/// SQL-контроллер таблицы записей таблиц PLUS_STORAGE_METHODS, PLUS_STORAGE_METHODS_FK.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlPluStorageMethodFkRepository : WsSqlTableRepositoryBase<WsSqlPluStorageMethodFkModel>
{
    #region Public and private methods

    public WsSqlPluStorageMethodFkModel GetItemByPlu(WsSqlPluModel plu)
    {
        if (plu.IsNew)
            return new();
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualFk(nameof(WsSqlPluStorageMethodFkModel.Plu), plu));
        return SqlCore.GetItemByCrud<WsSqlPluStorageMethodFkModel>(sqlCrudConfig);
    }

    public List<WsSqlPluStorageMethodFkModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        IEnumerable<WsSqlPluStorageMethodFkModel> items = SqlCore.GetEnumerableNotNullable<WsSqlPluStorageMethodFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder)
            items = items.OrderBy(item => item.Plu.Number);
        return items.ToList();
    }

    #endregion
}