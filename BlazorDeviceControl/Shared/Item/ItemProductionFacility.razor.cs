// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Sql.Models;
using DataCore.Sql.TableScaleModels;
using DataCore.Localizations;
using DataCore.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class ItemProductionFacility
    {
        #region Public and private fields and properties

        public ProductionFacilityEntity ItemCast { get => Item == null ? new() : (ProductionFacilityEntity)Item; set => Item = value; }

        #endregion

        #region Constructor and destructor

        public ItemProductionFacility() : base()
        {
            //
        }

        #endregion

        #region Public and private methods

        private void Default()
        {
            IsLoaded = false;
            Table = new TableScaleEntity(ProjectsEnums.TableScale.ProductionFacilities);
            ItemCast = new();
            ButtonSettings = new();
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocaleCore.Action.ActionMethod} {nameof(SetParametersAsync)}", "", LocaleCore.Dialog.DialogResultFail, "",
                new Task(async() => {
                    Default();
                    await GuiRefreshWithWaitAsync();

                    ItemCast = AppSettings.DataAccess.Crud.GetEntity<ProductionFacilityEntity>(
                        new FieldListEntity(new Dictionary<DbField, object?>{ { DbField.IdentityId, IdentityId } }), null);
                    if (IdentityId != null && TableAction == DbTableAction.New)
                        ItemCast.IdentityId = (long)IdentityId;
                    ButtonSettings = new(false, false, false, false, false, true, true);
                    IsLoaded = true;
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
