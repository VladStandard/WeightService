// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore;
using DataProjectsCore.DAL;
using DataProjectsCore.Models;
using DataShareCore;
using DataShareCore.DAL.DataModels;
using DataShareCore.DAL.Interfaces;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Sys
{
    public partial class Logs
    {
        #region Public and private fields and properties

        private List<LogSummaryEntity> ItemsCast => Items == null ? new List<LogSummaryEntity>() : Items.Select(x => (LogSummaryEntity)x).ToList();

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async() => {
                    Table = new TableSystemEntity(ProjectsEnums.TableSystem.Logs);
                    UidItem = null;
                    Items = null;
                    await GuiRefreshWithWaitAsync();

                    object[] objects = AppSettings.DataAccess.GetEntitiesNativeObject(SqlQueries.DbServiceManaging.Tables.Logs.GetLogs, string.Empty, 0, string.Empty);
                    Items = new List<LogSummaryEntity>().ToList<IBaseEntity>();
                    foreach (object obj in objects)
                    {
                        if (obj is object[] { Length: 11 } item)
                        {
                            if (Guid.TryParse(Convert.ToString(item[0]), out Guid uid))
                            {
                                Items.Add(new LogSummaryEntity()
                                {
                                    Uid = uid,
                                    CreateDt = Convert.ToDateTime(item[1]),
                                    Scale = Convert.ToString(item[2]),
                                    Host = Convert.ToString(item[3]),
                                    App = Convert.ToString(item[4]),
                                    Version = Convert.ToString(item[5]),
                                    File = Convert.ToString(item[6]),
                                    Line = Convert.ToInt32(item[7]),
                                    Member = Convert.ToString(item[8]),
                                    Icon = Convert.ToString(item[9]),
                                    Message = Convert.ToString(item[10]),
                                });
                            }
                        }
                    }
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
