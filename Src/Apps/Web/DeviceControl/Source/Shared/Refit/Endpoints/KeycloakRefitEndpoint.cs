using DeviceControl.Source.Shared.Api;
using DeviceControl.Source.Shared.Auth;
using Refit;

namespace DeviceControl.Source.Shared.Refit.Endpoints;

internal class KeycloakRefitEndpoint : IRefitEndpoint
{
    public void Configure(WebApplicationBuilder builder)
    {
        IConfigurationSection oidcConfiguration = builder.Configuration.GetSection("Oidc");
        string keycloakAdminUrl = $"{oidcConfiguration.GetValue<string>("Authority")}/admin/realms/{oidcConfiguration.GetValue<string>("Realm")}";

        builder.Services.AddRefitClient<IKeycloakApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new(keycloakAdminUrl))
            .AddHttpMessageHandler<ServerAuthorizationMessageHandler>();

        builder.Services.AddScoped<UserApi>();
    }
}