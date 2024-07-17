using Ws.Domain.Models.Entities.Print;

namespace Ws.Domain.Services.Features.ZplResources;

public interface IZplResourceService : IGetItemByUid<ZplResource>, IGetAll<ZplResource>,
    ICreate<ZplResource>, IUpdate<ZplResource>
{
    void Delete(Guid id, string name);
}