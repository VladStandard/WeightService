// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore;
using DataCore.DAL;
using DataCore.DAL.TableModels;
using DataCore.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Section
{
    public partial class Nomenclatures
    {
        #region Public and private fields and properties

        private List<NomenclatureEntity> Items { get; set; }

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new List<Task> {
                    new(async() => {
                        Table = new TableScaleEntity(EnumTableScale.Printers);
                        IdItem = null;
                        Items = null;
                        ItemsCount = 0;
                        await GuiRefreshWithWaitAsync();

                        Items = AppSettings.DataAccess.NomenclaturesCrud.GetEntities(
                            //new FieldListEntity(new Dictionary<string, object> { { EnumField.Marked.ToString(), false } }),
                            null,
                            new FieldOrderEntity(EnumField.Name, EnumOrderDirection.Asc))
                            .ToList();
                        ItemsCount = Items.Count;
                        await GuiRefreshWithWaitAsync();
                    }),
            }, true);
        }

        #endregion
    }
}
