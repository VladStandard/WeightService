using System.Windows.Forms;
using CommunityToolkit.Mvvm.Messaging;
using Gma.System.MouseKeyHook;
using Microsoft.Maui.LifecycleEvents;
using ScalesHybrid.Messages;
using Microsoft.UI.Windowing;
using WinRT.Interop;
using Microsoft.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ScalesHybrid.WinUI;

public partial class App : MauiWinUIApplication
{
    private IKeyboardMouseEvents _mGlobalHook;
    
    public App()
    {
        InitializeComponent();
        _mGlobalHook = Hook.GlobalEvents();
        MouseSubscribe();
    }

    protected override MauiApp CreateMauiApp()
    {
        MauiAppBuilder builder = MauiProgram.CreateMauiApp();

        builder.ConfigureLifecycleEvents(events => {
            events.AddWindows(windowsLifecycleBuilder =>
            {
                windowsLifecycleBuilder.OnWindowCreated(window => {
                    OpenFullScreen(window);
                });
                windowsLifecycleBuilder.OnClosed((_, _) => {
                    MouseUnsubscribe();
                });
            });
        });

        return builder.Build();
    }

    private static void OpenFullScreen(Microsoft.UI.Xaml.Window window)
    {
    #if RELEASE_VS
            window.ExtendsContentIntoTitleBar = false;
            IntPtr handle = WindowNative.GetWindowHandle(window);
            WindowId id = Win32Interop.GetWindowIdFromWindow(handle);
            AppWindow appWindow = AppWindow.GetFromWindowId(id);

            if (appWindow.Presenter is not OverlappedPresenter overlappedPresenter)
                return;
                        
            overlappedPresenter.SetBorderAndTitleBar(false, false);
            overlappedPresenter.Maximize();
    #endif
    }
    
    private static void MiddleButtonHandler(object sender, MouseEventExtArgs e)
    {
        if (e.Button == MouseButtons.Middle)
        {
            WeakReferenceMessenger.Default.Send(new MiddleBtnIsClickedMessage());
        }
    }
    
    private void MouseSubscribe()
    {
        _mGlobalHook.MouseDownExt += MiddleButtonHandler;
    }
    
    private void MouseUnsubscribe()
    {
        _mGlobalHook.MouseDownExt -= MiddleButtonHandler;
        _mGlobalHook.Dispose();
    }
}
