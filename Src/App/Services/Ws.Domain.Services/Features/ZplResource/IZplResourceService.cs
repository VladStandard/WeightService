using Ws.Domain.Services.Common.Commands;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.ZplResource;

public interface IZplResourceService : IGetItemByUid<Models.Entities.Print.ZplResource>, IGetAll<Models.Entities.Print.ZplResource>,
    ICreate<Models.Entities.Print.ZplResource>, IUpdate<Models.Entities.Print.ZplResource>, IDelete<Models.Entities.Print.ZplResource>
{
    Dictionary<string, string> GetAllResourcesFromCacheOrDb();
}