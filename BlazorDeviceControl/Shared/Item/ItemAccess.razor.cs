// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.DAL.Models;
using DataCore.DAL.TableScaleModels;
using DataCore.Localizations;
using DataCore.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class ItemAccess
    {
        #region Public and private fields and properties

        public AccessEntity ItemCast { get => Item == null ? new() : (AccessEntity)Item; set => Item = value; }
        public List<TypeEntity<AccessRights>>? TemplateAccessRights { get; set; }
        public AccessRights Rights
        {
            get => ItemCast == null ? AccessRights.None : (AccessRights)ItemCast.Rights;
            set { if (ItemCast != null) ItemCast.Rights = (byte)value; }
        }

        #endregion

        #region Constructor and destructor

        public ItemAccess() : base()
        {
            //
        }

        #endregion

        #region Public and private methods

        private void Default()
        {
            IsLoaded = false;
            Table = new TableSystemEntity(ProjectsEnums.TableSystem.Accesses);
            ItemCast = new();
            TemplateAccessRights = AppSettings.DataSourceDics.GetTemplateAccessRights();
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
                            ItemCast.ChangeDt = ItemCast.CreateDt = DateTime.Now;
                            ItemCast.IsMarked = false;
                            ItemCast.User = "NEW USER";
                            break;
                        default:
                            ItemCast = AppSettings.DataAccess.Crud.GetEntity<AccessEntity>(
                                new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IdentityUid, IdentityUid } }), null);
                            break;
                    }
                    TemplateAccessRights = AppSettings.DataSourceDics.GetTemplateAccessRights(ItemCast.Rights);
                    ButtonSettings = new(false, false, false, false, false, true, true);
                    IsLoaded = true;
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
