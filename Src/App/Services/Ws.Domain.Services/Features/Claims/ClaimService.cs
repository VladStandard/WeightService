using Ws.Database.Nhibernate.Entities.Ref.Claims;
using Ws.Domain.Models.Entities.Users;
using Ws.Domain.Services.Aspects;
using Ws.Domain.Services.Features.Claims.Validators;

namespace Ws.Domain.Services.Features.Claims;

internal class ClaimService(SqlClaimRepository claimRepo) : IClaimService
{
    [Transactional]
    public IEnumerable<Claim> GetAll() => claimRepo.GetAll();

    [Transactional]
    public Claim GetItemByUid(Guid uid) => claimRepo.GetByUid(uid);

    [Transactional]
    public void Delete(Claim item) => claimRepo.Delete(item);

    [Transactional, Validate<ClaimNewValidator>]
    public Claim Create(Claim item) => claimRepo.Save(item);

    [Transactional, Validate<ClaimUpdateValidator>]
    public Claim Update(Claim item) => claimRepo.Update(item);
}