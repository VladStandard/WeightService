using Ws.DeviceControl.Models.Features.Print.Labels;

namespace Ws.DeviceControl.Api.App.Features.Print.Labels.Common;

public interface ILabelService : IGetApiService<LabelDto>
{
    public Task<ZplDto> GetZplByIdAsync(Guid id);
}