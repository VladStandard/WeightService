namespace DeviceControl.Pages.Default;

public partial class MainLayout : LayoutComponentBase
{
    [Inject] private WsUserService UserService { get; set; } = default!;
    [Inject] private NavigationManager UriHelper { get; set; } = default!;
    private ClaimsPrincipal? User { get; set; }
    private static BlazorAppSettingsHelper BlazorAppSettings => BlazorAppSettingsHelper.Instance;
    private static string VerBlazor => $"v{WsBlazorCoreUtils.GetLibVersion()}";
    private static string TmpStyle => WsDebugHelper.Instance.IsDevelop ? "background-color: darkorange;" : "background-color: grey;";

    #region Public and private methods

    protected override async Task OnInitializedAsync()
    {
        User = await UserService.GetUser();
        BlazorAppSettings.SetupMemory();
        BlazorAppSettings.Memory.OpenAsync().ConfigureAwait(false);
        await base.OnInitializedAsync();
    }
    
    #endregion
}
