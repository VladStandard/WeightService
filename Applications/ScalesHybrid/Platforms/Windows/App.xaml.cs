using Microsoft.Maui.LifecycleEvents;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ScalesHybrid.WinUI;

public partial class App : MauiWinUIApplication
{
    public App()
    {
        InitializeComponent();
    }

    protected override MauiApp CreateMauiApp()
    {
        MauiAppBuilder builder = MauiProgram.CreateMauiApp();
        builder.ConfigureLifecycleEvents(events => {
            events.AddWindows(windowsLifecycleBuilder =>
            {
                windowsLifecycleBuilder.OnWindowCreated(window =>
                {
                    window.ExtendsContentIntoTitleBar = false;
                    IntPtr handle = WinRT.Interop.WindowNative.GetWindowHandle(window);
                    WindowId id = Win32Interop.GetWindowIdFromWindow(handle);
                    AppWindow appWindow = AppWindow.GetFromWindowId(id);

                    if (appWindow.Presenter is not OverlappedPresenter overlappedPresenter)
                        return;
                    
                    overlappedPresenter.SetBorderAndTitleBar(false, false);
                    overlappedPresenter.Maximize();
                });
            });
        });
        return builder.Build();
    }
}

