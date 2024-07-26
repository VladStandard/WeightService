using Ws.Database.EntityFramework.Entities.Ref1C.Plus;
using Ws.DeviceControl.Api.App.Features.References.Templates.Common;
using Ws.DeviceControl.Api.App.Features.References.Templates.Impl.Expressions;

namespace Ws.DeviceControl.Api.App.Features.References.Templates.Impl;

public class TemplateApiService(
    WsDbContext dbContext
    ): ApiService, ITemplateService
{
    #region Queries

    public async Task<List<ProxyDto>> GetProxiesByPluAsync(Guid pluId)
    {
        PluEntity pluEntity = await dbContext.Plus.SafeGetById(pluId, "Не найдено");
        bool isWeight = pluEntity.IsWeight;

        return await dbContext.Templates
            .AsNoTracking()
            .Where(i => i.IsWeight == isWeight)
            .Select(TemplatesExpressions.ToProxy)
            .OrderBy(i => i.Name)
            .ToListAsync();
    }

    #endregion

}