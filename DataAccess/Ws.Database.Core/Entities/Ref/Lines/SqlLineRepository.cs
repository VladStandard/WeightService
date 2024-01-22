using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.Lines;

public sealed class SqlLineRepository : SqlTableRepositoryBase<LineEntity>
{
    private IEnumerable<LineEntity>GetEnumerable(SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.AddOrder(SqlOrder.Asc(nameof(LineEntity.Name)));
        IEnumerable<LineEntity> lines = SqlCore.GetEnumerable<LineEntity>(sqlCrudConfig);
        return lines.OrderBy(item => item.Warehouse.Name);
    }
    
    public IEnumerable<LineEntity> GetAll() => GetEnumerable(new());
    
    public LineEntity GetItemByPcName(string pcName)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.Equal(nameof(LineEntity.PcName), pcName));
        return SqlCore.GetItemByCrud<LineEntity>(sqlCrudConfig);
    }
    
    public LineEntity GetItemByName(string name)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.Equal(nameof(LineEntity.Name), name));
        return SqlCore.GetItemByCrud<LineEntity>(sqlCrudConfig);
    }
    
    public IEnumerable<LineEntity> GetLinesByWarehouse(WarehouseEntity warehouse)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualFk(nameof(LineEntity.Warehouse), warehouse));
        return GetEnumerable(sqlCrudConfig);
    }
}