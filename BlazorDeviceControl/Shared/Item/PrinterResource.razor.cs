// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Models;
using DataCore;
using DataCore.DAL.Models;
using DataCore.DAL.TableScaleModels;
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

        public PrinterResourceEntity PrinterResourceItem { get => (PrinterResourceEntity)Item; set => Item = value; }
        public List<PrinterEntity> PrinterItems { get; set; } = null;
        public List<TemplateResourceEntity> ResourceItems { get; set; } = null;
        private readonly object _locker = new();

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async() => {
                    lock (_locker)
                    {
                        Table = new TableScaleEntity(ProjectsEnums.TableScale.Printers);
                        PrinterResourceItem = AppSettings.DataAccess.Crud.GetEntity<PrinterResourceEntity>(new FieldListEntity(new Dictionary<string, object>
                        { { ShareEnums.DbField.Id.ToString(), Id } }), null);
                        if (Id != null && TableAction == ShareEnums.DbTableAction.New)
                            PrinterResourceItem.Id = (long)Id;
                        PrinterItems = AppSettings.DataAccess.Crud.GetEntities<PrinterEntity>(null, null).ToList();
                        ResourceItems = AppSettings.DataAccess.Crud.GetEntities<TemplateResourceEntity>(null, null).ToList();
                        ButtonSettings = new ButtonSettingsEntity(false, false, false, false, false, true, true);
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
                                    PrinterResourceItem.Printer = null;
                                else
                                {
                                    PrinterResourceItem.Printer = AppSettings.DataAccess.Crud.GetEntity<PrinterEntity>(
                                        new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Id.ToString(), idZebraPrinter } }),
                                    null);
                                }
                            }
                            break;
                        case nameof(TemplateResourceEntity):
                            if (value is long idTemplateResource)
                            {
                                if (idTemplateResource <= 0)
                                    PrinterResourceItem.Printer = null;
                                else
                                {
                                    PrinterResourceItem.Resource = AppSettings.DataAccess.Crud.GetEntity<TemplateResourceEntity>(
                                        new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Id.ToString(), idTemplateResource } }),
                                    null);
                                    if (string.IsNullOrEmpty(PrinterResourceItem.Description))
                                    {
                                        PrinterResourceItem.Description = PrinterResourceItem.Resource.Name;
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
