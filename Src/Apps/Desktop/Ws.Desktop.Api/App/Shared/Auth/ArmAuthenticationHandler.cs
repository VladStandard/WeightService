using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Ws.Database.EntityFramework;
using Ws.Shared.Enums;

namespace Ws.Desktop.Api.App.Shared.Auth;

file record Arm(Guid Id, ArmType Role);

public class ArmAuthenticationHandler(
    IOptionsMonitor<ArmAuthenticationOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    WsDbContext dbContext
    ) : AuthenticationHandler<ArmAuthenticationOptions>(options, logger, encoder)
{
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue(Options.TokenHeaderName, out StringValues authHeader))
            return await Task.FromResult(AuthenticateResult.Fail($"Missing header: {Options.TokenHeaderName}"));

        string[] headerValue = authHeader.ToString().Split(' ', 2);

        // Проверка формата заголовка и схемы
        if (headerValue is not [ArmAuthenticationOptions.DefaultScheme, _])
            return AuthenticateResult.Fail($"Invalid Authorization header format or scheme. " +
                                           $"Expected: {ArmAuthenticationOptions.DefaultScheme}");

        string token = headerValue[1];

        Arm? arm = await dbContext.Lines
            .Where(i => i.PcName == token)
            .Select(i => new Arm(i.Id, i.Type))
            .FirstOrDefaultAsync();

        if (arm == null)
            return AuthenticateResult.Fail($"Invalid value in header: {Options.TokenHeaderName}");

        List<Claim> claims = [
            new(ClaimTypes.Name, token),
            new(ClaimTypes.NameIdentifier, arm.Id.ToString()),
            new(ClaimTypes.Role, arm.Role.ToString())
        ];

        ClaimsIdentity claimsIdentity = new(claims, Scheme.Name);
        ClaimsPrincipal claimsPrincipal = new(claimsIdentity);

        return await Task.FromResult(AuthenticateResult.Success(new(claimsPrincipal, Scheme.Name)));
    }
}