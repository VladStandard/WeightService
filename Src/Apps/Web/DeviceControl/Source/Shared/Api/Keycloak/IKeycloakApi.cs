using DeviceControl.Source.Shared.Api.Keycloak.Models;
using Refit;

namespace DeviceControl.Source.Shared.Api.Keycloak;

public interface IKeycloakApi
{
    [Get("/users")]
    Task<KeycloakUser[]> GetAllUsers();

    [Post("/users/{userId}/logout")]
    Task LogoutUser(Guid userId);
}