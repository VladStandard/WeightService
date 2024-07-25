using Phetch.Core;
using Ws.DeviceControl.Models;
using Ws.DeviceControl.Models.Dto.Print.Labels;

namespace DeviceControl.Source.Shared.Services;

public class OperationsEndpoints(IWebApi webApi)
{
    public ParameterlessEndpoint<LabelDto[]> LabelsEndpoint { get; } = new(
        webApi.GetLabels,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(5) });

    public Endpoint<Guid, ZplDto> LabelZplEndpoint { get; } = new(
        webApi.GetLabelZplByUid,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(5) });
}