// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Sql.Models;
using DataCore.Sql.TableScaleModels;
using DataCore.Localizations;
using DataCore.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Section
{
    public partial class SectionPrinters
    {
        #region Public and private fields and properties

        private List<PrinterEntity> ItemsCast => Items == null ? new() : Items.Select(x => (PrinterEntity)x).ToList();

        #endregion

        #region Constructor and destructor

        public SectionPrinters() : base()
        {
            //
        }

        #endregion

        #region Public and private methods

        private void Default()
        {
            IsLoaded = false;
            Table = new TableScaleEntity(ProjectsEnums.TableScale.Printers);
            Items = new();
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

                    if (AppSettings.DataAccess != null)
                        Items = AppSettings.DataAccess.Crud.GetEntities<PrinterEntity>(
                            new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IsMarked, false } }),
                            new FieldOrderEntity(DbField.Name, DbOrderDirection.Asc),
                            IsSelectTopRows ? AppSettings.DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0)
                        ?.ToList<BaseEntity>();
                    ButtonSettings = new(true, true, true, true, true, false, false);
                    //foreach (PrinterEntity item in ItemsCast)
                    //{
                    //    //await item.SetHttpStatusAsync().ConfigureAwait(true);
                    //}
                    IsLoaded = true;
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
