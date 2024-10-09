using Ws.Database.Entities.Ref.Printers;
using Ws.Database.Entities.Ref.ProductionSites;
using Ws.DeviceControl.Models.Features.Devices.Printers.Commands.Create;
using Ws.DeviceControl.Models.Features.Devices.Printers.Commands.Update;

namespace Ws.DeviceControl.Api.App.Features.Devices.Printers.Impl.Extensions;

internal static class PrinterDtoExtensions
{
    public static PrinterEntity ToEntity(this PrinterCreateDto dto, ProductionSiteEntity productionSiteEntity)
    {
        return new()
        {
            Name = dto.Name,
            Ip = dto.Ip,
            Type = dto.Type,
            ProductionSite = productionSiteEntity
        };
    }


    public static void UpdateEntity(this PrinterUpdateDto dto, PrinterEntity entity)
    {
        entity.Name = dto.Name;
        entity.Ip = dto.Ip;
        entity.Type = dto.Type;
    }
}