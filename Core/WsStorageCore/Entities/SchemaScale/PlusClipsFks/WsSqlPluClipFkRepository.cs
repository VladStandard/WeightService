using WsStorageCore.OrmUtils;
namespace WsStorageCore.Entities.SchemaScale.PlusClipsFks;

/// <summary>
/// SQL-контроллер таблицы связей клипс и ПЛУ.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlPluClipFkRepository : WsSqlTableRepositoryBase<WsSqlPluClipFkEntity>
{
    #region Public and private methods

    public WsSqlPluClipFkEntity GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlPluClipFkEntity>();

    public WsSqlPluClipFkEntity GetItemByPlu(WsSqlPluEntity plu)
    {
        if (plu.IsNew) return new();
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualFk(nameof(WsSqlPluClipFkEntity.Plu), plu));
        return SqlCore.GetItemByCrud<WsSqlPluClipFkEntity>(sqlCrudConfig);
    }

    public IEnumerable<WsSqlPluClipFkEntity> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig) {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new($"{nameof(PluClipFkModel.Clip)}.{nameof(ClipModel.Name)}", SqlOrderDirection.Asc));
        IEnumerable<WsSqlPluClipFkEntity> items = SqlCore.GetEnumerable<WsSqlPluClipFkEntity>(sqlCrudConfig).ToList();
        if (items.Any())
        {
            WsSqlPluClipFkEntity pluClipFk = items.First();
            if (pluClipFk.Plu.IsNew)
                pluClipFk.Plu = SqlCore.GetItemNewEmpty<WsSqlPluEntity>();
            if (pluClipFk.Clip.IsNew)
                pluClipFk.Clip = SqlCore.GetItemNewEmpty<WsSqlClipEntity>();
        }
        if (sqlCrudConfig.IsResultOrder)
            items = items.OrderBy(item => item.Clip.Name);
        return items;
    }

    #endregion
}