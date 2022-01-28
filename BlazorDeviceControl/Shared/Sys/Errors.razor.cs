// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore;
using DataProjectsCore.DAL.Models;
using DataProjectsCore.DAL.TableSystemModels;
using DataProjectsCore.Models;
using DataShareCore;
using DataShareCore.DAL.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Sys
{
    public partial class Errors
    {
        #region Public and private fields and properties

        private List<ErrorEntity> ItemsCast => Items == null ? new List<ErrorEntity>() : Items.Select(x => (ErrorEntity)x).ToList();

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async () =>
                {
                    lock (Locker)
                    {
                        Table = new TableSystemEntity(ProjectsEnums.TableSystem.Errors);
                        Items = AppSettings.DataAccess.Crud.GetEntities<ErrorEntity>(null,
                            new FieldOrderEntity(ShareEnums.DbField.Name, ShareEnums.DbOrderDirection.Asc))
                            .ToList<BaseEntity>();
                        ButtonSettings = new BlazorCore.Models.ButtonSettingsEntity(true, true, true, true, true);
                    }
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
