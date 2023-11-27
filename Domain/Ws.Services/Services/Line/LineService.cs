using Ws.StorageCore.Entities.SchemaRef.WorkShops;
using Ws.StorageCore.Entities.SchemaRef1c.Plus;
using Ws.StorageCore.Entities.SchemaScale.PlusScales;
using Ws.StorageCore.Entities.SchemaScale.Scales;

namespace Ws.Services.Services.Line;

public class LineService : ILineService
{
    public IEnumerable<SqlPluEntity> GetLinePlus(SqlLineEntity line)
    {
       return new SqlPluLineRepository().GetListByLine(line, new())
           .Select(i => i.Plu).OrderBy(item => item.Number);
    }
    
    public IEnumerable<SqlLineEntity> GetLinesByWorkshop(SqlWorkShopEntity workShop)
    {
        return new SqlLineRepository().GetLinesByWorkshop(workShop);
    }
}
    