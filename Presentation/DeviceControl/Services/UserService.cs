namespace DeviceControl.Services;

public class UserService
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public UserService(AuthenticationStateProvider authenticationStateProvider)
    {
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<ClaimsPrincipal?> GetUser()
    {
        AuthenticationState authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        return authState.User;
    }
}
