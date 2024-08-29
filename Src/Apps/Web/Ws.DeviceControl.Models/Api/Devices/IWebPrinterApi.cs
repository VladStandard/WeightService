using Ws.DeviceControl.Models.Features.Devices.Printers.Commands.Create;
using Ws.DeviceControl.Models.Features.Devices.Printers.Commands.Update;
using Ws.DeviceControl.Models.Features.Devices.Printers.Queries;
using Ws.DeviceControl.Models.Shared;

namespace Ws.DeviceControl.Models.Api.Devices;

public interface IWebPrinterApi
{
    #region Queries

    [Get("/printers/{uid}")]
    Task<PrinterDto> GetPrinterByUid(Guid uid);

    [Get("/printers?productionSite={productionSiteUid}")]
    Task<PrinterDto[]> GetPrintersByProductionSite(Guid productionSiteUid);

    [Get("/printers/proxy?productionSite={productionSiteUid}")]
    Task<ProxyDto[]> GetProxyPrintersByProductionSite(Guid productionSiteUid);

    #endregion

    #region Commands

    [Delete("/printers/{uid}")]
    Task<bool> DeletePrinter(Guid uid);

    [Post("/printers")]
    Task<PrinterDto> CreatePrinter([Body] PrinterCreateDto createDto);

    [Post("/printers/{uid}")]
    Task<PrinterDto> UpdatePrinter(Guid uid, [Body] PrinterUpdateDto updateDto);

    #endregion
}