using MDSoft.NetUtils;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.StorageCore.Entities.Ref.Lines;
using Ws.StorageCore.Entities.Ref.PlusLines;
using Ws.StorageCore.Helpers;

namespace Ws.Services.Features.Line;

internal class LineService : ILineService
{
    public IEnumerable<LineEntity> GetAll() => new SqlLineRepository().GetAll();
    
    public LineEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemByUid<LineEntity>(uid);
    
    public IEnumerable<PluEntity> GetLinePlus(LineEntity line)
    {
       return new SqlPluLineRepository().GetListByLine(line).Select(i => i.Plu);
    }
    
    [Obsolete("Use GetLinePlus instead")]
    public IEnumerable<PluLineEntity> GetLinePlusFk(LineEntity line)
    {
        return new SqlPluLineRepository().GetListByLine(line);
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

    public LineEntity GetCurrentLine()
    {
        return new SqlLineRepository().GetItemByPcName(MdNetUtils.GetLocalDeviceName(false));
    }
}
    