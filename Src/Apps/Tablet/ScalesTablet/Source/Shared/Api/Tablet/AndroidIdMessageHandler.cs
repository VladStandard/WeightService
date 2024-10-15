using Android.Provider;

namespace ScalesTablet.Source.Shared.Api.Tablet;

public class AndroidIdMessageHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Authorization = new("ArmAuthenticationScheme",
            Settings.Secure.GetString(Android.App.Application.Context.ContentResolver, Settings.Secure.AndroidId));
        return await base.SendAsync(request, cancellationToken);
    }
}