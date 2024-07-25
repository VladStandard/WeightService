using Phetch.Core;
using Ws.DeviceControl.Models;
using Ws.DeviceControl.Models.Dto.Admins.PalletMen.Queries;
using Ws.DeviceControl.Models.Dto.Admins.Users.Queries;
using Ws.DeviceControl.Models.Dto.Print.Labels;
using Ws.Shared.Extensions;

namespace DeviceControl.Source.Shared.Services;

public class OperationsEndpoints(IWebApi webApi)
{
    public ParameterlessEndpoint<LabelDto[]> LabelsEndpoint { get; } = new(
        webApi.GetLabels,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(5) });
}