// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors;

public partial class MainLayout : BlazorCore.Models.RazorBase
{
    #region Public and private methods

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Table = new TableSystemEntity(ProjectsEnums.TableSystem.Default);
        Items = new();
    }

    private void MemoryClear()
    {
        GC.Collect();
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        RunActions(new()
        {
            () => UserSettings.SetupAccessRights(),
            () =>
            {
                AppSettings.SetupMemory();
                AppSettings.Memory.OpenAsync().ConfigureAwait(false);
            }
        });
    }

    #endregion
}
