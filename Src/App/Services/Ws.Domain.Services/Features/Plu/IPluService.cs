using Ws.Domain.Models.Entities.Ref1c.Plu;
using Ws.Domain.Services.Common.Commands;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.Plu;

public interface IPluService : IGetItemByUid<PluEntity>, IGetAll<PluEntity>, IUpdate<PluEntity>;