// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Utils;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared
{
    public partial class DataLoad
    {
        #region Public and private fields and properties

        [Parameter] public bool IsShowDiv { get; set; }
        [Parameter] public bool IsShowLabel { get; set; }

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);

            RunTasks(LocalizationStrings.DeviceControl.MethodOnInitializedAsync, "", LocalizationStrings.Share.DialogResultFail, "",
                new List<Task> {
                    new(() => {
                        //
                    }),
                }, GuiRefreshAsync, false);
        }

        #endregion
    }
}