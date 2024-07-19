using Ws.Database.EntityFramework.Entities.Ref.Printers;
using Ws.DeviceControl.Models.Dto.Devices.Printers.Queries;

namespace Ws.DeviceControl.Api.App.Features.Devices.Printers.Impl.Expressions;

public static class PrinterExpressions
{
    public static Expression<Func<PrinterEntity, PrinterDto>> ToDto =>
        printer => new()
        {
            Id = printer.Id,
            Name = printer.Name,
            Ip = printer.Ip,
            Type = printer.Type,
            CreateDt = printer.CreateDt,
            ChangeDt = printer.ChangeDt
        };

    public static Expression<Func<PrinterEntity, ProxyDto>> ToProxy =>
        printer => new()
        {
            Id = printer.Id,
            Name = printer.Name + " | " + printer.Ip
        };
}