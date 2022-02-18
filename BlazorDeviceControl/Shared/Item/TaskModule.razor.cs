// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Models;
using DataCore;
using DataCore.DAL.Models;
using DataCore.DAL.TableScaleModels;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class TaskModule
    {
        #region Public and private fields and properties

        public TaskEntity TaskItem { get => (TaskEntity)Item; set => Item = value; }

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async () =>
                {
                    Table = new TableSystemEntity(ProjectsEnums.TableSystem.Tasks);
                    TaskItem = AppSettings.DataAccess.Crud.GetEntity<TaskEntity>(new FieldListEntity(new Dictionary<string, object> {
                        { ShareEnums.DbField.Uid.ToString(), Uid },
                    }), null);
                    if (Id != null && TableAction == ShareEnums.DbTableAction.New)
                        TaskItem.Id = (int)Id;
                    ButtonSettings = new ButtonSettingsEntity(false, false, false, false, false, true, true);
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
