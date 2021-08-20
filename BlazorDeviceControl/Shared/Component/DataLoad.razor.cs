// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore;
using Microsoft.AspNetCore.Components;

namespace BlazorDeviceControl.Shared.Component
{
    public partial class DataLoad
    {
        #region Public and private fields and properties

        [Parameter] public bool IsShowProgress { get; set; }
        [Parameter] public EnumDataLoad DataLoadItem { get; set; }

        #endregion

        #region Public and private methods

        //public override async Task SetParametersAsync(ParameterView parameters)
        //{
        //    await base.SetParametersAsync(parameters).ConfigureAwait(true);

        //    await GuiRefreshWithWaitAsync();
        //}

        #endregion
    }
}
