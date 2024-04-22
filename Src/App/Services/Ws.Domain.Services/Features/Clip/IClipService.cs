using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.Clip;

public interface IClipService : IGetItemByUid<ClipEntity>, IGetAll<ClipEntity>;