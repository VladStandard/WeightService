using Ws.Database.Core.Entities.Ref.ZplResources;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Services.Features.ZplResource;

internal class ZplResourceService(SqlZplResourceRepository zplResourceRepo) : IZplResourceService
{
    public ZplResourceEntity GetItemByUid(Guid uid) => zplResourceRepo.GetByUid(uid);
    public IEnumerable<ZplResourceEntity> GetAll() => zplResourceRepo.GetAll();
}