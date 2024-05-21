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

    public Arm GetCurrentLine();
    public IList<Plu> GetArmAllPlus(Arm line);
    public IList<Plu> GetArmPiecePlus(Arm line);
    public IList<Plu> GetArmWeightPlus(Arm line);
    public IList<ArmPlu> GetLinePlusFk(Arm line);
    public IList<Arm> GetAllByProductionSite(ProductionSite site);

    #endregion
}