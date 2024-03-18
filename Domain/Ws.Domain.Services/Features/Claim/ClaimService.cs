using Ws.Database.Nhibernate.Entities.Ref.Claims;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Aspects;
using Ws.Domain.Services.Features.Claim.Validators;

namespace Ws.Domain.Services.Features.Claim;

internal class ClaimService(SqlClaimRepository claimRepo) : IClaimService
{
    [Transactional]
    public IEnumerable<ClaimEntity> GetAll() => claimRepo.GetAll();

    [Transactional]
    public ClaimEntity GetItemByUid(Guid uid) => claimRepo.GetByUid(uid);

    [Transactional]
    public void Delete(ClaimEntity item) => claimRepo.Delete(item);

    [Transactional, Validate<ClaimNewValidator>]
    public ClaimEntity Create(ClaimEntity item) => claimRepo.Save(item);

    [Transactional, Validate<ClaimUpdateValidator>]
    public ClaimEntity Update(ClaimEntity item) => claimRepo.Update(item);
}

