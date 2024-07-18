using Ws.Database.EntityFramework.Entities.Ref1C.Bundles;
using Ws.DeviceControl.Api.App.Features.References1C.Bundles.Common;
using Ws.DeviceControl.Api.App.Features.References1C.Bundles.Impl.Expressions;

namespace Ws.DeviceControl.Api.App.Features.References1C.Bundles.Impl;

public class BundleApiService(WsDbContext dbContext) : IBundleService
{
    #region Queries

    public async Task<PackageDto> GetByIdAsync(Guid id)
    {
        BundleEntity? bundle = await dbContext.Bundles.FindAsync(id);
        if (bundle == null) throw new KeyNotFoundException();
        return BundleExpressions.ToDto.Compile().Invoke(bundle);
    }

    public Task<List<PackageDto>> GetAllAsync()
    {
        return dbContext.Bundles
            .AsNoTracking().Select(BundleExpressions.ToDto)
            .OrderBy(i => i.Weight).ThenBy(i => i.Name)
            .ToListAsync();
    }

    #endregion
}