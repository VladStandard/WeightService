namespace DeviceControl.Pages.Default;

public partial class MainLayout : LayoutComponentBase
{
    [Inject] private UserService UserService { get; set; } = default!;
    private ClaimsPrincipal? User { get; set; }
    private static string VerBlazor => $"v{BlazorCoreUtils.GetLibVersion()}";

    #region Public and private methods

    protected override async Task OnInitializedAsync()
    {
        User = await UserService.GetUser();
    }
    
    #endregion
}
