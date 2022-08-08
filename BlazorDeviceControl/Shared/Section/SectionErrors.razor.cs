﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Sql.TableScaleModels;
using DataCore.Localizations;
using DataCore.Models;
using DataCore.Sql;
using DataCore.Sql.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Section
{
    public partial class SectionErrors
    {
        #region Public and private fields, properties, constructor

        private List<ErrorEntity> ItemsCast => Items == null ? new() : Items.Select(x => (ErrorEntity)x).ToList();

        #endregion

        #region Constructor and destructor

        public SectionErrors() : base()
        {
            //
        }

        #endregion

        #region Public and private methods

        private void Default()
        {
            IsLoaded = false;
            Table = new TableSystemEntity(ProjectsEnums.TableSystem.Errors);
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

                    object[] objects = AppSettings.DataAccess.Crud.GetEntitiesNativeObject(
                        SqlQueries.DbServiceManaging.Tables.Errors.GetErrors(
                            IsSelectTopRows ? AppSettings.DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0));
                    Items = new List<ErrorEntity>().ToList<BaseEntity>();
                    foreach (object obj in objects)
                    {
                        if (obj is object[] { Length: 8 } item)
                        {
                            if (long.TryParse(Convert.ToString(item[0]), out long id))
                            {
                                Items.Add(new ErrorEntity()
                                {
                                    IdentityId = id,
                                    CreateDt = Convert.ToDateTime(item[1]),
                                    ChangeDt = Convert.ToDateTime(item[2]),
                                    FilePath = item[3] is string file ? file : string.Empty,
                                    LineNumber = Convert.ToInt32(item[4]),
                                    MemberName = item[5] is string member ? member : string.Empty,
                                    Exception = item[6] is string ex ? ex : string.Empty,
                                    InnerException = item[7] is string inEx ? inEx : string.Empty,
                                });
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
