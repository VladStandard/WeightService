using Ws.Database.EntityFramework.Entities.Ref.ProductionSites;
using Ws.DeviceControl.Api.App.Features.References.ProductionSites.Common;
using Ws.DeviceControl.Api.App.Features.References.ProductionSites.Impl.Expressions;
using Ws.DeviceControl.Models.Dto.References.ProductionSites.Queries;
using Ws.DeviceControl.Models.Dto.References.ProductionSites.Commands.Create;
using Ws.DeviceControl.Models.Dto.References.ProductionSites.Commands.Update;

namespace Ws.DeviceControl.Api.App.Features.References.ProductionSites.Impl;

public class ProductionSiteApiService(WsDbContext dbContext): IProductionSiteService
{
    #region Queries

    public async Task<ProductionSiteDto> GetByIdAsync(Guid id)
    {
        ProductionSiteEntity? site = await dbContext.ProductionSites.FindAsync(id);
        if (site == null) throw new KeyNotFoundException();
        return ProductionSiteExpressions.ToDto.Compile().Invoke(site);
    }

    public Task<List<ProductionSiteDto>> GetAllAsync() => dbContext.ProductionSites
        .AsNoTracking().Select(ProductionSiteExpressions.ToDto)
        .OrderBy(i => i.Name)
        .ToListAsync();

    #endregion

    #region Commands

    public async Task<ProductionSiteDto> CreateAsync(ProductionSiteCreateDto dto)
    {
        ProductionSiteEntity item = new()
        {
            Name = dto.Name,
            Address = dto.Address,
        };
       await dbContext.ProductionSites.AddAsync(item);

       await dbContext.SaveChangesAsync();

       return ProductionSiteExpressions.ToDto.Compile().Invoke(item);
    }

    public Task<ProductionSiteDto> UpdateAsync(Guid id, ProductionSiteUpdateDto dto)
    {
        return Task.FromResult<ProductionSiteDto>(new()
        {
            Id = default,
            Name = string.Empty,
            Address = string.Empty,
            CreateDt = default,
            ChangeDt = default
        });
    }

    #endregion
}