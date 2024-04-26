using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Common.Commands;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.Line;

public interface ILineService : IGetItemByUid<LineEntity>, ICreate<LineEntity>, IUpdate<LineEntity>, IDelete<LineEntity>
{
    public void DeletePluLine(PluLineEntity item);
    void AddPluLine(PluLineEntity pluLine);

    #region Queries

    public IEnumerable<LineEntity> GetAllByProductionSite(ProductionSiteEntity site);
    public LineEntity GetCurrentLine();
    public IEnumerable<PluEntity> GetLinePlus(LineEntity line);
    public IEnumerable<PluEntity> GetLineWeightPlus(LineEntity line);
    public IEnumerable<PluEntity> GetLinePiecePlus(LineEntity line);
    public IEnumerable<PluLineEntity> GetLinePlusFk(LineEntity line);

    #endregion
}