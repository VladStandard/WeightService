// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Localizations;
using DataCore.Models;
using DataCore.Sql.Models;
using DataCore.Sql.TableScaleModels;
using Microsoft.AspNetCore.Components;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Section
{
    public partial class SectionContragents
    {
        #region Public and private fields and properties

        private List<ContragentV2Entity> ItemsCast => Items == null ? new() : Items.Select(x => (ContragentV2Entity)x).ToList();

        #endregion

        #region Constructor and destructor

        public SectionContragents() : base()
        {
            //
        }

        #endregion

        #region Public and private methods

        private void Default()
        {
            IsLoaded = false;
            Table = new TableScaleEntity(ProjectsEnums.TableScale.Contragents);
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

                    Items = AppSettings.DataAccess.Crud.GetEntities<ContragentV2Entity>(
                        (IsShowMarkedItems == true) ? null
                            : new FieldListEntity(new() { new(DbField.IsMarked, DbComparer.Equal, false) }),
                        new(DbField.Name, DbOrderDirection.Asc),
                        IsSelectTopRows ? AppSettings.DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0)
                        ?.ToList<BaseEntity>();
                    ButtonSettings = new(true, true, true, true, true, false, false);
                    IsLoaded = true;
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
