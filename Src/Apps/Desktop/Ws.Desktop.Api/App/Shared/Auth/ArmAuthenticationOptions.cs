using Microsoft.AspNetCore.Authentication;

namespace Ws.Desktop.Api.App.Shared.Auth;

public class ArmAuthenticationOptions : AuthenticationSchemeOptions
{
    public const string DefaultScheme = "ArmAuthenticationScheme";
    public string TokenHeaderName { get; set; } = "Authorization";
}