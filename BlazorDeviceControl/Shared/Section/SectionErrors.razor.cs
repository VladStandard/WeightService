// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.DAL;
using DataCore.DAL.Models;
using DataCore.DAL.TableScaleModels;
using DataCore.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Section
{
    public partial class SectionErrors
    {
        #region Public and private fields and properties

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
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async () =>
                {
                    Default();
                    await GuiRefreshWithWaitAsync();

                    if (AppSettings.DataAccess != null)
                    {
                        object[] objects = AppSettings.DataAccess.Crud.GetEntitiesNativeObject(
                            SqlQueries.DbServiceManaging.Tables.Errors.GetErrors(IsShowTop100 ? 100 : 0));
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
                                        FilePath = Convert.ToString(item[3]),
                                        LineNumber = Convert.ToInt32(item[4]),
                                        MemberName = Convert.ToString(item[5]),
                                        Exception = Convert.ToString(item[6]),
                                        InnerException = Convert.ToString(item[7]),
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
