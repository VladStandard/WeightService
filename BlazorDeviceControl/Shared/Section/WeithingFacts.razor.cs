// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.DAL.DataModels;
using BlazorCore.Utils;
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
        public object[] Objects { get; set; }
        private string TemplateCategory { get; set; }

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationStrings.DeviceControl.Method} {nameof(SetParametersAsync)}", "", LocalizationStrings.Share.DialogResultFail, "",
                new List<Task> {
                    new(async() => {
                        Item = null;
                        Objects = AppSettings.DataAccess.GetEntitiesNativeObject(SqlQueries.GetWeithingFacts, string.Empty, 0, string.Empty);
                        Items = new List<WeithingFactSummaryEntity>();
                        foreach (object obj in Objects)
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
                        await GuiRefreshAsync(false).ConfigureAwait(false);
                    }),
            }, true);
        }

        //private async Task ActionEditAsync(EnumTableScales table, BaseIdEntity item, BaseIdEntity parentEntity)
        //{
        //    // Backup
        //    //await AppSettings.ActionAsync(table, EnumTableAction.Edit, entity, parentEntity).ConfigureAwait(true);
        //    //await SetParametersAsync(new ParameterView()).ConfigureAwait(false);

        //    Task task = null;
        //    string title = LocalizationStrings.DeviceControl.GetItemTitle(table);
        //    switch (table)
        //    {
        //        case EnumTableScales.Printer:
        //            task = new Task(() =>
        //            {
        //                Action(table, EnumTableAction.Edit, item, LocalizationStrings.DeviceControl.UriRouteItemPrinter, false);
        //            });
        //            break;
        //        default:
        //            Action(table, EnumTableAction.Edit, item, LocalizationStrings.DeviceControl.UriRouteItemPrinter, false, parentEntity);
        //            await SetParametersAsync(new ParameterView()).ConfigureAwait(false);
        //            break;
        //    }
        //    await RunTasksAsync(title, "", LocalizationStrings.Share.DialogResultFail, "",
        //        new List<Task> {
        //            task,
        //        }, GuiRefreshAsync, true).ConfigureAwait(false);
        //}

        #endregion
    }
}