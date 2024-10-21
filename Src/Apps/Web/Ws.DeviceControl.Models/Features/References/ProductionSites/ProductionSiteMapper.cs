using Ws.DeviceControl.Models.Features.References.ProductionSites.Commands;
using Ws.DeviceControl.Models.Features.References.ProductionSites.Queries;

namespace Ws.DeviceControl.Models.Features.References.ProductionSites;

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