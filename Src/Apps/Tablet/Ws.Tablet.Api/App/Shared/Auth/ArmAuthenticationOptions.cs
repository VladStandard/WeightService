using Microsoft.AspNetCore.Authentication;

namespace Ws.Tablet.Api.App.Shared.Auth;

public class ArmAuthenticationOptions : AuthenticationSchemeOptions
{
    public const string DefaultScheme = "ArmAuthenticationScheme";
    public string TokenHeaderName { get; set; } = "Authorization";
}