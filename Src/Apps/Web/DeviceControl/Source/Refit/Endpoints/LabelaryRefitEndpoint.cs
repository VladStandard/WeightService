using DeviceControl.Source.Shared.Api;
using Refit;

namespace DeviceControl.Source.Refit.Endpoints;

internal class LabelaryRefitEndpoint : IRefitEndpoint
{
    public void Configure(WebApplicationBuilder builder)
    {
        builder.Services.AddRefitClient<ILabelaryApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new(builder.Configuration.GetValue<string>("LabelaryApi") ?? ""));

        builder.Services.AddScoped<ZplApi>();
    }
}