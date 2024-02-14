using Ws.Database.Core.Entities.Ref.Claims;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Services.Features.Claim;

internal class ClaimService(SqlClaimRepository claimRepo) : IClaimService
{
    public IEnumerable<ClaimEntity> GetAll() => claimRepo.GetAll();
    public ClaimEntity GetItemByUid(Guid uid) => claimRepo.GetByUid(uid);
}