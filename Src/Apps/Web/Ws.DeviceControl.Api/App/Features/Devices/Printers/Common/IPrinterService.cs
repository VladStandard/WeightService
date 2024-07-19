using Ws.DeviceControl.Models.Dto.Devices.Printers.Queries;

namespace Ws.DeviceControl.Api.App.Features.Devices.Printers.Common;

public interface IPrinterService
{
    #region Queries

    Task<List<PrinterDto>> GetAllByProductionSiteAsync(Guid productionSiteId);
    Task<List<ProxyDto>> GetProxiesByProductionSiteAsync(Guid productionSiteId);

    Task<PrinterDto> GetByIdAsync(Guid id);

    #endregion
}