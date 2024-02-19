using Ws.Database.Core.Entities.Ref.ZplResources;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.ZplResource;

internal class ZplResourceService(SqlZplResourceRepository zplResourceRepo) : IZplResourceService
{
    [Session] public ZplResourceEntity GetItemByUid(Guid uid) => zplResourceRepo.GetByUid(uid);
    [Session] public IEnumerable<ZplResourceEntity> GetAll() => zplResourceRepo.GetAll();
}