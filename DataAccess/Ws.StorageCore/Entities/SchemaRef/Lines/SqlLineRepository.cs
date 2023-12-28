using Ws.StorageCore.Entities.SchemaRef.Hosts;

namespace Ws.StorageCore.Entities.SchemaRef.Lines;

public sealed class SqlLineRepository : SqlTableRepositoryBase<SqlLineEntity>
{ 
    public SqlLineEntity GetItemByHost(SqlHostEntity host)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualFk(nameof(SqlLineEntity.Host), host));
        return SqlCore.GetItemByCrud<SqlLineEntity>(sqlCrudConfig);
    }
    
    public SqlLineEntity GetItemByName(string name)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.Equal(nameof(SqlLineEntity.Name), name));
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
            sqlCrudConfig.AddOrder(SqlOrder.Asc(nameof(SqlEntityBase.Name)));
        return SqlCore.GetEnumerable<SqlLineEntity>(sqlCrudConfig);
    }
}