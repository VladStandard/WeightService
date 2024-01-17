using Ws.StorageCore.Entities.SchemaRef.Lines;
using Ws.StorageCore.Entities.SchemaRef.Warehouses;
using Ws.StorageCore.Entities.SchemaRef1c.Plus;

namespace Ws.Services.Services.Line;

public interface ILineService
{
    public SqlLineEntity GetCurrentLine();
    public IEnumerable<SqlPluEntity> GetLinePlus(SqlLineEntity line);
    public IEnumerable<SqlPluEntity> GetLineWeightPlus(SqlLineEntity line);
    public IEnumerable<SqlPluEntity> GetLinePiecePlus(SqlLineEntity line);
    public IEnumerable<SqlLineEntity> GetLinesByWarehouse(SqlWarehouseEntity warehouse);
}