using System.Net;
using Ws.Database.Nhibernate.Entities.Ref.Arms;
using Ws.Database.Nhibernate.Entities.Ref.PlusLines;
using Ws.Domain.Models.Entities.Devices.Arms;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c.Plu;
using Ws.Domain.Services.Aspects;
using Ws.Domain.Services.Features.Arms.Specs;
using Ws.Domain.Services.Features.Arms.Validators;
using Ws.Domain.Services.Features.Plus.Specs;

namespace Ws.Domain.Services.Features.Arms;

internal partial class ArmService(SqlArmRepository armRepo, SqlPluLineRepository pluLineRepo) : IArmService
{
    #region Items

    [Transactional]
    public Arm GetCurrentLine() => armRepo.GetItemBySpec(ArmSpecs.GetByPcName(Dns.GetHostName()));

    [Transactional]
    public Arm GetItemByUid(Guid uid) => armRepo.GetByUid(uid);

    #endregion

    #region List

    [Transactional]
    public IList<ArmPlu> GetLinePlusFk(Arm arm) => pluLineRepo.GetListByLine(arm);

    [Transactional]
    public IList<Plu> GetArmWeightPlus(Arm line) => GetPluListByArmAndSpec(line.Uid, PluSpecs.GetWeight());

    [Transactional]
    public IList<Arm> GetAllByProductionSite(ProductionSite site) =>
        armRepo.GetListBySpec(ArmSpecs.GetByProductionSite(site)).ToList();

    [Transactional]
    public IList<Plu> GetArmAllPlus(Arm arm) => pluLineRepo.GetListByLine(arm).Select(i => i.Plu).ToList();

    [Transactional]
    public IList<Plu> GetArmPiecePlus(Guid uid)  => GetPluListByArmAndSpec(uid, PluSpecs.GetPiece());

    #endregion

    #region CRUD

    [Transactional, Validate<ArmNewValidator>]
    public Arm Create(Arm line) => armRepo.Save(line);

    [Transactional, Validate<ArmUpdateValidator>]
    public Arm Update(Arm line) => armRepo.Update(line);

    [Transactional]
    public void Delete(Arm item) => armRepo.Delete(item);

    [Transactional]
    public void DeletePluLine(ArmPlu item) => pluLineRepo.Delete(item);

    [Transactional]
    public void AddPluLine(ArmPlu armPlu) => pluLineRepo.Save(armPlu);

    #endregion
}