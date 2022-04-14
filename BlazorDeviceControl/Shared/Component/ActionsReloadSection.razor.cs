// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Localization;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Component
{
    public partial class ActionsReloadSection
    {
        #region Public and private fields and properties

        //

        #endregion

        #region Constructor and destructor

        public ActionsReloadSection() : base()
        {
            //
        }

        #endregion

        #region Public and private methods

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
