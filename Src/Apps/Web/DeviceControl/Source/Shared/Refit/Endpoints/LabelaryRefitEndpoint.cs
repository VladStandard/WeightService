using DeviceControl.Source.Shared.Api;
using DeviceControl.Source.Shared.Services;
using Refit;

namespace DeviceControl.Source.Shared.Refit.Endpoints;

internal class LabelaryRefitEndpoint : IRefitEndpoint
{
    public void Configure(WebApplicationBuilder builder)
    {
        string apiUrl = builder.Configuration.GetValue<string>("LabelaryApi")!;
        builder.Services.AddRefitClient<ILabelaryApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new(apiUrl));

        builder.Services.AddScoped<ZplEndpoints>();
    }
}