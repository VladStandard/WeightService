// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.DAL.Models;
using DataCore.DAL.TableScaleModels;
using DataCore.Localizations;
using DataCore.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class ItemBarCodeType
    {
        #region Public and private fields and properties

        public BarCodeTypeEntityV2 ItemCast { get => Item == null ? new() : (BarCodeTypeEntityV2)Item; set => Item = value; }

        #endregion

        #region Constructor and destructor

        public ItemBarCodeType() : base()
        {
            //
        }

        #endregion

        #region Public and private methods

        private void Default()
        {
            IsLoaded = false;
            Table = new TableScaleEntity(ProjectsEnums.TableScale.BarCodeTypes);
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
                            ItemCast.Name = "NEW BARCODE_TYPE";
                            break;
                        default:
                            ItemCast = AppSettings.DataAccess.Crud.GetEntity<BarCodeTypeEntityV2>(
                                new FieldListEntity(new Dictionary<string, object?> 
                                { { DbField.IdentityUid.ToString(), IdentityUid } }), null);
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
