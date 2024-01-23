using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Common;

namespace Ws.Domain.Services.Features.Box;

public interface IBoxService : IUid<BoxEntity>, IAll<BoxEntity>, IUid1C<BoxEntity>
{
    BoxEntity GetDefaultForCharacteristic();
    BoxEntity GetDefault();
}