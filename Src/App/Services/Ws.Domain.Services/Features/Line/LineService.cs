using System.Net;
using Ws.Database.Nhibernate.Entities.Ref.Lines;
using Ws.Database.Nhibernate.Entities.Ref.PlusLines;
using Ws.Domain.Models.Entities.Devices.Arms;
using Ws.Domain.Models.Entities.Ref1c.Plu;
using Ws.Domain.Services.Aspects;
using Ws.Domain.Services.Features.Line.Validators;

namespace Ws.Domain.Services.Features.Line;

internal partial class LineService(SqlLineRepository lineRepo, SqlPluLineRepository pluLineRepo) : ILineService
{
    #region Queries

    [Transactional]
    public Arm GetCurrentLine() => lineRepo.GetByPcName(Dns.GetHostName());

    [Transactional]
    public Arm GetItemByUid(Guid uid) => lineRepo.GetByUid(uid);

    [Transactional]
    public IEnumerable<Arm> GetAllByProductionSite(Models.Entities.Ref.ProductionSite site)
        => lineRepo.GetAllByProductionSite(site);

    [Transactional]
    public IEnumerable<PluEntity> GetLinePlus(Arm line) => pluLineRepo.GetListByLine(line).Select(i => i.Plu);

    [Transactional]
    public IEnumerable<PluEntity> GetLineWeightPlus(Arm line) => GetPluEntitiesByWeightCheck(line, true);

    [Transactional]
    public IEnumerable<PluEntity> GetLinePiecePlus(Arm line) => GetPluEntitiesByWeightCheck(line, false);

    [Transactional]
    public IEnumerable<ArmLine> GetLinePlusFk(Arm line) => pluLineRepo.GetListByLine(line);

    #endregion

    #region Commands

    [Transactional, Validate<LineNewValidator>]
    public Arm Create(Arm line) => lineRepo.Save(line);

    [Transactional, Validate<LineUpdateValidator>]
    public Arm Update(Arm line) => lineRepo.Update(line);

    [Transactional]
    public void Delete(Arm item) => lineRepo.Delete(item);

    [Transactional]
    public void DeletePluLine(ArmLine item) => pluLineRepo.Delete(item);

    [Transactional]
    public void AddPluLine(ArmLine armLine) => pluLineRepo.Save(armLine);

    #endregion
}