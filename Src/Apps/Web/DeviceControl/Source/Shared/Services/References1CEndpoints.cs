using Phetch.Core;
using Ws.DeviceControl.Models;
using Ws.DeviceControl.Models.Dto.Shared;

namespace DeviceControl.Source.Shared.Services;

public class References1CEndpoints(IWebApi webApi)
{
    public ParameterlessEndpoint<PackageDto[]> BoxesEndpoint { get; } = new(
        webApi.GetBoxes,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });
}