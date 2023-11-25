using Ws.StorageCore.Entities.SchemaRef.Hosts;

namespace Ws.StorageCore.Entities.SchemaScale.Scales;

/// <summary>
/// Контроллер таблицы SCALES.
/// </summary>
public sealed class SqlLineRepository : SqlTableRepositoryBase<SqlLineEntity>
{

    #region Public and private methods

    public SqlLineEntity GetNewItem() => SqlCore.GetItemNewEmpty<SqlLineEntity>();

    public SqlLineEntity GetItemById(long id) => SqlCore.GetItemById<SqlLineEntity>(id);

    public SqlLineEntity GetItemByHost(SqlHostEntity host)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualFk(nameof(SqlLineEntity.Host), host));
        return SqlCore.GetItemByCrud<SqlLineEntity>(sqlCrudConfig);
    }
    
    public IEnumerable<SqlLineEntity> GetLinesByWorkshop(SqlWorkShopEntity workShop)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualFk(nameof(SqlLineEntity.WorkShop), workShop));
        return GetEnumerable(sqlCrudConfig);
    }


    public IEnumerable<SqlLineEntity> GetEnumerable(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.Asc(nameof(SqlEntityBase.Description)));
        return SqlCore.GetEnumerable<SqlLineEntity>(sqlCrudConfig);
    }

    public void Update(SqlLineEntity line) => SqlCore.Update(line);

    #endregion
}