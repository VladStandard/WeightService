using Ws.DeviceControl.Api.App.Features.Print.Labels.Common;
using Ws.DeviceControl.Models.Dto.Print.Labels;

namespace Ws.DeviceControl.Api.App.Features.Print.Labels;

[ApiController]
[Route("api/labels")]
public class LabelController(ILabelService labelService)
{
    #region Queries

    [HttpGet]
    public Task<List<LabelDto>> GetAll() => labelService.GetAllAsync();

    [HttpGet("{id:guid}")]
    public Task<LabelDto> GetById([FromRoute] Guid id) => labelService.GetByIdAsync(id);

    #endregion
}