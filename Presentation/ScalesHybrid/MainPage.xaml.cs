using MauiPageFullScreen;
using Microsoft.AspNetCore.Components.WebView;
using Ws.Shared.Enums;
using Ws.Shared.Utils;

namespace ScalesHybrid;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }
    
    private void Bwv_BlazorWebViewInitialized(object sender, BlazorWebViewInitializedEventArgs e)
    {
        e.WebView.CoreWebView2.Settings.IsPinchZoomEnabled = false;
        e.WebView.CoreWebView2.Settings.IsZoomControlEnabled = false;
        
        if (ConfigurationUtil.Config == EnumConfiguration.ReleaseVs)
            Controls.FullScreen();
    }
}
