namespace WsStorageCore.Entities.SchemaScale.PlusFks;

/// <summary>
/// SQL-контроллер таблицы PLUS_BUNDLES_FK.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlPluFkRepository : WsSqlTableRepositoryBase<WsSqlPluFkEntity>
{
    #region Public and private fields, properties, constructor

    private WsSqlPluRepository ContextPlu { get; } = new();

    #endregion

    #region Public and private methods

    public WsSqlPluFkEntity GetNewItem()
    {
        WsSqlPluFkEntity item = SqlCore.GetItemNewEmpty<WsSqlPluFkEntity>();
        item.Plu = ContextPlu.GetNewItem();
        item.Parent = ContextPlu.GetNewItem();
        item.Category = null;
        return item;
    }

    public IEnumerable<WsSqlPluFkEntity> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        IEnumerable<WsSqlPluFkEntity> items = SqlCore.GetEnumerable<WsSqlPluFkEntity>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder)
            items = items.OrderBy(item => item.Plu.Number);
        return items.ToList();
    }
    
    public WsSqlPluFkEntity GetByPlu(WsSqlPluEntity plu)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualFk(nameof(WsSqlPluStorageMethodFkEntity.Plu), plu));
        return SqlCore.GetItemByCrud<WsSqlPluFkEntity>(sqlCrudConfig);
    }
    
    #endregion
}