using Ws.DeviceControl.Api.App.Features.Print.Labels.Common;
using Ws.DeviceControl.Models.Features.Print.Labels;

namespace Ws.DeviceControl.Api.App.Features.Print.Labels;

[ApiController]
[Route(ApiEndpoints.Labels)]
public class LabelController(ILabelService labelService)
{
    #region Queries

    [HttpGet]
    public Task<List<LabelDto>> GetAll() => labelService.GetAllAsync();

    [HttpGet("{id:guid}")]
    public Task<LabelDto> GetById([FromRoute] Guid id) => labelService.GetByIdAsync(id);

    [HttpGet("{id:guid}/zpl")]
    public Task<ZplDto> GetZplById([FromRoute] Guid id) => labelService.GetZplByIdAsync(id);

    #endregion
}