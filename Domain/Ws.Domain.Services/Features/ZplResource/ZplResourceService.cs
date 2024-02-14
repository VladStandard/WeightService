using Ws.Database.Core.Entities.Ref.ZplResources;
using Ws.Domain.Models.Entities.Scale;

namespace Ws.Domain.Services.Features.ZplResource;

internal class ZplResourceService(SqlZplResourceRepository zplResourceRepo) : IZplResourceService
{
    public TemplateResourceEntity GetItemByUid(Guid uid) => zplResourceRepo.GetByUid(uid);
    public IEnumerable<TemplateResourceEntity> GetAll() => zplResourceRepo.GetAll();
}