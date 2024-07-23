using Phetch.Core;
using Ws.DeviceControl.Models;
using Ws.DeviceControl.Models.Dto.Devices.Arms.Queries;
using Ws.DeviceControl.Models.Dto.Devices.Printers.Queries;
using Ws.DeviceControl.Models.Dto.Shared;
using Ws.Shared.Extensions;

namespace DeviceControl.Source.Shared.Services;

public class DevicesEndpoints(IWebApi webApi)
{
    # region Arm

    public Endpoint<Guid, ArmDto[]> ArmsEndpoint { get; } = new(
        webApi.GetArmsByProductionSite,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public void AddArm(Guid productionSiteId, ArmDto arm) =>
        ArmsEndpoint.UpdateQueryData(productionSiteId, query =>
            query.Data == null ? query.Data! : query.Data.Prepend(arm).ToArray());

    public void UpdateArm(Guid productionSiteId, ArmDto arm) =>
        ArmsEndpoint.UpdateQueryData(productionSiteId, query =>
            query.Data == null ? query.Data! : query.Data.ReplaceItemByKey(arm, p => p.Id == arm.Id).ToArray());

    public void DeleteArm(Guid productionSiteId, Guid armId) =>
        ArmsEndpoint.UpdateQueryData(productionSiteId, query =>
            query.Data == null ? query.Data! : query.Data.Where(x => x.Id != armId).ToArray());

    # endregion

    # region Printer

    public Endpoint<Guid, PrinterDto[]> PrintersEndpoint { get; } = new(
        webApi.GetPrintersByProductionSite,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public void AddPrinter(Guid productionSiteId, PrinterDto printer)
    {
        PrintersEndpoint.UpdateQueryData(productionSiteId, query =>
            query.Data == null ? query.Data! : query.Data.Prepend(printer).ToArray());
        AddProxyPrinter(productionSiteId, new() { Id = printer.Id, Name = printer.Name });
    }

    public void UpdatePrinter(Guid productionSiteId, PrinterDto printer)
    {
        PrintersEndpoint.UpdateQueryData(productionSiteId, query =>
            query.Data == null ? query.Data! : query.Data.ReplaceItemByKey(printer, p => p.Id == printer.Id).ToArray());
        UpdateProxyPrinter(productionSiteId, new() { Id = printer.Id, Name = printer.Name });
    }

    public void DeletePrinter(Guid productionSiteId, Guid printerId)
    {
        PrintersEndpoint.UpdateQueryData(productionSiteId, query =>
            query.Data == null ? query.Data! : query.Data.Where(x => x.Id != printerId).ToArray());
        DeleteProxyPrinter(productionSiteId, printerId);
    }

    # endregion

    # region ProxyPrinter

    public Endpoint<Guid, ProxyDto[]> ProxyPrintersEndpoint { get; } = new(
        webApi.GetProxyPrintersByProductionSite,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public void AddProxyPrinter(Guid productionSiteId, ProxyDto proxyPrinter) =>
        ProxyPrintersEndpoint.UpdateQueryData(productionSiteId, query =>
            query.Data == null ? query.Data! : query.Data.Prepend(proxyPrinter).ToArray());

    public void UpdateProxyPrinter(Guid productionSiteId, ProxyDto proxyPrinter) =>
        ProxyPrintersEndpoint.UpdateQueryData(productionSiteId, query =>
            query.Data == null ? query.Data! : query.Data.ReplaceItemByKey(proxyPrinter, p => p.Id == proxyPrinter.Id).ToArray());

    public void DeleteProxyPrinter(Guid productionSiteId, Guid proxyPrinterId) =>
        ProxyPrintersEndpoint.UpdateQueryData(productionSiteId, query =>
            query.Data == null ? query.Data! : query.Data.Where(x => x.Id != proxyPrinterId).ToArray());

    # endregion
}
