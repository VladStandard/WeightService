using Ws.Domain.Models.Entities.Ref;
using Ws.StorageCore.Entities.Ref.Claims;
using Ws.StorageCore.Helpers;

namespace Ws.Services.Features.Claim;

internal class ClaimService : IClaimService
{
    public ClaimEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemByUid<ClaimEntity>(uid);
    
    public IEnumerable<ClaimEntity> GetAll() => new SqlClaimRepository().GetEnumerable();
}