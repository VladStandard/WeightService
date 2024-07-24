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

    public async Task<ProductionSiteDto> GetByIdAsync(Guid id) =>
        ProductionSiteExpressions.ToDto.Compile().Invoke(await dbContext.ProductionSites.SafeGetById(id, "Не найдено"));

    public Task<List<ProductionSiteDto>> GetAllAsync() => dbContext.ProductionSites
        .AsNoTracking().Select(ProductionSiteExpressions.ToDto)
        .OrderBy(i => i.Name)
        .ToListAsync();

    #endregion

    #region Commands

    public async Task<ProductionSiteDto> CreateAsync(ProductionSiteCreateDto dto)
    {
        await ValidateAsync(dto, createValidator);
        await dbContext.ProductionSites.SafeExistAsync(i => i.Name == dto.Name, "Ошибка уникальности");
        await dbContext.ProductionSites.SafeExistAsync(i => i.Address == dto.Address, "Ошибка уникальности");

        ProductionSiteEntity entity = dto.ToEntity();

        await dbContext.ProductionSites.AddAsync(entity);
        await dbContext.SaveChangesAsync();

       return ProductionSiteExpressions.ToDto.Compile().Invoke(entity);
    }

    public async Task<ProductionSiteDto> UpdateAsync(Guid id, ProductionSiteUpdateDto dto)
    {
        await ValidateAsync(dto, updateValidator);
        await dbContext.ProductionSites.SafeExistAsync(i => i.Name == dto.Name && i.Id != id, "Ошибка уникальности");
        await dbContext.ProductionSites.SafeExistAsync(i => i.Address == dto.Address && i.Id != id, "Ошибка уникальности");

        ProductionSiteEntity entity = await dbContext.ProductionSites.SafeGetById(id, "Не найдено");
        dto.UpdateEntity(entity);
        await dbContext.SaveChangesAsync();

        return ProductionSiteExpressions.ToDto.Compile().Invoke(entity);
    }

    #endregion
}