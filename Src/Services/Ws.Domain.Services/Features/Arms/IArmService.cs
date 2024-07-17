using Ws.Domain.Models.Entities.Devices.Arms;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c.Plus;

namespace Ws.Domain.Services.Features.Arms;

public interface IArmService : IGetItemByUid<Arm>, ICreate<Arm>, IUpdate<Arm>, IDelete<Guid>
{
    #region Queries

    public IList<Plu> GetArmAllPlus(Arm line);
    public IList<Plu> GetArmPiecePlus(Guid uid);
    public IList<Arm> GetAllByProductionSite(ProductionSite site);

    #endregion

    #region CRUD

    public void DeletePlu(Arm arm, Plu plu);
    void AddPlu(Arm arm, Plu plu);

    #endregion
}