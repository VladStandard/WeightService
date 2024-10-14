using System.Globalization;

namespace DeviceControl.Source.Shared.Handlers;

public class AcceptLanguageHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        string currentCulture = CultureInfo.CurrentUICulture.Name;
        request.Headers.AcceptLanguage.Add(new(currentCulture));
        return await base.SendAsync(request, cancellationToken);
    }
}