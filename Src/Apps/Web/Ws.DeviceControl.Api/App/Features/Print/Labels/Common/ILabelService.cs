using Ws.DeviceControl.Models.Dto.Print.Labels;

namespace Ws.DeviceControl.Api.App.Features.Print.Labels.Common;

public interface ILabelService : IGetApiService<LabelDto>
{
    public Task<ZplDto> GetZplByIdAsync(Guid id);
}