using Ws.Domain.Models.Entities.Ref1c;
using Ws.Services.Common;

namespace Ws.Services.Features.Clip;

public interface IClipService : IUid<ClipEntity>, IAll<ClipEntity>, IUid1C<ClipEntity>
{
    ClipEntity GetDefault();
}