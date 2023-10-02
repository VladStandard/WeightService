using WsStorageCore.OrmUtils;
namespace WsStorageCore.Tables.TableScaleFkModels.PlusClipsFks;

/// <summary>
/// SQL-контроллер таблицы связей клипс и ПЛУ.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlPluClipFkRepository : WsSqlTableRepositoryBase<WsSqlPluClipFkModel>
{
    #region Public and private methods

    public WsSqlPluClipFkModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlPluClipFkModel>();

    public WsSqlPluClipFkModel GetItemByPlu(WsSqlPluModel plu)
    {
        if (plu.IsNew) return new();
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualFk(nameof(WsSqlPluClipFkModel.Plu), plu));
        return SqlCore.GetItemByCrud<WsSqlPluClipFkModel>(sqlCrudConfig);
    }

    public IEnumerable<WsSqlPluClipFkModel> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig) {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new($"{nameof(PluClipFkModel.Clip)}.{nameof(ClipModel.Name)}", SqlOrderDirection.Asc));
        IEnumerable<WsSqlPluClipFkModel> items = SqlCore.GetEnumerableNotNullable<WsSqlPluClipFkModel>(sqlCrudConfig).ToList();
        if (items.Any())
        {
            WsSqlPluClipFkModel pluClipFk = items.First();
            if (pluClipFk.Plu.IsNew)
                pluClipFk.Plu = SqlCore.GetItemNewEmpty<WsSqlPluModel>();
            if (pluClipFk.Clip.IsNew)
                pluClipFk.Clip = SqlCore.GetItemNewEmpty<WsSqlClipModel>();
        }
        if (sqlCrudConfig.IsResultOrder)
            items = items.OrderBy(item => item.Clip.Name);
        return items;
    }

    #endregion
}