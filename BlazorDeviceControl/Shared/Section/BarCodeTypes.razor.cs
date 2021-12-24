// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore;
using DataProjectsCore.DAL.Models;
using DataProjectsCore.DAL.TableScaleModels;
using DataProjectsCore.Models;
using DataShareCore;
using DataShareCore.DAL.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Section
{
    public partial class BarcodeTypes
    {
        #region Public and private fields and properties

        private List<BarcodeTypeEntity> ItemsCast => Items == null ? new List<BarcodeTypeEntity>() : Items.Select(x => (BarcodeTypeEntity)x).ToList();

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async() => {
                    Table = new TableScaleEntity(ProjectsEnums.TableScale.Printers);
                    IdItem = null;
                    Items = null;
                    await GuiRefreshWithWaitAsync();

                    Items = AppSettings.DataAccess.BarcodeTypesCrud.GetEntities<BarcodeTypeEntity>(
                        null,
                        new FieldOrderEntity(ShareEnums.DbField.Name, ShareEnums.DbOrderDirection.Asc))
                        .ToList<IBaseEntity>();
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
