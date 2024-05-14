using Ws.Domain.Services.Common.Commands;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.Claim;

public interface IClaimService : IGetItemByUid<Models.Entities.Users.Claim>, IGetAll<Models.Entities.Users.Claim>, ICreate<Models.Entities.Users.Claim>,
    IUpdate<Models.Entities.Users.Claim>, IDelete<Models.Entities.Users.Claim>;