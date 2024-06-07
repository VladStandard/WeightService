using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.Boxes;

public interface IBoxService : IGetItemByUid<Box>, IGetAll<Box>;