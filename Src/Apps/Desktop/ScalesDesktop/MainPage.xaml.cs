using MauiPageFullScreen;
using Microsoft.AspNetCore.Components.WebView;
using Microsoft.Extensions.Configuration;

namespace ScalesDesktop;

public partial class MainPage : ContentPage
{
    private readonly bool _fullScreen;

    public MainPage(IConfiguration configuration)
    {
        InitializeComponent();
        _fullScreen = configuration.GetValue<bool>("FullScreenMode");
    }

    private void Bwv_BlazorWebViewInitialized(object sender, BlazorWebViewInitializedEventArgs e)
    {
        e.WebView.CoreWebView2.Settings.IsPinchZoomEnabled = false;
        e.WebView.CoreWebView2.Settings.IsZoomControlEnabled = false;

        if (_fullScreen)
            Controls.FullScreen();
    }
}