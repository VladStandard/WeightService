// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore;
using DataProjectsCore.DAL.Models;
using DataProjectsCore.DAL.TableScaleModels;
using DataProjectsCore.Models;
using DataShareCore;
using DataShareCore.DAL.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Section
{
    public partial class PrinterResources
    {
        #region Public and private fields and properties

        [Parameter] public int? PrinterId { get; set; }
        private List<PrinterResourceEntity> ItemsCast => Items == null ? new List<PrinterResourceEntity>() : Items.Select(x => (PrinterResourceEntity)x).ToList();

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async () =>
                {
                    lock (Locker)
                    {
                        Table = new TableScaleEntity(ProjectsEnums.TableScale.PrintersResources);
                        Items = AppSettings.DataAccess.Crud.GetEntities<PrinterResourceEntity>(
                            new FieldListEntity(new Dictionary<string, object> { { "Printer.Id", PrinterId } }),
                            new FieldOrderEntity(ShareEnums.DbField.Description, ShareEnums.DbOrderDirection.Asc))
                            .ToList<BaseEntity>();
                        ButtonSettings = new BlazorCore.Models.ButtonSettingsEntity(true, true, true, true, true);
                    }
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
