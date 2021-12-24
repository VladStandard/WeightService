// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore;
using DataProjectsCore.DAL.Models;
using DataProjectsCore.DAL.TableScaleModels;
using DataProjectsCore.Models;
using DataShareCore;
using DataShareCore.DAL.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Section
{
    public partial class TaskModules
    {
        #region Public and private fields and properties

        public bool Disabled { get => TaskItem == null || TaskItem?.EqualsDefault() == true; }
        public TaskEntity TaskItem { get => (TaskEntity)UidItem; set => UidItem = value; }
        private List<TaskEntity> ItemsCast
        {
            get
            {
                List<TaskEntity> items = Items == null 
                    ? new List<TaskEntity>() 
                    : Items.Select(x => (TaskEntity)x).ToList();
                //ItemsCast.Sort(delegate (TaskEntity a, TaskEntity b) { return string.Compare(a.Scale.Host?.Name, b.Scale.Host?.Name); });
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
                    TaskItem = null;
                    Items = null;
                    Table = new TableScaleEntity(ProjectsEnums.TableScale.Tasks);
                    await GuiRefreshWithWaitAsync();

                    TaskItem = AppSettings.DataAccess.TaskCrud.GetEntity<TaskEntity>(new FieldListEntity(new Dictionary<string, object> {
                        { ShareEnums.DbField.Uid.ToString(), Uid },
                    }), null);
                    Items = TaskItem == null || TaskItem?.EqualsDefault() == true
                        ? AppSettings.DataAccess.TaskCrud.GetEntities<TaskEntity>(null, null)
                            //new FieldOrderEntity(ShareEnums.DbField.Uid, ShareEnums.DbOrderDirection.Asc))
                            .ToList<IBaseEntity>()
                        : AppSettings.DataAccess.TaskCrud.GetEntities<TaskEntity>(
                            new FieldListEntity(
                            new Dictionary<string, object> {
                                { "Scale.Id", TaskItem.Scale.Id },
                            }),
                            null)
                            //new FieldOrderEntity(ShareEnums.DbField.Uid, ShareEnums.DbOrderDirection.Asc))
                            .ToList<IBaseEntity>()
                        ;
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
