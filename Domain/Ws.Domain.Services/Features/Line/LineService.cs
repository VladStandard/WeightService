using System.Net;
using Ws.Database.Nhibernate.Entities.Ref.Lines;
using Ws.Database.Nhibernate.Entities.Ref.PlusLines;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Aspects;
using Ws.Domain.Services.Features.Line.Validators;

namespace Ws.Domain.Services.Features.Line;

internal partial class LineService(SqlLineRepository lineRepo, SqlPluLineRepository pluLineRepo) : ILineService
{
    #region Queries

    [Transactional]
    public LineEntity GetCurrentLine() => lineRepo.GetByPcName(Dns.GetHostName());

    [Transactional]
    public LineEntity GetItemByUid(Guid uid) => lineRepo.GetByUid(uid);

    [Transactional]
    public IEnumerable<LineEntity> GetAll() => lineRepo.GetAll();

    [Transactional]
    public IEnumerable<LineEntity> GetAllByProductionSite(ProductionSiteEntity site)
        => lineRepo.GetAllByProductionSite(site);

    [Transactional]
    public IEnumerable<PluEntity> GetLinePlus(LineEntity line) => pluLineRepo.GetListByLine(line).Select(i => i.Plu);

    [Transactional]
    public IEnumerable<PluEntity> GetLineWeightPlus(LineEntity line) => GetPluEntitiesByWeightCheck(line, true);

    [Transactional]
    public IEnumerable<PluEntity> GetLinePiecePlus(LineEntity line) => GetPluEntitiesByWeightCheck(line, false);

    [Transactional]
    public IEnumerable<PluLineEntity> GetLinePlusFk(LineEntity line) => pluLineRepo.GetListByLine(line);

    #endregion

    #region Commands

    [Transactional, Validate<LineNewValidator>]
    public LineEntity Create(LineEntity line) => lineRepo.Save(line);

    [Transactional, Validate<LineUpdateValidator>]
    public LineEntity Update(LineEntity line) => lineRepo.Update(line);

    [Transactional]
    public void Delete(LineEntity item) => lineRepo.Delete(item);

    [Transactional]
    public void DeletePluLine(PluLineEntity item) => pluLineRepo.Delete(item);

    [Transactional]
    public void AddPluLine(PluLineEntity pluLine) => pluLineRepo.Save(pluLine);

    #endregion
}