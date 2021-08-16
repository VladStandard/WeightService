// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared
{
    public partial class DataLoad
    {
        #region Public and private fields and properties

        [Parameter] public bool IsShowDiv { get; set; }
        [Parameter] public EnumDataLoad DataLoadItem { get; set; }
        [Parameter] public bool IsShowProgress { get; set; } = true;

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);

            //RunTasks(LocalizationStrings.DeviceControl.MethodOnInitializedAsync, "", LocalizationStrings.Share.DialogResultFail, "",
            //    new List<Task> {
            //        new(() => {
            //            //
            //        }),
            //    }, GuiRefreshAsync, false);
            await GuiRefreshAsync(false).ConfigureAwait(false);
        }

        #endregion
    }
}