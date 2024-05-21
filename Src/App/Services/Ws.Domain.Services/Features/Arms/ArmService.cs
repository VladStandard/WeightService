using System.Net;
using Ws.Database.Nhibernate.Entities.Ref.Lines;
using Ws.Database.Nhibernate.Entities.Ref.PlusLines;
using Ws.Domain.Models.Entities.Devices.Arms;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c.Plu;
using Ws.Domain.Services.Aspects;
using Ws.Domain.Services.Features.Arms.Specs;
using Ws.Domain.Services.Features.Arms.Validators;
using Ws.Domain.Services.Features.Plus.Specs;

namespace Ws.Domain.Services.Features.Arms;

internal partial class ArmService(SqlLineRepository lineRepo, SqlPluLineRepository pluLineRepo) : IArmService
{
    #region Items

    [Transactional]
    public Arm GetCurrentLine() => lineRepo.GetItemBySpec(ArmSpecs.GetByPcName(Dns.GetHostName()));

    [Transactional]
    public Arm GetItemByUid(Guid uid) => lineRepo.GetByUid(uid);

    #endregion

    #region List

    [Transactional]
    public IList<Arm> GetAllByProductionSite(ProductionSite site) =>
        lineRepo.GetListBySpec(ArmSpecs.GetByProductionSite(site)).ToList();

    [Transactional]
    public IList<Plu> GetArmWeightPlus(Arm arm) => GetPluListByArmAndSpec(arm, PluSpecs.GetWeight());

    [Transactional]
    public IList<Plu> GetArmPiecePlus(Arm arm) => GetPluListByArmAndSpec(arm, PluSpecs.GetPiece());

    [Transactional]
    public IList<Plu> GetArmAllPlus(Arm arm) => pluLineRepo.GetListByLine(arm).Select(i => i.Plu).ToList();

    [Transactional]
    public IList<ArmPlu> GetLinePlusFk(Arm arm) => pluLineRepo.GetListByLine(arm);

    #endregion

    #region CRUD

    [Transactional, Validate<ArmNewValidator>]
    public Arm Create(Arm line) => lineRepo.Save(line);

    [Transactional, Validate<ArmUpdateValidator>]
    public Arm Update(Arm line) => lineRepo.Update(line);

    [Transactional]
    public void Delete(Arm item) => lineRepo.Delete(item);

    [Transactional]
    public void DeletePluLine(ArmPlu item) => pluLineRepo.Delete(item);

    [Transactional]
    public void AddPluLine(ArmPlu armPlu) => pluLineRepo.Save(armPlu);

    #endregion
}