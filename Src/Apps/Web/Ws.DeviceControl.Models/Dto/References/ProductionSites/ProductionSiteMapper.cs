using Ws.DeviceControl.Models.Dto.References.ProductionSites.Commands.Update;
using Ws.DeviceControl.Models.Dto.References.ProductionSites.Queries;

namespace Ws.DeviceControl.Models.Dto.References.ProductionSites;

public static class ProductionSiteMapper
{
    public static ProductionSiteUpdateDto DtoToUpdateDto(ProductionSiteDto item)
    {
        return new()
        {
            Name = item.Name,
            Address = item.Address,
        };
    }
}