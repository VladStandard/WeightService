// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.DAL.Models;
using DataCore.DAL.TableScaleModels;
using DataCore.Localizations;
using DataCore.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Section
{
    public partial class SectionPrinterResources
    {
        #region Public and private fields and properties

        private List<PrinterResourceEntity> ItemsCast => Items == null ? new() : Items.Select(x => (PrinterResourceEntity)x).ToList();

        #endregion

        #region Constructor and destructor

        public SectionPrinterResources() : base()
        {
            //
        }

        #endregion

        #region Public and private methods

        private void Default()
        {
            IsLoaded = false;
            Table = new TableScaleEntity(ProjectsEnums.TableScale.PrintersResources);
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
                    {
                        long? printerId = null;
                        if (ItemFilter is PrinterEntity printer)
                            printerId = printer.IdentityId;
                        if (IsShowMarkedItems)
                        {
                            if (printerId == null)
                                Items = AppSettings.DataAccess.Crud.GetEntities<PrinterResourceEntity>(
                                    null,
                                    new FieldOrderEntity(DbField.Description, DbOrderDirection.Asc),
                                    IsSelectTopRows ? AppSettings.DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0)
                                    ?.ToList<BaseEntity>();
                            else
                            {
                                Items = AppSettings.DataAccess.Crud.GetEntities<PrinterResourceEntity>(
                                    new FieldListEntity(new Dictionary<string, object?> { { $"Printer.{DbField.IdentityId}", printerId } }),
                                    new FieldOrderEntity(DbField.Description, DbOrderDirection.Asc),
                                    IsSelectTopRows ? AppSettings.DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0)
                                    ?.ToList<BaseEntity>();
                            }
                        }
                        else
                        {
                            if (printerId == null)
                                Items = AppSettings.DataAccess.Crud.GetEntities<PrinterResourceEntity>(
                                    new FieldListEntity(new Dictionary<string, object?> {
                                        { DbField.IsMarked.ToString(), false } }),
                                    new FieldOrderEntity(DbField.Description, DbOrderDirection.Asc),
                                    IsSelectTopRows ? AppSettings.DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0)
                                    ?.ToList<BaseEntity>();
                            else
                            {
                                Items = AppSettings.DataAccess.Crud.GetEntities<PrinterResourceEntity>(
                                    new FieldListEntity(new Dictionary<string, object?> {
                                        { $"Printer.{DbField.IdentityId}", printerId }, { DbField.IsMarked.ToString(), false } }),
                                    new FieldOrderEntity(DbField.Description, DbOrderDirection.Asc),
                                    IsSelectTopRows ? AppSettings.DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0)
                                    ?.ToList<BaseEntity>();
                            }
                        }

                    }
                    ButtonSettings = new(true, true, true, true, true, false, false);
                    IsLoaded = true;
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
