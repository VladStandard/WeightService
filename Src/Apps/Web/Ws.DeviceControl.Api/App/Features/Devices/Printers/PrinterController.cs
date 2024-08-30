using Ws.DeviceControl.Api.App.Features.Devices.Printers.Common;
using Ws.DeviceControl.Models.Features.Devices.Printers.Commands.Create;
using Ws.DeviceControl.Models.Features.Devices.Printers.Commands.Update;
using Ws.DeviceControl.Models.Features.Devices.Printers.Queries;

namespace Ws.DeviceControl.Api.App.Features.Devices.Printers;

[ApiController]
[Route("api/printers/")]
public class PrinterController(IPrinterService printerService)
{
    #region Queries

    [HttpGet("proxy")]
    public Task<List<ProxyDto>> GetProxiesByProductionSite([FromQuery(Name = "productionSite")] Guid productionSiteId) =>
        printerService.GetProxiesByProductionSiteAsync(productionSiteId);

    [HttpGet]
    public Task<List<PrinterDto>> GetAllByProductionSite([FromQuery(Name = "productionSite")] Guid productionSiteId) =>
        printerService.GetAllByProductionSiteAsync(productionSiteId);

    [HttpGet("{id:guid}")]
    public Task<PrinterDto> GetById([FromRoute] Guid id) => printerService.GetByIdAsync(id);

    #endregion

    #region Commands

    [Authorize(PolicyEnum.Support)]
    [HttpPost]
    public Task<PrinterDto> Create([FromBody] PrinterCreateDto dto) =>
        printerService.CreateAsync(dto);

    [Authorize(PolicyEnum.Support)]
    [HttpPost("{id:guid}")]
    public Task<PrinterDto> Update([FromRoute] Guid id, [FromBody] PrinterUpdateDto dto) =>
        printerService.UpdateAsync(id, dto);

    [Authorize(PolicyEnum.Support)]
    [HttpPost("{id:guid}/delete")]
    public Task Delete([FromRoute] Guid id) => printerService.DeleteAsync(id);

    #endregion
}