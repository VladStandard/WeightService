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

namespace BlazorDeviceControl.Shared.Item
{
    public partial class ItemWorkshop
    {
        #region Public and private fields and properties

        public WorkShopEntity? ItemCast { get => Item == null ? null : (WorkShopEntity)Item; set => Item = value; }
        public List<ProductionFacilityEntity>? ProductionFacilities { get; set; } = null;

        #endregion

        #region Constructor and destructor

        public ItemWorkshop() : base()
        {
            //Default();
        }

        #endregion

        #region Public and private methods

        private void Default()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                Table = new TableScaleEntity(ProjectsEnums.TableScale.Workshops);
                ItemCast = null;
                ProductionFacilities = null;
                ButtonSettings = new();
                IsBusy = false;
            }
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async() => {
                    Default();
                    await GuiRefreshWithWaitAsync();

                    if (!IsBusy)
                    {
                        IsBusy = true;
                        ItemCast = AppSettings.DataAccess.Crud.GetEntity<WorkShopEntity>(
                            new FieldListEntity(new Dictionary<string, object?>{ { DbField.IdentityId.ToString(), IdentityId } }), null);
                        if (IdentityId != null && TableAction == DbTableAction.New)
                            ItemCast.IdentityId = (long)IdentityId;
                        ProductionFacilities = AppSettings.DataAccess.Crud.GetEntities<ProductionFacilityEntity>(null, null)?.ToList();
                        ButtonSettings = new(false, false, false, false, false, true, true);
                        IsBusy = false;
                    }
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
