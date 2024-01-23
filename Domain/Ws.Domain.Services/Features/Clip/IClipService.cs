using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Common;

namespace Ws.Domain.Services.Features.Clip;

public interface IClipService : IUid<ClipEntity>, IAll<ClipEntity>, IUid1C<ClipEntity>
{
    ClipEntity GetDefault();
}