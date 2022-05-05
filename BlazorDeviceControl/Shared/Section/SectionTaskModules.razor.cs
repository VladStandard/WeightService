// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Sql.Models;
using DataCore.Sql.TableScaleModels;
using DataCore.Localizations;
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

        #endregion

        #region Constructor and destructor

        public SectionTaskModules() : base()
        {
            //
        }

        #endregion

        #region Public and private methods

        private void Default()
        {
            IsLoaded = false;
            Table = new TableSystemEntity(ProjectsEnums.TableSystem.Tasks);
            TaskItem = null;
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

                    if (AppSettings.DataAccess != null)
                    {
                        TaskItem = AppSettings.DataAccess.Crud.GetEntity<TaskEntity>(
                            new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IdentityUid, IdentityUid } }));
                        Items = TaskItem == null || TaskItem.EqualsDefault() == true
                            ? AppSettings.DataAccess.Crud.GetEntities<TaskEntity>(null, null,
                            IsSelectTopRows ? AppSettings.DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0)
                            ?.ToList<BaseEntity>()
                            : AppSettings.DataAccess.Crud.GetEntities<TaskEntity>(
                                new FieldListEntity(new Dictionary<string, object?> { { $"Scale.{DbField.IdentityId}", TaskItem.Scale.IdentityId } }), 
                                null,
                                IsSelectTopRows ? AppSettings.DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0)
                                ?.ToList<BaseEntity>();
                    }
                    ButtonSettings = new(true, true, true, true, true, false, false);
                    IsLoaded = true;
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
