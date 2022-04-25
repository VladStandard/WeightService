// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.DAL;
using DataCore.DAL.DataModels;
using DataCore.DAL.Models;
using DataCore.Localizations;
using DataCore.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Section
{
    public partial class SectionLogs
    {
        #region Public and private fields and properties

        private List<LogQuickEntity> ItemsCast => Items == null ? new() : Items.Select(x => (LogQuickEntity)x).ToList();

        #endregion

        #region Constructor and destructor

        public SectionLogs() : base()
        {
            //
        }

        #endregion

        #region Public and private methods

        private void Default()
        {
            IsLoaded = false;
            Table = new TableSystemEntity(ProjectsEnums.TableSystem.Logs);
            Items = new();
            ButtonSettings = new();
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocaleCore.Action.ActionMethod} {nameof(SetParametersAsync)}", "", LocaleCore.Dialog.DialogResultFail, "",
                new Task(async () =>
                {
                    Default();
                    await GuiRefreshWithWaitAsync();

                    if (AppSettings.DataAccess != null)
                    {
                        object[] objects = AppSettings.DataAccess.Crud.GetEntitiesNativeObject(
                            SqlQueries.DbServiceManaging.Tables.Logs.GetLogs(
                                IsSelectTopRows ? AppSettings.DataAccess.JsonSettings.SelectTopRowsCount : 0));
                        Items = new List<LogQuickEntity>().ToList<BaseEntity>();
                        foreach (object obj in objects)
                        {
                            if (obj is object[] { Length: 11 } item)
                            {
                                if (Guid.TryParse(Convert.ToString(item[0]), out Guid uid))
                                {
                                    Items.Add(new LogQuickEntity()
                                    {
                                        IdentityUid = uid,
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
                    IsLoaded = true;
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
