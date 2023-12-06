namespace Ws.StorageCore.Entities.SchemaRef1c.Plus;

/// <summary>
/// SQL-контроллер таблицы PLUS.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class SqlPluRepository : SqlTableRepositoryBase<SqlPluEntity>
{
    private SqlViewPluLineRepository PluLineRepository { get; } = new();
    
    #region Public and private methods

    /// <summary>
    /// Получить ПЛУ по полю UID_1C.
    /// </summary>
    public SqlPluEntity GetItemByUid1C(Guid uid1C)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualUid1C(uid1C));
        return SqlCore.GetItemByCrud<SqlPluEntity>(sqlCrudConfig);
    }

    public SqlPluEntity GetNewItem() => SqlCore.GetItemNewEmpty<SqlPluEntity>();

    public IEnumerable<SqlPluEntity> GetEnumerable() => GetEnumerable(SqlCrudConfigFactory.GetCrudAll());

    public IEnumerable<SqlPluEntity> GetEnumerable(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.Asc(nameof(SqlPluEntity.Number)));
        return SqlCore.GetEnumerable<SqlPluEntity>(sqlCrudConfig);
    }
    
    public IEnumerable<SqlPluEntity> GetEnumerableByNumber(short number)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.Equal(nameof(SqlPluEntity.Number), number));
        return GetEnumerable(sqlCrudConfig);
    }

    /// <summary>
    /// Получить ПЛУ по UID_1C.
    /// </summary>
    public SqlPluEntity GetByUid1C(Guid uid)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.Equal(nameof(SqlPluEntity.Uid1C), uid));
        return SqlCore.GetItemByCrud<SqlPluEntity>(sqlCrudConfig);
    }

    #endregion
}