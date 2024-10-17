namespace ScalesDesktop.Source.Shared.Api.Desktop.Handlers;

public class HostNameMessageHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Authorization = new("ArmAuthenticationScheme", Dns.GetHostName());
        return await base.SendAsync(request, cancellationToken);
    }
}