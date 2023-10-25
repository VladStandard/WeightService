namespace DeviceControl.Services;

public class WsUserService
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public WsUserService(AuthenticationStateProvider authenticationStateProvider)
    {
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<ClaimsPrincipal?> GetUser()
    {
        AuthenticationState authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        return authState.User;
    }
}
