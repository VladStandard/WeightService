using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.Bundle;

public interface IBundleService : IGetAll<BundleEntity>, IGetItemByUid<BundleEntity>;