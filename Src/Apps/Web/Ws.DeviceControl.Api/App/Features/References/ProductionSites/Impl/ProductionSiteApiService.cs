using Ws.Database.EntityFramework.Entities.Ref.ProductionSites;
using Ws.DeviceControl.Api.App.Common;
using Ws.DeviceControl.Api.App.Features.References.ProductionSites.Common;
using Ws.DeviceControl.Api.App.Features.References.ProductionSites.Impl.Expressions;
using Ws.DeviceControl.Api.App.Features.References.ProductionSites.Impl.Extensions;
using Ws.DeviceControl.Api.App.Shared.Extensions;
using Ws.DeviceControl.Models.Dto.References.ProductionSites.Queries;
using Ws.DeviceControl.Models.Dto.References.ProductionSites.Commands.Create;
using Ws.DeviceControl.Models.Dto.References.ProductionSites.Commands.Update;

namespace Ws.DeviceControl.Api.App.Features.References.ProductionSites.Impl;

public class ProductionSiteApiService(
    WsDbContext dbContext,
    ProductionSiteCreateValidator createValidator,
    ProductionSiteUpdateValidator updateValidator
    ): ApiService, IProductionSiteService
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
        await ValidateAsync(dto, createValidator);
        await dbContext.ProductionSites.CheckUqAsync(i => i.Name == dto.Name, "Ошибка уникальности");

        ProductionSiteEntity item = dto.ToEntity();

        await dbContext.ProductionSites.AddAsync(item);
        await dbContext.SaveChangesAsync();

       return ProductionSiteExpressions.ToDto.Compile().Invoke(item);
    }

    public async Task<ProductionSiteDto> UpdateAsync(Guid id, ProductionSiteUpdateDto dto)
    {
        await ValidateAsync(dto, updateValidator);
        await dbContext.ProductionSites.CheckUqAsync(i => i.Name == dto.Name && i.Id != id, "Ошибка уникальности");

        ProductionSiteEntity productionSite = await dbContext.ProductionSites.SaveGetById(id, "Не найдено");
        dto.UpdateEntity(productionSite);
        await dbContext.SaveChangesAsync();

        return ProductionSiteExpressions.ToDto.Compile().Invoke(productionSite);
    }

    #endregion
}