using Phetch.Core;
using Ws.DeviceControl.Models;
using Ws.DeviceControl.Models.Features.Print.Labels;
// ReSharper disable ClassNeverInstantiated.Global

namespace DeviceControl.Source.Shared.Api.Web.Endpoints;

public class OperationsEndpoints(IWebApi webApi)
{
    public ParameterlessEndpoint<LabelDto[]> LabelsEndpoint { get; } = new(
        webApi.GetLabels,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(5) });

    public Endpoint<Guid, ZplDto> LabelZplEndpoint { get; } = new(
        webApi.GetLabelZplByUid,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(5) });
}