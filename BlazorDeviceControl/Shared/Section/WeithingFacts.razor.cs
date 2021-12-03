// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore;
using DataProjectsCore.DAL;
using DataProjectsCore.DAL.DataModels;
using DataProjectsCore.Models;
using DataShareCore;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Section
{
    public partial class WeithingFacts
    {
        #region Public and private fields and properties

        public List<WeithingFactSummaryEntity> Items { get; set; }

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async() => {
                    Table = new TableScaleEntity(ProjectsEnums.TableScale.WeithingFacts);
                    SetItem();
                    Items = null;
                    ItemsCount = 0;
                    await GuiRefreshWithWaitAsync();

                    object[] objects = AppSettings.DataAccess.GetEntitiesNativeObject(
                        SqlQueries.DbScales.Tables.WeithingFacts.GetWeithingFacts, string.Empty, 0, string.Empty);
                    Items = new List<WeithingFactSummaryEntity>();
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
                    ItemsCount = Items.Count;
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
