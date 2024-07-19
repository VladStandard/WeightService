using Ws.Database.EntityFramework.Entities.Ref.Warehouses;
using Ws.DeviceControl.Api.App.Features.References.Warehouses.Common;
using Ws.DeviceControl.Api.App.Features.References.Warehouses.Impl.Expressions;
using Ws.DeviceControl.Models.Dto.References.Warehouses.Queries;

namespace Ws.DeviceControl.Api.App.Features.References.Warehouses.Impl;

public class WarehouseApiService(WsDbContext dbContext) : IWarehouseService
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
        WarehouseEntity? warehouse = await dbContext.Warehouses.FindAsync(id);
        if (warehouse == null) throw new KeyNotFoundException();
        return WarehouseExpressions.ToDto.Compile().Invoke(warehouse);
    }

    #endregion
}