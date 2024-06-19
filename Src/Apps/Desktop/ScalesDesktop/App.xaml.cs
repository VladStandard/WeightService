using System.Reflection;

namespace ScalesDesktop;

public partial class App : Application
{
    private static readonly Mutex Mutex = new(true, Assembly.GetEntryAssembly()?.GetName().Name);

    public App()
    {
        if (!Mutex.WaitOne(TimeSpan.Zero, true))
        {
            Current?.Quit();
            Environment.Exit(0);
        }

        InitializeComponent();
        MainPage = new MainPage();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        Window window = base.CreateWindow(activationState);
        window.Title = "Весовой пост";
        return window;
    }
}