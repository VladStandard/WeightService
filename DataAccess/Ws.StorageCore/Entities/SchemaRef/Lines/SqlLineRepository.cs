using Ws.StorageCore.Entities.SchemaRef.Warehouses;

namespace Ws.StorageCore.Entities.SchemaRef.Lines;

public sealed class SqlLineRepository : SqlTableRepositoryBase<SqlLineEntity>
{ 
    public SqlLineEntity GetItemByPcName(string pcName)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.Equal(nameof(SqlLineEntity.PcName), pcName));
        return SqlCore.GetItemByCrud<SqlLineEntity>(sqlCrudConfig);
    }
    
    public SqlLineEntity GetItemByName(string name)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.Equal(nameof(SqlLineEntity.Name), name));
        return SqlCore.GetItemByCrud<SqlLineEntity>(sqlCrudConfig);
    }
    
    public IEnumerable<SqlLineEntity> GetLinesByWarehouse(SqlWarehouseEntity warehouse)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualFk(nameof(SqlLineEntity.Warehouse), warehouse));
        return GetEnumerable(sqlCrudConfig);
    }
    
    public IEnumerable<SqlLineEntity> GetEnumerable(SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.AddOrder(SqlOrder.Asc(nameof(SqlEntityBase.Name)));
        IEnumerable<SqlLineEntity> lines = SqlCore.GetEnumerable<SqlLineEntity>(sqlCrudConfig);
        return lines.OrderBy(item => item.Warehouse.Name);
    }
}