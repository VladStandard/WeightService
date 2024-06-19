using Refit;

namespace KeycloakBlazor.Source.Utils.Api.Keycloak;

public interface IKeycloakApi
{
    [Get("/users")]
    Task<User[]> GetAllUsers();

    [Post("/users/{userId}/logout")]
    Task Logout(Guid userId);
}