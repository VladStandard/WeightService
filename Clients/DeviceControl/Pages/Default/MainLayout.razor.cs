// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DeviceControl.Pages.Default;

public partial class MainLayout : LayoutComponentBase
{
    [Inject] private WsUserService UserService { get; set; }
    private ClaimsPrincipal? User { get; set; }
    private static BlazorAppSettingsHelper BlazorAppSettings => BlazorAppSettingsHelper.Instance;
    private static string VerBlazor => $"v{BlazorCoreUtils.GetLibVersion()}";
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