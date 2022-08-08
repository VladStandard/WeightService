// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Localizations;
using DataCore.Models;
using DataCore.Sql;
using DataCore.Sql.DataModels;
using DataCore.Sql.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Section.Measurements
{
    public partial class SectionWeithingFactsAggregation
    {
        #region Public and private fields, properties, constructor

        private List<WeithingFactSummaryEntity> ItemsCast => Items == null ? new() : Items.Select(x => (WeithingFactSummaryEntity)x).ToList();

        #endregion

        #region Constructor and destructor

        public SectionWeithingFactsAggregation() : base()
        {
            //
        }

        #endregion

        #region Public and private methods

        private void Default()
        {
            IsLoaded = false;
            Table = new TableScaleEntity(ProjectsEnums.TableScale.WeithingFacts);
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
                        SqlQueries.DbScales.Tables.WeithingFacts.GetWeithingFacts(
                            IsSelectTopRows ? AppSettings.DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0));
                    Items = new List<WeithingFactSummaryEntity>().ToList<BaseEntity>();
                    foreach (object obj in objects)
                    {
                        if (obj is object[] { Length: 5 } item)
                        {
                            Items.Add(new WeithingFactSummaryEntity
                            {
                                WeithingDate = Convert.ToDateTime(item[0]),
                                Count = Convert.ToInt32(item[1]),
                                Scale = item[2] is string scale ? scale : string.Empty,
                                Host = item[3] is string host ? host : string.Empty,
                                Printer = item[4] is string printer ? printer : string.Empty,
                            });
                        }
                    }

                    ButtonSettings = new(true, true, true, true, true, false, false);
                    IsLoaded = true;
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
