using DeviceControl.Source.Shared.Api;
using DeviceControl.Source.Shared.Auth;
using Refit;

namespace DeviceControl.Source.Shared.Refit.Clients;

internal sealed class KeycloakRefitClient : IRefitClient
{
    public void Configure(WebApplicationBuilder builder)
    {
        IConfigurationSection oidcConfiguration = builder.Configuration.GetSection("Oidc");

        string authority = oidcConfiguration.GetValue<string>("Authority")!;
        string realm = oidcConfiguration.GetValue<string>("Realm")!;
        string keycloakAdminUrl = $"{authority}/admin/realms/{realm}";

        builder.Services.AddRefitClient<IKeycloakApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new(keycloakAdminUrl))
            .AddHttpMessageHandler<ServerAuthorizationMessageHandler>();
    }
}