using Ws.StorageCore.Entities.SchemaRef.WorkShops;
using Ws.StorageCore.Entities.SchemaRef1c.Plus;
using Ws.StorageCore.Entities.SchemaScale.PlusScales;
using Ws.StorageCore.Entities.SchemaScale.Scales;
using Ws.StorageCore.Models;
using Ws.StorageCore.OrmUtils;

namespace Ws.Services.Services.Line;

public class LineService : ILineService
{
    public IEnumerable<SqlPluEntity> GetLinePlus(SqlLineEntity line)
    {
        SqlCrudConfigModel crud = new();
        crud.AddFilter(SqlRestrictions.Equal(nameof(SqlPluScaleEntity.IsActive), true));
       return new SqlPluLineRepository().GetListByLine(line, crud)
           .Select(i => i.Plu).Where(plu => plu.IsGroup == false).OrderBy(item => item.Number);
    }
    
    public IEnumerable<SqlLineEntity> GetLinesByWorkshop(SqlWorkShopEntity workShop)
    {
        return new SqlLineRepository().GetLinesByWorkshop(workShop);
    }
}
    