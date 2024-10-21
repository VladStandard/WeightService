using Ws.DeviceControl.Models.Features.References.Warehouses.Commands;
using Ws.DeviceControl.Models.Features.References.Warehouses.Queries;

namespace Ws.DeviceControl.Models.Features.References.Warehouses;

public static class WarehouseMapper
{
    public static WarehouseUpdateDto DtoToUpdateDto(WarehouseDto item)
    {
        return new()
        {
            Id1C = item.Id1C,
            Name = item.Name
        };
    }
}