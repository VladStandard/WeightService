using DeviceControl.Source.Shared.Api;
using Phetch.Core;

namespace DeviceControl.Source.Shared.Services;

public class KeycloakEndpoints(IKeycloakApi keycloakApi)
{
    public ParameterlessEndpoint<KeycloakUser[]> KeycloakUsersEndpoint { get; } = new(
        keycloakApi.GetAllUsers,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(5) });
}