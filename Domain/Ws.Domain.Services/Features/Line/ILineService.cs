using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Common;

namespace Ws.Domain.Services.Features.Line;

public interface ILineService : IUid<LineEntity>, IAll<LineEntity>
{
    public LineEntity GetCurrentLine();

    [Obsolete("Use GetLinePlus instead")]
    public IEnumerable<PluLineEntity> GetLinePlusFk(LineEntity line);
    
    public IEnumerable<PluEntity> GetLinePlus(LineEntity line);
    public IEnumerable<PluEntity> GetLineWeightPlus(LineEntity line);
    public IEnumerable<PluEntity> GetLinePiecePlus(LineEntity line);
    public IEnumerable<LineEntity> GetLinesByWarehouse(WarehouseEntity warehouse);
}