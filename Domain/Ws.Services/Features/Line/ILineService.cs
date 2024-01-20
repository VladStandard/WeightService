using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Services.Features.Line;

public interface ILineService
{
    public LineEntity GetCurrentLine();
    public IEnumerable<PluEntity> GetLinePlus(LineEntity line);
    public IEnumerable<PluEntity> GetLineWeightPlus(LineEntity line);
    public IEnumerable<PluEntity> GetLinePiecePlus(LineEntity line);
    public IEnumerable<LineEntity> GetLinesByWarehouse(WarehouseEntity warehouse);
    public IEnumerable<LineEntity> GetLinesAll();
}