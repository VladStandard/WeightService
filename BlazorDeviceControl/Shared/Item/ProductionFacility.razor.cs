// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore;
using DataProjectsCore.DAL.Models;
using DataProjectsCore.DAL.TableScaleModels;
using DataProjectsCore.Models;
using DataShareCore;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class ProductionFacility
    {
        #region Public and private fields and properties

        public ProductionFacilityEntity ProductionFacilityItem { get => (ProductionFacilityEntity)IdItem; set => SetItem(value); }

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async() => {
                    Table = new TableScaleEntity(ProjectsEnums.TableScale.ProductionFacilities);
                    ProductionFacilityItem = null;
                    await GuiRefreshWithWaitAsync();

                    ProductionFacilityItem = AppSettings.DataAccess.ProductionFacilitiesCrud.GetEntity(new FieldListEntity(new Dictionary<string, object>
                        { { ShareEnums.DbField.Id.ToString(), Id } }), null);
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
