using Ws.Database.EntityFramework.Entities.Ref1C.Brands;
using Ws.DeviceControl.Api.App.Features.References1C.Brands.Common;
using Ws.DeviceControl.Api.App.Features.References1C.Brands.Impl.Expressions;
using Ws.DeviceControl.Models.Dto.References1C.Brands;

namespace Ws.DeviceControl.Api.App.Features.References1C.Brands.Impl;

public class BrandApiService(WsDbContext dbContext) : IBrandService
{
    #region Queries

    public async Task<BrandDto> GetByIdAsync(Guid id)
    {
        BrandEntity? brand = await dbContext.Brands.FindAsync(id);
        if (brand == null) throw new KeyNotFoundException();
        return BrandExpressions.ToDto.Compile().Invoke(brand);
    }

    public Task<List<BrandDto>> GetAllAsync()
    {
        return dbContext.Brands
            .AsNoTracking().Select(BrandExpressions.ToDto)
            .OrderBy(i => i.Name)
            .ToListAsync();
    }

    #endregion
}