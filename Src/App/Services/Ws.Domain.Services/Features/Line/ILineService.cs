using Ws.Domain.Models.Entities.Devices.Arms;
using Ws.Domain.Models.Entities.Ref1c.Plu;
using Ws.Domain.Services.Common.Commands;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.Line;

public interface ILineService : IGetItemByUid<Arm>, ICreate<Arm>, IUpdate<Arm>, IDelete<Arm>
{
    public void DeletePluLine(ArmLine item);
    void AddPluLine(ArmLine armLine);

    #region Queries

    public IEnumerable<Arm> GetAllByProductionSite(Models.Entities.Ref.ProductionSite site);
    public Arm GetCurrentLine();
    public IEnumerable<PluEntity> GetLinePlus(Arm line);
    public IEnumerable<PluEntity> GetLineWeightPlus(Arm line);
    public IEnumerable<PluEntity> GetLinePiecePlus(Arm line);
    public IEnumerable<ArmLine> GetLinePlusFk(Arm line);

    #endregion
}