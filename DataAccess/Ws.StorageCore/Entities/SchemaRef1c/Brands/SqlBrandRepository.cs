using System;

namespace Ws.StorageCore.Entities.SchemaRef1c.Brands;

/// <summary>
/// SQL-контроллер таблицы брендов.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class SqlBrandRepository : SqlTableRepositoryBase<SqlBrandEntity>
{
    #region Public and private methods

    public SqlBrandEntity GetNewItem() => SqlCore.GetItemNewEmpty<SqlBrandEntity>();

    public IEnumerable<SqlBrandEntity> GetEnumerable(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<SqlBrandEntity>(sqlCrudConfig);
    }

    public SqlBrandEntity GetItemByUid1C(Guid uid1C)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualUid1C(uid1C));
        return SqlCore.GetItemByCrud<SqlBrandEntity>(sqlCrudConfig);
    }

    #endregion
}