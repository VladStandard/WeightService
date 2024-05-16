using Ws.Domain.Models.Entities.Ref1c.Plu;
using Ws.Domain.Services.Common.Commands;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.Plus;

public interface IPluService : IGetItemByUid<Plu>, IGetAll<Plu>, IUpdate<Plu>;