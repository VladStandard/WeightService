// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Principal;

namespace BlazorCore
{
    public class AuthEntity
    {
        private readonly AuthenticationStateProvider _stateProvider;

        public AuthEntity(AuthenticationStateProvider authenticationStateProvider)
        {
            _stateProvider = authenticationStateProvider;
        }

        public async Task<IIdentity> GetIdentityAsync()
        {
            AuthenticationState authState = await _stateProvider.GetAuthenticationStateAsync();
            System.Security.Claims.ClaimsPrincipal user = authState.User;
            return user.Identity;
        }

        public IIdentity GetIdentity()
        {
            Task<AuthenticationState> state = _stateProvider.GetAuthenticationStateAsync();
            if (!state.IsCompleted)
                return null;

            AuthenticationState authenticationState = state.Result;
            if (authenticationState?.User == null)
                return null;
            IIdentity identity = authenticationState.User.Identity;
            return identity;
        }
    }
}
