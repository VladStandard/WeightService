using DeviceControl.Source.Shared.Api;
using Refit;

namespace DeviceControl.Source.Shared.Refit.Clients;

internal class LabelaryRefitClient : IRefitClient
{
    public void Configure(WebApplicationBuilder builder)
    {
        string apiUrl = builder.Configuration.GetValue<string>("LabelaryApi")!;
        builder.Services.AddRefitClient<ILabelaryApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new(apiUrl));
    }
}