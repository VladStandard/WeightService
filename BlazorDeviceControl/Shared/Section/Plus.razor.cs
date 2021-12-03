// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore;
using DataProjectsCore.DAL.Models;
using DataProjectsCore.DAL.TableScaleModels;
using DataProjectsCore.Models;
using DataShareCore;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Section
{
    public partial class Plus
    {
        #region Public and private fields and properties

        [Parameter] public int ScaleId { get; set; }
        private List<PluEntity> Items { get; set; }

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async() => {
                    Table = new TableScaleEntity(ProjectsEnums.TableScale.Plus);
                    IdItem = null;
                    Items = null;
                    ItemsCount = 0;
                    await GuiRefreshWithWaitAsync();

                    Items = AppSettings.DataAccess.PlusCrud.GetEntities(
                        new FieldListEntity(
                            new Dictionary<string, object> {
                                { "Scale.Id", ScaleId },
                                { ShareEnums.DbField.Marked.ToString(), false },
                        }),
                        new FieldOrderEntity(ShareEnums.DbField.GoodsName, ShareEnums.DbOrderDirection.Asc))
                        .ToList();
                    ItemsCount = Items.Count;
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
