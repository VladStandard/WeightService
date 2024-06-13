using Ws.Domain.Models.Entities.Users;
using Ws.Domain.Services.Common.Commands;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.Claims;

public interface IClaimService : IGetItemByUid<Claim>, IGetAll<Claim>, ICreate<Claim>,
    IUpdate<Claim>, IDelete<Claim>;