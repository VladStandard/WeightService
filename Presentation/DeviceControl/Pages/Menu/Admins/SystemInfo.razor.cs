namespace DeviceControl.Pages.Menu.Admins;

public sealed partial class SystemInfo : ComponentBase
{
    #region Public and private fields, properties, constructor
    private static string VerApp => WsAssemblyUtils.GetAppVersion(Assembly.GetExecutingAssembly());
    private static string VerLibBlazorCore => WsBlazorCoreUtils.GetLibVersion();
    private static string VerLibDataCore => WsAssemblyUtils.GetLibVersion();
    private ulong CurrentRamSize => BlazorAppSettingsHelper.Instance.Memory.MemorySize.PhysicalAllocated.MegaBytes;
    private ulong TotalRamSize => BlazorAppSettingsHelper.Instance.Memory.MemorySize.PhysicalTotal.MegaBytes;

    #endregion
}
