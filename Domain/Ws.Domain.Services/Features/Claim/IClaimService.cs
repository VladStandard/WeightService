using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Common.Commands;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.Claim;

public interface IClaimService : IGetItemByUid<ClaimEntity>, IGetAll<ClaimEntity>, ICreate<ClaimEntity>,
    IUpdate<ClaimEntity>, IDelete<ClaimEntity>;