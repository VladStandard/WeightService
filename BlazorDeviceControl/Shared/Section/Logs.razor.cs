// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Models;
using DataCore;
using DataCore.DAL;
using DataCore.DAL.DataModels;
using DataCore.DAL.Models;
using DataCore.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Section
{
    public partial class Logs
    {
        #region Public and private fields and properties

        private List<LogQuickEntity>? ItemsCast => Items?.Select(x => (LogQuickEntity)x).ToList();
        private readonly object _locker = new();

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async () =>
                {
                    Table = new TableSystemEntity(ProjectsEnums.TableSystem.Logs);

                    lock (_locker)
                    {
                        Items = null;
                        ButtonSettings = new();
                    }
                    await GuiRefreshWithWaitAsync();

                    lock (_locker)
                    {
                        if (AppSettings.DataAccess != null)
                        {
                            object[] objects = AppSettings.DataAccess.Crud.GetEntitiesNativeObject(
                                SqlQueries.DbServiceManaging.Tables.Logs.GetLogs);
                            Items = new List<LogQuickEntity>().ToList<BaseEntity>();
                            foreach (object obj in objects)
                            {
                                if (obj is object[] { Length: 11 } item)
                                {
                                    if (Guid.TryParse(Convert.ToString(item[0]), out Guid uid))
                                    {
                                        Items.Add(new LogQuickEntity()
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
                        }
                        ButtonSettings = new(false, true, true, false, false, false, false);
                    }
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
