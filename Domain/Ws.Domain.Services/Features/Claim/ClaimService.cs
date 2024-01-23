using Ws.Database.Core.Entities.Ref.Claims;
using Ws.Database.Core.Helpers;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Services.Features.Claim;

internal class ClaimService : IClaimService
{
    public ClaimEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemByUid<ClaimEntity>(uid);
    
    public IEnumerable<ClaimEntity> GetAll() => new SqlClaimRepository().GetEnumerable();
}