using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.Bundles;

public interface IBundleService : IGetAll<Bundle>, IGetItemByUid<Bundle>;