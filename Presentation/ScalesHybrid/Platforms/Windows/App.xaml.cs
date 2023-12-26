using System.Windows.Forms;
using CommunityToolkit.Mvvm.Messaging;
using Gma.System.MouseKeyHook;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.LifecycleEvents;
using ScalesHybrid.Events;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ScalesHybrid.WinUI;

public partial class App : MauiWinUIApplication
{
    private readonly IKeyboardMouseEvents _mGlobalHook;
    
    public App()
    {
        InitializeComponent();
        _mGlobalHook = Hook.GlobalEvents();
        MouseSubscribe();
    }

    protected override MauiApp CreateMauiApp()
    {
        return MauiProgram.CreateMauiApp()
            .ConfigureLifecycleEvents(events =>
                events.AddWindows(windowsLifecycleBuilder =>
                    windowsLifecycleBuilder.OnClosed((_, _) => MouseUnsubscribe())))
            .Build();
    }
    
    private static void MiddleButtonHandler(object sender, MouseEventExtArgs e)
    {
        if (e.Button == MouseButtons.Middle)
        {
            WeakReferenceMessenger.Default.Send(new MiddleBtnIsClickedEvent());
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
