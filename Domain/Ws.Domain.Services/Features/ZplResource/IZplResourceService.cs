using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Common.Commands;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.ZplResource;

public interface IZplResourceService : IGetItemByUid<ZplResourceEntity>, IGetAll<ZplResourceEntity>, 
    ICreate<ZplResourceEntity>, IUpdate<ZplResourceEntity>
{
    Dictionary<string, string> GetAllCachedResources();
};