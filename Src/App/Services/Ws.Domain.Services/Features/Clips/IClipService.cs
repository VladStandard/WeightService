using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.Clips;

public interface IClipService : IGetItemByUid<Clip>, IGetAll<Clip>;