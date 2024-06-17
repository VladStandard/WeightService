using Ws.Domain.Models.Entities.Devices.Arms;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c.Plu;
using Ws.Domain.Services.Common.Commands;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.Arms;

public interface IArmService : IGetItemByUid<Arm>, ICreate<Arm>, IUpdate<Arm>, IDelete<Arm>
{
    #region Queries

    public IList<Plu> GetArmAllPlus(Arm line);
    public IList<Plu> GetArmPiecePlus(Guid uid);
    public IList<Arm> GetAllByProductionSite(ProductionSite site);
    public IList<ArmPlu> GetLinePlusFk(Arm line);

    #endregion

    #region CRUD

    public void DeletePluLine(ArmPlu item);
    void AddPluLine(ArmPlu armPlu);

    #endregion
}