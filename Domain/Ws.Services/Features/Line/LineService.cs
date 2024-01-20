using MDSoft.NetUtils;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.StorageCore.Entities.Ref.Lines;
using Ws.StorageCore.Entities.Ref.PlusLines;

namespace Ws.Services.Features.Line;

public class LineService : ILineService
{
    public IEnumerable<PluEntity> GetLinePlus(LineEntity line)
    {
       return new SqlPluLineRepository().GetListByLine(line).Select(i => i.Plu);
    }
    
    public IEnumerable<PluEntity> GetLineWeightPlus(LineEntity line)
    {
        return new SqlPluLineRepository().GetWeightListByLine(line).Select(i => i.Plu);
    }
    
    public IEnumerable<PluEntity> GetLinePiecePlus(LineEntity line)
    {
        return new SqlPluLineRepository().GetPieceListByLine(line).Select(i => i.Plu);
    }
    
    public IEnumerable<LineEntity> GetLinesByWarehouse(WarehouseEntity warehouse)
    {
        return new SqlLineRepository().GetLinesByWarehouse(warehouse);
    }
    
    public IEnumerable<LineEntity> GetLinesAll()
    {
        return new SqlLineRepository().GetAll();
    }

    public LineEntity GetCurrentLine()
    {
        return new SqlLineRepository().GetItemByPcName(MdNetUtils.GetLocalDeviceName(false));
    }
}
    