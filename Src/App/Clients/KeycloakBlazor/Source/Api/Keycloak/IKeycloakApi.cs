namespace KeycloakBlazor.Source.Api.Keycloak;

public interface IKeycloakApi
{
    Task<IEnumerable<User>> GetAllUsers();
    Task Logout(Guid userId);
}