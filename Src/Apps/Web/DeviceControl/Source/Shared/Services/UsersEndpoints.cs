using DeviceControl.Source.Shared.Api;
using Phetch.Core;

namespace DeviceControl.Source.Shared.Services;

public class UsersEndpoints(IKeycloakApi keycloakApi)
{
    public ParameterlessEndpoint<KeycloakUser[]> UsersEndpoint { get; } = new(
        keycloakApi.GetAllUsers,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(5) });
}