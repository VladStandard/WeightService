// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Localizations;
using Microsoft.AspNetCore.Components;

namespace BlazorDeviceControl.Shared.Component;

public partial class ActionsReloadItem : Shared.Component.ActionsReloadBase
{
    #region Public and private fields and properties

    //

    #endregion

    #region Public and private methods

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters).ConfigureAwait(true);
        RunTasks($"{LocaleCore.Action.ActionMethod} {nameof(SetParametersAsync)}", "", LocaleCore.Dialog.DialogResultFail, "",
            new Task(async () =>
            {
                Default();
                IsLoaded = true;
                await GuiRefreshWithWaitAsync();
            }), true);
    }

    #endregion
}
