using System.Net;
using Ws.Database.Nhibernate.Entities.Ref.Lines;
using Ws.Database.Nhibernate.Entities.Ref.PlusLines;
using Ws.Domain.Models.Entities.Devices.Arms;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c.Plu;
using Ws.Domain.Services.Aspects;
using Ws.Domain.Services.Features.Arms.Specs;
using Ws.Domain.Services.Features.Arms.Validators;

namespace Ws.Domain.Services.Features.Arms;

internal partial class ArmService(SqlLineRepository lineRepo, SqlPluLineRepository pluLineRepo) : IArmService
{
    #region Queries

    [Transactional]
    public Arm GetCurrentLine() => lineRepo.GetItemBySpec(ArmSpecs.GetByPcName(Dns.GetHostName()));

    [Transactional]
    public Arm GetItemByUid(Guid uid) => lineRepo.GetByUid(uid);

    [Transactional]
    public IEnumerable<Arm> GetAllByProductionSite(ProductionSite site)
        => lineRepo.GetListBySpec(ArmSpecs.GetByProductionSite(site));

    [Transactional]
    public IEnumerable<Plu> GetLinePlus(Arm line) => pluLineRepo.GetListByLine(line).Select(i => i.Plu);

    [Transactional]
    public IEnumerable<Plu> GetLineWeightPlus(Arm line) => GetPluEntitiesByWeightCheck(line, true);

    [Transactional]
    public IEnumerable<Plu> GetLinePiecePlus(Arm line) => GetPluEntitiesByWeightCheck(line, false);

    [Transactional]
    public IEnumerable<ArmPlu> GetLinePlusFk(Arm line) => pluLineRepo.GetListByLine(line);

    #endregion

    #region Commands

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