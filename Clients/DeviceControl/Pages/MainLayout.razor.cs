// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Pages;

public partial class MainLayout : RazorComponentBase
{
    #region Public and private methods

    private string VerBlazor => $"v{BlazorCoreUtils.GetLibVersion()}";

    protected override void OnParametersSet()
    {
        BlazorAppSettings.SetupMemory();
        BlazorAppSettings.Memory.OpenAsync().ConfigureAwait(false);
    }

    #endregion
}
