// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using MudBlazor;

namespace BlazorDeviceControl.Razors;

public partial class MudDialogExample
{
    #region Public and private fields, properties, constructor

    [CascadingParameter] MudDialogInstance MudDialog { get; set; }

    #endregion

    #region Public and private methods

    private void Submit() => MudDialog.Close(DialogResult.Ok(true));
    private void Cancel() => MudDialog.Cancel();

    #endregion
}
