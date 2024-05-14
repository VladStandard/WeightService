using Ws.Database.Nhibernate.Entities.Ref.Claims;
using Ws.Domain.Services.Aspects;
using Ws.Domain.Services.Features.Claim.Validators;

namespace Ws.Domain.Services.Features.Claim;

internal class ClaimService(SqlClaimRepository claimRepo) : IClaimService
{
    [Transactional]
    public IEnumerable<Models.Entities.Users.Claim> GetAll() => claimRepo.GetAll();

    [Transactional]
    public Models.Entities.Users.Claim GetItemByUid(Guid uid) => claimRepo.GetByUid(uid);

    [Transactional]
    public void Delete(Models.Entities.Users.Claim item) => claimRepo.Delete(item);

    [Transactional, Validate<ClaimNewValidator>]
    public Models.Entities.Users.Claim Create(Models.Entities.Users.Claim item) => claimRepo.Save(item);

    [Transactional, Validate<ClaimUpdateValidator>]
    public Models.Entities.Users.Claim Update(Models.Entities.Users.Claim item) => claimRepo.Update(item);
}