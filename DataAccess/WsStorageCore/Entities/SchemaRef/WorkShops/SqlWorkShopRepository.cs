namespace WsStorageCore.Entities.SchemaRef.WorkShops;

public sealed class SqlWorkShopRepository : SqlTableRepositoryBase<SqlWorkShopEntity>
{
    #region Public and private methods

    public SqlWorkShopEntity GetNewItem() => SqlCore.GetItemNewEmpty<SqlWorkShopEntity>();

    public IEnumerable<SqlWorkShopEntity> GetEnumerable(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<SqlWorkShopEntity>(sqlCrudConfig);
    }

    #endregion
}