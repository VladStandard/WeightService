// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.DAL.Models;
using DataCore.DAL.TableScaleModels;
using DataCore.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Section
{
    public partial class SectionTaskModules
    {
        #region Public and private fields and properties

        public bool Disabled { get => TaskItem == null || TaskItem.EqualsDefault() == true; }
        public TaskEntity? TaskItem { get => Item == null ? null : (TaskEntity)Item; set => Item = value; }
        private List<TaskEntity>? ItemsCast
        {
            get
            {
                List<TaskEntity>? items = Items?.Select(x => (TaskEntity)x).ToList();
                //ItemsCast.Sort(delegate (TaskEntity a, TaskEntity b) { return string.Compare(a.Scale.Host?.Name, b.Scale.Host?.Name); });
                items?.Sort((a, b) => string.Compare(a.Scale.Host?.Name, b.Scale.Host?.Name));
                return items;
            }
        }
        private readonly object _locker = new();

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async () =>
                {
                    lock (_locker)
                    {
                        Table = new TableSystemEntity(ProjectsEnums.TableSystem.Tasks);
                        TaskItem = null;
                        Items = null;
                        ButtonSettings = new();
                    }
                    await GuiRefreshWithWaitAsync();

                    lock (_locker)
                    {
                        if (AppSettings.DataAccess != null)
                        {
                            TaskItem = AppSettings.DataAccess.Crud.GetEntity<TaskEntity>(
                                new FieldListEntity(new Dictionary<string, object?> {
                                { DbField.Uid.ToString(), Uid },
                            }), null);
                            Items = TaskItem == null || TaskItem.EqualsDefault() == true
                                ? AppSettings.DataAccess.Crud.GetEntities<TaskEntity>(null, null)?.ToList<BaseEntity>()
                                : AppSettings.DataAccess.Crud.GetEntities<TaskEntity>(
                                    new FieldListEntity(new Dictionary<string, object?> { { "Scale.Id", TaskItem.Scale.Id } }), null)
                                    ?.ToList<BaseEntity>();
                        }
                        ButtonSettings = new(true, true, true, true, true, false, false);
                    }
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
