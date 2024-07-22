using Ws.DeviceControl.Models.Dto.References.Warehouses.Commands.Update;
using Ws.DeviceControl.Models.Dto.References.Warehouses.Queries;

namespace Ws.DeviceControl.Models.Dto.References.Warehouses;

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