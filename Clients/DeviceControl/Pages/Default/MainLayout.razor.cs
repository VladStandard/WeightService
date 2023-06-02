// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsDataCore.Helpers;

namespace DeviceControl.Pages.Default;

public partial class MainLayout : RazorComponentBase
{
    #region Public and private methods

    private static string VerBlazor => $"v{BlazorCoreUtils.GetLibVersion()}";
    
    private static string TmpStyle => WsDebugHelper.Instance.IsDevelop ? "background-color: darkorange;" : "background-color: grey;";

    protected override void OnParametersSet()
    {
        BlazorAppSettings.SetupMemory();
        BlazorAppSettings.Memory.OpenAsync().ConfigureAwait(false);
    }

    #endregion
}