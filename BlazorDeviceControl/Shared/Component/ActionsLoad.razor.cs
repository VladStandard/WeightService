// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Localization;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Component
{
    public partial class ActionsLoad
    {
        #region Public and private fields and properties

        [Parameter] public ShareEnums.ActionLoad DataLoadItem { get; set; }
        [Parameter] public bool IsShowProgress { get; set; }

        #endregion

        #region Constructor and destructor

        public ActionsLoad() : base()
        {
            //
        }

        #endregion
        
        #region Public and private methods

        private void Default()
        {
            IsLoaded = false;
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{Core.Strings.Method} {nameof(SetParametersAsync)}", "", Core.Strings.DialogResultFail, "",
                new Task(async () =>
                {
                    Default();
                    IsLoaded = true;
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion

    }
}
