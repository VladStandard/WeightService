using Ws.Database.Core.Entities.Ref.Claims;
using Ws.Database.Core.Helpers;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Services.Features.Claim;

internal class ClaimService : IClaimService
{
    public IEnumerable<ClaimEntity> GetAll() => new SqlClaimRepository().GetAll();
    public ClaimEntity GetItemByUid(Guid uid) => SqlCoreHelper.GetItemById<ClaimEntity>(uid);
}