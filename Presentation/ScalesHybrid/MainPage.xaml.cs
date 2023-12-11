namespace ScalesHybrid;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }
    private void Bwv_BlazorWebViewInitialized(object sender, Microsoft.AspNetCore.Components.WebView.BlazorWebViewInitializedEventArgs e)
    {
    #if WINDOWS
        e.WebView.CoreWebView2.Settings.IsZoomControlEnabled = false;
    #endif
    }
}
