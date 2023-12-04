namespace DeviceControl.Components.Layout;

public partial class MainLayout : LayoutComponentBase
{
    [Inject] private UserService UserService { get; set; } = null!;
    private ClaimsPrincipal? User { get; set; }

    protected override async Task OnInitializedAsync()
    {
        User = await UserService.GetUser();
    }
    
    private static string VerBlazor => $"v{BlazorCoreUtils.GetLibVersion()}";

}
