using Ws.Database.EntityFramework.Entities.Ref.ProductionSites;
using Ws.Database.EntityFramework.Entities.Ref.Warehouses;
using Ws.DeviceControl.Models.Features.References.Warehouses.Commands.Create;
using Ws.DeviceControl.Models.Features.References.Warehouses.Commands.Update;

namespace Ws.DeviceControl.Api.App.Features.References.Warehouses.Impl.Extensions;

internal static class WarehouseDtoExtensions
{
    public static WarehouseEntity ToEntity(this WarehouseCreateDto dto, ProductionSiteEntity productionSiteEntity)
    {
        return new()
        {
            Name = dto.Name,
            Uid1C = dto.Id1C,
            ProductionSite = productionSiteEntity
        };
    }


    public static void UpdateEntity(this WarehouseUpdateDto dto, WarehouseEntity entity)
    {
        entity.Name = dto.Name;
        entity.Uid1C = dto.Id1C;
    }
}