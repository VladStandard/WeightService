// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore.DAL.Models;
using DataProjectsCore.DAL.TableScaleModels;
using DataShareCore;
using DataShareCore.DAL.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Section
{
    public partial class Tasks
    {
        #region Public and private fields and properties

        private List<TaskEntity> ItemsCast
        {
            get
            {
                List<TaskEntity> items = Items == null 
                    ? new List<TaskEntity>() 
                    : Items.Select(x => (TaskEntity)x).ToList();
                //ItemsCast.Sort(delegate (TaskEntity a, TaskEntity b) { return a.Scale.Host.Name.CompareTo(b.Scale.Host.Name); });
                items.Sort((a, b) => string.Compare(a.Scale.Host?.Name, b.Scale.Host?.Name));
                return items;
            }
        }

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async () =>
                {
                    IdItem = null;
                    Items = null;
                    await GuiRefreshWithWaitAsync();

                    Items = AppSettings.DataAccess.TaskCrud.GetEntities(
                        null, null)
                        //new FieldOrderEntity(ShareEnums.DbField.Uid, ShareEnums.DbOrderDirection.Asc))
                        .ToList<IBaseEntity>();
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
