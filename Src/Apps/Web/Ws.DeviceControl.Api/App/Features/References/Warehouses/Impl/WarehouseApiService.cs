using Ws.Database.EntityFramework.Entities.Ref.ProductionSites;
using Ws.Database.EntityFramework.Entities.Ref.Warehouses;
using Ws.DeviceControl.Api.App.Features.References.Warehouses.Common;
using Ws.DeviceControl.Api.App.Features.References.Warehouses.Impl.Expressions;
using Ws.DeviceControl.Api.App.Features.References.Warehouses.Impl.Extensions;
using Ws.DeviceControl.Models.Features.References.Warehouses.Commands.Create;
using Ws.DeviceControl.Models.Features.References.Warehouses.Commands.Update;
using Ws.DeviceControl.Models.Features.References.Warehouses.Queries;

namespace Ws.DeviceControl.Api.App.Features.References.Warehouses.Impl;

public class WarehouseApiService(
    WsDbContext dbContext,
    WarehouseCreateValidator createValidator,
    WarehouseUpdateValidator updateValidator,
    UserManager userManager
    ) : ApiService, IWarehouseService
{
    #region Queries

    public Task<List<WarehouseDto>> GetAllByProductionSiteAsync(Guid productionSiteId)
    {
        return dbContext.Warehouses
            .AsNoTracking()
            .Where(i => i.ProductionSite.Id == productionSiteId)
            .Select(WarehouseExpressions.ToDto)
            .OrderBy(i => i.Name)
            .ToListAsync();
    }

    public Task<List<ProxyDto>> GetProxiesByProductionSiteAsync(Guid productionSiteId)
    {
        return dbContext.Warehouses
            .Where(i => i.ProductionSite.Id == productionSiteId)
            .Select(WarehouseExpressions.ToProxy)
            .OrderBy(i => i.Name)
            .ToListAsync();
    }

    public async Task<WarehouseDto> GetByIdAsync(Guid id)
    {
        WarehouseEntity entity = await dbContext.Warehouses.SafeGetById(id, "Не найдено");
        await LoadDefaultForeignKeysAsync(entity);
        return WarehouseExpressions.ToDto.Compile().Invoke(entity);
    }

    #endregion

    #region Commands

    public async Task<WarehouseDto> CreateAsync(WarehouseCreateDto dto)
    {
        await ValidateAsync(dto, createValidator);
        await dbContext.Warehouses.ThrowIfExistAsync(i => i.Name == dto.Name, "Ошибка уникальности");
        await dbContext.Warehouses.ThrowIfExistAsync(i => i.Uid1C == dto.Id1C, "Ошибка уникальности");

        ProductionSiteEntity productionSiteEntity = await dbContext.ProductionSites.SafeGetById(dto.ProductionSiteId, "Не найдено");
        WarehouseEntity entity = dto.ToEntity(productionSiteEntity);
        await userManager.CanUserWorkWithProductionSiteAsync(entity.ProductionSiteId);

        await dbContext.Warehouses.AddAsync(entity);
        await dbContext.SaveChangesAsync();

        await LoadDefaultForeignKeysAsync(entity);
        return WarehouseExpressions.ToDto.Compile().Invoke(entity);
    }

    public async Task<WarehouseDto> UpdateAsync(Guid id, WarehouseUpdateDto dto)
    {
        await ValidateAsync(dto, updateValidator);
        await dbContext.Warehouses.ThrowIfExistAsync(i => i.Name == dto.Name && i.Id != id, "Ошибка уникальности");
        await dbContext.Warehouses.ThrowIfExistAsync(i => i.Uid1C == dto.Id1C && i.Id != id, "Ошибка уникальности");

        WarehouseEntity entity = await dbContext.Warehouses.SafeGetById(id, "Не найдено");
        await userManager.CanUserWorkWithProductionSiteAsync(entity.ProductionSiteId);

        dto.UpdateEntity(entity);
        await dbContext.SaveChangesAsync();

        await LoadDefaultForeignKeysAsync(entity);
        return WarehouseExpressions.ToDto.Compile().Invoke(entity);
    }

    public Task DeleteAsync(Guid id) => dbContext.Warehouses.SafeDeleteAsync(i => i.Id == id);

    #endregion

    #region Private

    private async Task LoadDefaultForeignKeysAsync(WarehouseEntity entity)
    {
        await dbContext.Entry(entity).Reference(e => e.ProductionSite).LoadAsync();
    }

    #endregion

}