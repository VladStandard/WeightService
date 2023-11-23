using Ws.StorageCore.Entities.SchemaRef.WorkShops;
using Ws.StorageCore.Entities.SchemaRef1c.Plus;
using Ws.StorageCore.Entities.SchemaScale.Scales;

namespace Ws.Services.Services.Line;

public interface ILineService
{
    public IEnumerable<SqlPluEntity> GetLinePlus(SqlScaleEntity line);
    public IEnumerable<SqlScaleEntity> GetLinesByWorkshop(SqlWorkShopEntity workShop);
}