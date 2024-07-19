using Phetch.Core;
using Ws.DeviceControl.Models;
using Ws.DeviceControl.Models.Dto.Database;
using Ws.DeviceControl.Models.Dto.Devices.Printers.Queries;
using Ws.DeviceControl.Models.Dto.Shared;

namespace DeviceControl.Source.Shared.Services;

public class DevicesEndpoints(IWebApi webApi)
{
    public Endpoint<Guid, PrinterDto[]> PrintersEndpoint { get; } = new(
        webApi.GetPrinters,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public Endpoint<Guid, ProxyDto[]> ProxyPrinterEndpoint { get; } = new(
        webApi.GetProxyPrinters,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });
}