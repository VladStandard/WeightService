// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.DAL;
using DataCore.DAL.DataModels;
using DataCore.DAL.Models;
using DataCore.Localization;
using DataCore.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Section
{
    public partial class SectionLabels
    {
        #region Public and private fields and properties

        private List<LabelQuickEntity> ItemsCast => Items == null ? new() : Items.Select(x => (LabelQuickEntity)x).ToList();

        #endregion

        #region Constructor and destructor

        public SectionLabels() : base()
        {
            //
        }

        #endregion

        #region Public and private methods

        private void Default()
        {
            IsLoaded = false;
            Table = new TableScaleEntity(ProjectsEnums.TableScale.Labels);
            Items = new();
            ButtonSettings = new();
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{Core.Strings.Method} {nameof(SetParametersAsync)}", "", Core.Strings.DialogResultFail, "",
                new Task(async () =>
                {
                    Default();
                    await GuiRefreshWithWaitAsync();

                    if (AppSettings.DataAccess != null)
                    {
                        object[] objects = AppSettings.DataAccess.Crud.GetEntitiesNativeObject(
                            SqlQueries.DbScales.Tables.Labels.GetLabels);
                        Items = new List<LabelQuickEntity>().ToList<BaseEntity>();
                        foreach (object obj in objects)
                        {
                            if (obj is object[] { Length: 14 } item)
                            {
                                if (long.TryParse(Convert.ToString(item[0]), out long id))
                                {
                                    Items.Add(new LabelQuickEntity()
                                    {
                                        IdentityId = id,
                                        CreateDt = Convert.ToDateTime(item[1]),
                                        //Label = Convert.ToByte(item[2]),
                                        ScaleId = Convert.ToInt64(item[3]),
                                        ScaleDescription = Convert.ToString(item[4]),
                                        PluId = Convert.ToInt32(item[5]),
                                        WeithingDate = Convert.ToDateTime(item[6]),
                                        NetWeight = Convert.ToDecimal(item[7]),
                                        TareWeight = Convert.ToDecimal(item[8]),
                                        ProductDate = Convert.ToDateTime(item[9]),
                                        RegNum = Convert.ToInt32(item[10]),
                                        Kneading = Convert.ToInt32(item[11]),
                                        Zpl = Convert.ToString(item[12]),
                                    });
                                }
                            }
                        }
                    }
                    ButtonSettings = new(true, true, true, false, false, false, false);
                    IsLoaded = true;
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
