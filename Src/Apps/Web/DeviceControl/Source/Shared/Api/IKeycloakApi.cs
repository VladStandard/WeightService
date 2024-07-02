using Refit;

namespace DeviceControl.Source.Shared.Api;

public interface IKeycloakApi
{
    [Get("/users")]
    Task<KeycloakUser[]> GetAllUsers();

    [Post("/users/{userId}/logout")]
    Task LogoutUser(Guid userId);
}