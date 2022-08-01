// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Localizations;
using DataCore.Models;
using DataCore.Sql.Models;
using DataCore.Sql.TableScaleModels;
using Microsoft.AspNetCore.Components;
using Radzen;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Section
{
    public partial class SectionAccess
    {
        #region Public and private fields and properties

        private List<AccessEntity> ItemsCast => Items == null ? new() : Items.Select(x => (AccessEntity)x).ToList();

        #endregion

        #region Constructor and destructor

        public SectionAccess() : base()
        {
            //
        }

        #endregion

        #region Public and private methods

        private void Default()
        {
            IsLoaded = false;
            Table = new TableSystemEntity(ProjectsEnums.TableSystem.Accesses);
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

                    Items = AppSettings.DataAccess.Crud.GetEntities<AccessEntity>(
                        (IsShowMarkedItems == true) ? null
                            : new FieldListEntity(new() { new(DbField.IsMarked, DbComparer.Equal, false) }),
                        new(DbField.User, DbOrderDirection.Asc),
                        IsSelectTopRows ? AppSettings.DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0)
                    ?.ToList<BaseEntity>();
                    ButtonSettings = new(true, false, true, true, true, false, false);
                    IsLoaded = true;
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        public void RowRender(RowRenderEventArgs<AccessEntity> args)
        {
            args.Attributes.Add("class", UserSettings.Identity.GetColorAccessRights(args.Data.Rights));
            //RowCounter += 1;
        }

        #endregion
    }
}
