using KeycloakBlazor.Source.Utils.Api.Keycloak;
using Phetch.Core;

namespace KeycloakBlazor.Source.Utils.Services;

public class UserApi(IKeycloakApi keycloakApi)
{
    public ParameterlessEndpoint<User[]> UsersEndpoint { get; } = new(
        keycloakApi.GetAllUsers,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(5) });
}