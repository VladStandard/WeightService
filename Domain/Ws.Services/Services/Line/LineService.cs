using Ws.StorageCore.Entities.SchemaRef.PlusLines;
using Ws.StorageCore.Entities.SchemaRef.WorkShops;
using Ws.StorageCore.Entities.SchemaRef1c.Plus;
using Ws.StorageCore.Entities.SchemaScale.Scales;
using Ws.StorageCore.Models;
using Ws.StorageCore.OrmUtils;

namespace Ws.Services.Services.Line;

public class LineService : ILineService
{
    public IEnumerable<SqlPluEntity> GetLinePlus(SqlLineEntity line)
    {
       return new SqlPluLineRepository().GetListByLine(line, new())
           .Select(i => i.Plu).Where(plu => plu.IsGroup == false).OrderBy(item => item.Number);
    }
    
    public IEnumerable<SqlLineEntity> GetLinesByWorkshop(SqlWorkShopEntity workShop)
    {
        return new SqlLineRepository().GetLinesByWorkshop(workShop);
    }
}
    