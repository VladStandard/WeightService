using MDSoft.NetUtils;
using Ws.StorageCore.Entities.SchemaRef.Lines;
using Ws.StorageCore.Entities.SchemaRef.PlusLines;
using Ws.StorageCore.Entities.SchemaRef.WorkShops;
using Ws.StorageCore.Entities.SchemaRef1c.Plus;

namespace Ws.Services.Services.Line;

public class LineService : ILineService
{
    public IEnumerable<SqlPluEntity> GetLinePlus(SqlLineEntity line)
    {
       return new SqlPluLineRepository().GetListByLine(line).Select(i => i.Plu);
    }
    
    public IEnumerable<SqlPluEntity> GetLineWeightPlus(SqlLineEntity line)
    {
        return new SqlPluLineRepository().GetWeightListByLine(line).Select(i => i.Plu);
    }
    
    public IEnumerable<SqlPluEntity> GetLinePiecePlus(SqlLineEntity line)
    {
        return new SqlPluLineRepository().GetPieceListByLine(line).Select(i => i.Plu);
    }
    
    public IEnumerable<SqlLineEntity> GetLinesByWorkshop(SqlWorkShopEntity workShop)
    {
        return new SqlLineRepository().GetLinesByWorkshop(workShop);
    }

    public SqlLineEntity GetCurrentLine()
    {
        return new SqlLineRepository().GetItemByPcName(MdNetUtils.GetLocalDeviceName(false));
    }
}
    