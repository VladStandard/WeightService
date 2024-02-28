using EasyCaching.Core;
using Ws.Database.Core.Entities.Ref.ZplResources;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Aspects;
using Ws.Domain.Services.Features.ZplResource.Validators;

namespace Ws.Domain.Services.Features.ZplResource;

internal partial class ZplResourceService(SqlZplResourceRepository zplResourceRepo, IRedisCachingProvider provider) : IZplResourceService
{
    [Transactional] 
    public ZplResourceEntity GetItemByUid(Guid uid) => zplResourceRepo.GetByUid(uid);
    
    [Transactional] 
    public IEnumerable<ZplResourceEntity> GetAll() => zplResourceRepo.GetAll().ToList();
    
    [Transactional, Validate<ZplResourceNewValidator>] 
    public ZplResourceEntity Create(ZplResourceEntity item) => UpdateCache(zplResourceRepo.Save(item));
    
    [Transactional, Validate<ZplResourceUpdateValidator>]
    public ZplResourceEntity Update(ZplResourceEntity item) => UpdateCache(zplResourceRepo.Update(item));
    
    public Dictionary<string, string> GetAllCachedResources()
    {
        Dictionary<String, String>? cached = provider.HGetAll("ZPL_RESOURCES");

        if (cached != null && cached.Any()) return cached;
        
        cached = GetAll().ToDictionary(i => i.Name, i => i.Zpl);
        provider.HMSet("ZPL_RESOURCES", cached, TimeSpan.FromHours(1));
        
        return cached;
    }
}