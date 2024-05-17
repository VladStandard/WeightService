using Ws.Domain.Models.Entities.Devices.Arms;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c.Plu;
using Ws.Domain.Services.Common.Commands;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.Arms;

public interface IArmService : IGetItemByUid<Arm>, ICreate<Arm>, IUpdate<Arm>, IDelete<Arm>
{
    public void DeletePluLine(ArmPlu item);
    void AddPluLine(ArmPlu armPlu);

    #region Queries

    public IEnumerable<Arm> GetAllByProductionSite(ProductionSite site);
    public Arm GetCurrentLine();
    public IEnumerable<Plu> GetLinePlus(Arm line);
    public IEnumerable<Plu> GetLineWeightPlus(Arm line);
    public IEnumerable<Plu> GetLinePiecePlus(Arm line);
    public IEnumerable<ArmPlu> GetLinePlusFk(Arm line);

    #endregion
}