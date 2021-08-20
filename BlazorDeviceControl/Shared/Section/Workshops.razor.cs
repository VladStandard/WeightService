// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore;
using BlazorCore.DAL;
using BlazorCore.DAL.TableModels;
using BlazorCore.Utils;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Section
{
    public partial class Workshops
    {
        #region Public and private fields and properties

        private List<WorkshopEntity> Items { get; set; }

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationStrings.DeviceControl.Method} {nameof(SetParametersAsync)}", "", LocalizationStrings.Share.DialogResultFail, "",
                new List<Task> {
                    new(async() => {
                        IdItem = null;
                        Items = null;
                        await GuiRefreshWithWaitAsync();

                        Items = AppSettings.DataAccess.WorkshopsCrud.GetEntities(
                            new FieldListEntity(new Dictionary<string, object> { { EnumField.Marked.ToString(), false } }),
                            new FieldOrderEntity(EnumField.Name, EnumOrderDirection.Asc))
                            .OrderBy(x => x.ProductionFacility.Name)
                            .ToList();
                        await GuiRefreshWithWaitAsync();
                    }),
            }, true);
        }

        #endregion
    }
}
