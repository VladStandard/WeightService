using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Common;

namespace Ws.Domain.Services.Features.Bundle;

public interface IBundleService : IAll<BundleEntity>, IUid<BundleEntity>, IUid1C<BundleEntity>
{
    BundleEntity GetDefault();
}