// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Localizations;
using DataCore.Models;
using DataCore.Sql.TableScaleModels;
using Microsoft.AspNetCore.Components;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class ItemContragent
    {
        #region Public and private fields and properties

        public ContragentV2Entity ItemCast { get => Item == null ? new() : (ContragentV2Entity)Item; set => Item = value; }

        #endregion

        #region Constructor and destructor

        public ItemContragent()
        {
            //
        }

        #endregion

        #region Public and private methods

        private void Default()
        {
            IsLoaded = false;
            Table = new TableScaleEntity(ProjectsEnums.TableScale.Contragents);
            ItemCast = new();
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

                    switch (TableAction)
                    {
                        case DbTableAction.New:
                            ItemCast = new();
                            ItemCast.ChangeDt = ItemCast.CreateDt = System.DateTime.Now;
                            ItemCast.IsMarked = false;
                            ItemCast.Name = "NEW CONTRAGENT";
                            break;
                        default:
                            ItemCast = AppSettings.DataAccess.Crud.GetEntity<ContragentV2Entity>(
                                new(new() { new(DbField.IdentityUid, DbComparer.Equal, IdentityUid) }));
                            break;
                    }
                    ButtonSettings = new(false, false, false, false, false, true, true);
                    IsLoaded = true;
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
