// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Models;
using DataCore;
using DataCore.DAL.Models;
using DataCore.DAL.TableScaleModels;
using DataCore.Models;
using Microsoft.AspNetCore.Components;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class PrinterResource
    {
        #region Public and private fields and properties

        public PrinterResourceEntity? ItemCast { get => Item == null ? null : (PrinterResourceEntity)Item; set => Item = value; }
        public List<PrinterEntity>? PrinterItems { get; set; } = null;
        public List<TemplateResourceEntity>? ResourceItems { get; set; } = null;
        private readonly object _locker = new();

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async() => {
                    Table = new TableScaleEntity(ProjectsEnums.TableScale.PrintersResources);

                    lock (_locker)
                    {
                        ItemCast = null;
                        PrinterItems = null;
                        ResourceItems = null;
                        ButtonSettings = new();
                    }
                    await GuiRefreshWithWaitAsync();

                    lock (_locker)
                    {
                        ItemCast = AppSettings.DataAccess.Crud.GetEntity<PrinterResourceEntity>(
                            new FieldListEntity(new Dictionary<string, object?>
                            { { ShareEnums.DbField.Id.ToString(), Id } }), null);
                        if (Id != null && TableAction == ShareEnums.DbTableAction.New)
                            ItemCast.Id = (long)Id;
                        PrinterItems = AppSettings.DataAccess.Crud.GetEntities<PrinterEntity>(
                            new FieldListEntity(new Dictionary<string, object?> { { ShareEnums.DbField.Marked.ToString(), false } }),
                            null)?.ToList();
                        ResourceItems = AppSettings.DataAccess.Crud.GetEntities<TemplateResourceEntity>(null, null)?.ToList();
                        ButtonSettings = new(false, false, false, false, false, true, true);
                    }
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        private void OnChange(object value, string name,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                lock (_locker)
                {
                    switch (name)
                    {
                        case nameof(PrinterEntity):
                            if (value is long idZebraPrinter)
                            {
                                if (idZebraPrinter <= 0)
                                    ItemCast.Printer = null;
                                else
                                {
                                    ItemCast.Printer = AppSettings.DataAccess.Crud.GetEntity<PrinterEntity>(
                                        new FieldListEntity(new Dictionary<string, object?> { { ShareEnums.DbField.Id.ToString(), idZebraPrinter } }),
                                    null);
                                }
                            }
                            break;
                        case nameof(TemplateResourceEntity):
                            if (value is long idTemplateResource)
                            {
                                if (idTemplateResource <= 0)
                                    ItemCast.Printer = null;
                                else
                                {
                                    ItemCast.Resource = AppSettings.DataAccess.Crud.GetEntity<TemplateResourceEntity>(
                                        new FieldListEntity(new Dictionary<string, object?> { { ShareEnums.DbField.Id.ToString(), idTemplateResource } }),
                                    null);
                                    if (string.IsNullOrEmpty(ItemCast.Description))
                                    {
                                        ItemCast.Description = ItemCast.Resource.Name;
                                    }
                                }
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                NotificationMessage msg = new()
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"{LocalizationCore.Strings.Main.MethodError} [{nameof(OnChange)}]!",
                    Detail = ex.Message,
                    Duration = AppSettingsHelper.Delay
                };
                NotificationService.Notify(msg);
                AppSettings.DataAccess.Crud.LogExceptionToSql(ex, filePath, lineNumber, memberName);
            }
            finally
            {
                StateHasChanged();
            }
        }

        #endregion
    }
}
