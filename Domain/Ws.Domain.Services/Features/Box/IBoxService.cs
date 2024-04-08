using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.Box;

public interface IBoxService : IGetItemByUid<BoxEntity>, IGetAll<BoxEntity>;