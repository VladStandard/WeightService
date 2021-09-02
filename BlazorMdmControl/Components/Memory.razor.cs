// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace MdmControlBlazor.Components
{
    public partial class Memory
    {
        //private string MemoryIsExecute => AppSettings.Memory?.TokenSource != null && !AppSettings.Memory.TokenSource.IsCancellationRequested
        //? $"{LocalizationStrings.MemoryIsExecute}" : $"{LocalizationStrings.MemoryIsNotExecute}";
        //private string MemoryLimit => AppSettings.Memory?.MemorySize.Limit.MegaBytes <= 0
        //    ? LocalizationStrings.MemoryLimitNotSet
        //    : $"{LocalizationStrings.MemoryLimit}: {AppSettings.Memory?.MemorySize.Limit.MegaBytes:N0} MB";
        //private string MemoryInfo => AppSettings.Memory != null
        //    ? $"{MemoryIsExecute}" + Environment.NewLine +
        //      $"{MemoryLimit}" + Environment.NewLine + Environment.NewLine +
        //      $"{LocalizationStrings.MemoryPhysical}: {AppSettings.Memory.MemorySize.Physical.MegaBytes:N0} MB" + Environment.NewLine +
        //      $"{LocalizationStrings.MemoryVirtual}: {AppSettings.Memory.MemorySize.Virtual.MegaBytes:N0} MB" + Environment.NewLine +
        //      $"{LocalizationStrings.MemoryResult}: {AppSettings.Memory.Result}" + Environment.NewLine +
        //      $"{LocalizationStrings.MemoryException}: {AppSettings.Memory.ExceptionMsg}"
        //    : $"{MemoryIsExecute}" + Environment.NewLine +
        //      $"{MemoryLimit}" + Environment.NewLine + Environment.NewLine +
        //      $"{LocalizationStrings.MemoryPhysical}: - MB" + Environment.NewLine +
        //      $"{LocalizationStrings.MemoryVirtual}: - MB" + Environment.NewLine +
        //      $"{LocalizationStrings.MemoryResult}: -" + Environment.NewLine +
        //      $"{LocalizationStrings.MemoryException}: -";

        //private async Task OpenAsync()
        //{
        //    await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        //    await AppSettings.MemoryOpenAsync(GuiRefreshAsync).ConfigureAwait(false);
        //}

        //private async Task CloseAsync()
        //{
        //    await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        //    await AppSettings.MemoryCloseAsync().ConfigureAwait(false);
        //}

        //private async Task GuiRefreshAsync()
        //{
        //    await InvokeAsync(StateHasChanged).ConfigureAwait(false);
        //}

        //protected override async Task OnInitializedAsync()
        //{
        //    await base.OnInitializedAsync().ConfigureAwait(true);
        //    await OpenAsync().ConfigureAwait(false);
        //}

        //public void Dispose()
        //{
        //    CloseAsync().ConfigureAwait(false);
        //}
    }
}