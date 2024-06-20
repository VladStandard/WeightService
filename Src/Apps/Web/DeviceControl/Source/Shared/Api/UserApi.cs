// namespace DeviceControl.Source.Shared.Api;
//
// public class UserApi(IKeycloakApi keycloakApi)
// {
//     public ParameterlessEndpoint<User[]> UsersEndpoint { get; } = new(
//         keycloakApi.GetAllUsers,
//         options: new() { DefaultStaleTime = TimeSpan.FromMinutes(5) });
// }