namespace DeviceControl.Pages.Default;

public partial class MainLayout : LayoutComponentBase
{
    [Inject] private UserService UserService { get; set; } = default!;
    [Inject] private NavigationManager UriHelper { get; set; } = default!;
    private ClaimsPrincipal? User { get; set; }
    private static BlazorAppSettingsHelper BlazorAppSettings => BlazorAppSettingsHelper.Instance;
    private static string VerBlazor => $"v{BlazorCoreUtils.GetLibVersion()}";

    #region Public and private methods

    protected override async Task OnInitializedAsync()
    {
        User = await UserService.GetUser();
        await base.OnInitializedAsync();
    }
    
    #endregion
}
