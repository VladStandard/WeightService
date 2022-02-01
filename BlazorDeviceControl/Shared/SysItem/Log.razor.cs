// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore;
using DataProjectsCore.DAL.Models;
using DataProjectsCore.DAL.TableSystemModels;
using DataProjectsCore.Models;
using DataShareCore;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.SysItem
{
    public partial class Log
    {
        #region Public and private fields and properties

        [Parameter] public string UidStr
        {
            get => Item == null ? string.Empty : Item.Uid.ToString();
            set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                Uid = Guid.Parse(value);
            }
        }
        public LogEntity LogItem { get => (LogEntity)Item; set => Item = value; }

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async () =>
                {
                    lock (Locker)
                    {
                        Table = new TableSystemEntity(ProjectsEnums.TableSystem.Logs);
                        LogItem = AppSettings.DataAccess.Crud.GetEntity<LogEntity>(new FieldListEntity(new Dictionary<string, object>
                            { { ShareEnums.DbField.Uid.ToString(), Uid } }), null);
                        ButtonSettings = new BlazorCore.Models.ButtonSettingsEntity(false, false, false, false, false, false, true);
                    }
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
