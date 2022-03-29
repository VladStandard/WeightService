// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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
    public partial class SectionWeithingFacts
    {
        #region Public and private fields and properties

        private List<WeithingFactSummaryEntity>? ItemsCast => Items?.Select(x => (WeithingFactSummaryEntity)x).ToList();
        private readonly object _locker = new();

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async() => {
                    lock (_locker)
                    {
                        Table = new TableScaleEntity(ProjectsEnums.TableScale.WeithingFacts);
                        Items = null;
                        ButtonSettings = new();
                    }
                    await GuiRefreshWithWaitAsync();

                    lock (_locker)
                    {
                        if (AppSettings.DataAccess != null)
                        {
                            object[] objects = AppSettings.DataAccess.Crud.GetEntitiesNativeObject(
                                SqlQueries.DbScales.Tables.WeithingFacts.GetWeithingFacts);
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
                        ButtonSettings = new(true, true, true, true, true, false, false);
                    }
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
