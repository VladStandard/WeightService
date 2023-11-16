namespace WsStorageCore.Entities.SchemaScale.PlusClipsFks;

/// <summary>
/// SQL-контроллер таблицы связей клипс и ПЛУ.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class SqlPluClipFkRepository : SqlTableRepositoryBase<SqlPluClipFkEntity>
{
    #region Public and private methods

    public SqlPluClipFkEntity GetNewItem() => SqlCore.GetItemNewEmpty<SqlPluClipFkEntity>();

    public SqlPluClipFkEntity GetItemByPlu(SqlPluEntity plu)
    {
        if (plu.IsNew) return new();
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualFk(nameof(SqlPluClipFkEntity.Plu), plu));
        return SqlCore.GetItemByCrud<SqlPluClipFkEntity>(sqlCrudConfig);
    }

    public IEnumerable<SqlPluClipFkEntity> GetEnumerable(SqlCrudConfigModel sqlCrudConfig) {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new($"{nameof(PluClipFkModel.Clip)}.{nameof(ClipModel.Name)}", SqlOrderDirection.Asc));
        IEnumerable<SqlPluClipFkEntity> items = SqlCore.GetEnumerable<SqlPluClipFkEntity>(sqlCrudConfig).ToList();
        if (items.Any())
        {
            SqlPluClipFkEntity pluClipFk = items.First();
            if (pluClipFk.Plu.IsNew)
                pluClipFk.Plu = SqlCore.GetItemNewEmpty<SqlPluEntity>();
            if (pluClipFk.Clip.IsNew)
                pluClipFk.Clip = SqlCore.GetItemNewEmpty<SqlClipEntity>();
        }
        if (sqlCrudConfig.IsResultOrder)
            items = items.OrderBy(item => item.Clip.Name);
        return items;
    }

    #endregion
}