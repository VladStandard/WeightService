namespace KeycloakBlazor.Source.Api.Keycloak;

internal sealed class KeycloakApi(HttpClient httpClient) : IKeycloakApi
{
    public async Task<IEnumerable<User>> GetAllUsers() =>
        await httpClient.GetFromJsonAsync<User[]>("users") ??
        throw new IOException("No users found!");

    public async Task Logout(Guid userId) =>
        await httpClient.PostAsync($"users/{userId}/logout", null);
}