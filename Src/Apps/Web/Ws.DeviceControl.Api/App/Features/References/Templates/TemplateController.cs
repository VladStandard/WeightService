using Ws.DeviceControl.Api.App.Features.References.Templates.Common;

namespace Ws.DeviceControl.Api.App.Features.References.Templates;

[ApiController]
[Route("api/templates")]
public class TemplateController(ITemplateService templateService)
{
    #region Queries

    [HttpGet("proxy")]
    public Task<List<ProxyDto>> GetProxiesByPlu([FromQuery(Name = "plu")] Guid pluId)
        => templateService.GetProxiesByPluAsync(pluId);

    #endregion
}