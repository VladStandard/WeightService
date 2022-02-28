// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.DAL.TableScaleModels;
using DataCore.Models;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class Contragent
    {
        #region Public and private fields and properties

        public ContragentEntity? ItemCast { get => Item == null ? null : (ContragentEntity)Item; set => Item = value; }
        private readonly object _locker = new();

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async () =>
                {
                    Table = new TableScaleEntity(ProjectsEnums.TableScale.Contragents);

                    lock (_locker)
                    {
                        ItemCast = null;
                        ButtonSettings = new();
                    }
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
