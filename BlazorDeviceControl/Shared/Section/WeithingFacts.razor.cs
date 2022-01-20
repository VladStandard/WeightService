// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore;
using DataProjectsCore.DAL;
using DataProjectsCore.DAL.DataModels;
using DataProjectsCore.Models;
using DataShareCore;
using DataShareCore.DAL.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Section
{
    public partial class WeithingFacts
    {
        #region Public and private fields and properties

        private List<WeithingFactSummaryEntity> ItemsCast => Items == null ? new List<WeithingFactSummaryEntity>() : Items.Select(x => (WeithingFactSummaryEntity)x).ToList();

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async() => {
                    lock (Locker)
                    {
                        Table = new TableScaleEntity(ProjectsEnums.TableScale.WeithingFacts);
                        Item = null;
                        Items = null;
                    }
                    await GuiRefreshWithWaitAsync();

                    lock (Locker)
                    {
                        object[] objects = AppSettings.DataAccess.Crud.GetEntitiesNativeObject(SqlQueries.DbScales.Tables.WeithingFacts.GetWeithingFacts);
                        Items = new List<WeithingFactSummaryEntity>().ToList<BaseEntity>();
                        foreach (object obj in objects)
                        {
                            if (obj is object[] { Length: 5 } item)
                            {
                                Items.Add(new WeithingFactSummaryEntity
                                {
                                    WeithingDate = Convert.ToDateTime(item[0]),
                                    Count = Convert.ToInt32(item[1]),
                                    Scale = Convert.ToString(item[2]),
                                    Host = Convert.ToString(item[3]),
                                    Printer = Convert.ToString(item[4]),
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
