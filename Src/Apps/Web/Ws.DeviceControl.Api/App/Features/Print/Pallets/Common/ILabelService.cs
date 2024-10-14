using Ws.DeviceControl.Models.Features.Print.Labels;

namespace Ws.DeviceControl.Api.App.Features.Print.Pallets.Common;

public interface ILabelService : IGetApiService<LabelDto>
{
    public Task<ZplDto> GetZplByIdAsync(Guid id);
}