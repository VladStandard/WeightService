// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore;
using BlazorCore.DAL;
using BlazorCore.DAL.DataModels;
using BlazorCore.DAL.TableModels;
using Microsoft.AspNetCore.Components;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Record
{
    public partial class PrinterResourceRef
    {
        #region Public and private fields and properties

        public List<ZebraPrinterEntity> PrinterItems { get; set; } = null;
        public List<TemplateResourcesEntity> ResourceItems { get; set; } = null;
        [Parameter]
        public ZebraPrinterResourceRefEntity Item { get; set; }

        #endregion

        #region Public and private methods

        public override async Task GetDataAsync()
        {
            await GetDataAsync(new Task(delegate
            {
                PrinterItems = AppSettings.DataAccess.ZebraPrinterCrud.GetEntities(null, null).ToList();
                ResourceItems = AppSettings.DataAccess.TemplateResourcesCrud.GetEntities(null, null).ToList();
            }));
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);

            await GetDataAsync().ConfigureAwait(true);
        }

        private async Task RowSelectAsync(BaseIdEntity entity, string resourceType,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            try
            {
                //
            }
            catch (Exception ex)
            {
                NotificationMessage msg = new()
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Ошибка метода [{memberName}]!",
                    Detail = ex.Message,
                    Duration = AppSettings.Delay
                };
                Notification.Notify(msg);
                Console.WriteLine(msg.Detail);
                AppSettings.DataAccess.LogExceptionToSql(ex, filePath, lineNumber, memberName);
            }
        }

        private async Task RowDoubleClickAsync(BaseIdEntity entity, string table,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            try
            {
                //
            }
            catch (Exception ex)
            {
                NotificationMessage msg = new()
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Ошибка метода [{memberName}]!",
                    Detail = ex.Message,
                    Duration = AppSettings.Delay
                };
                Notification.Notify(msg);
                Console.WriteLine(msg.Detail);
                AppSettings.DataAccess.LogExceptionToSql(ex, filePath, lineNumber, memberName);
            }
        }

        private void OnChange(object value, string name)
        {
            switch (name)
            {
                case "ZebraPrinter":
                    if (value is int idZebraPrinter)
                    {
                        if (idZebraPrinter <= 0)
                            Item.Printer = null;
                        else
                        {
                            Item.Printer = AppSettings.DataAccess.ZebraPrinterCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> { { EnumField.Id.ToString(), idZebraPrinter } }),
                            null);
                        }
                    }
                    break;
                case "TemplateResource":
                    if (value is int idTemplateResource)
                    {
                        if (idTemplateResource <= 0)
                            Item.Printer = null;
                        else
                        {
                            Item.Resource = AppSettings.DataAccess.TemplateResourcesCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> { { EnumField.Id.ToString(), idTemplateResource } }),
                            null);
                            if (string.IsNullOrEmpty(Item.Description))
                            {
                                Item.Description = Item.Resource.Name;
                            }
                        }
                    }
                    break;
            }
            StateHasChanged();
        }

        private async Task ActionEditAsync(EnumTable table, BaseIdEntity entity, BaseIdEntity parentEntity)
        {
            await ActionAsync(table, EnumTableAction.Edit, entity, parentEntity).ConfigureAwait(true);
            await GetDataAsync().ConfigureAwait(false);
        }

        private async Task ActionAddAsync(EnumTable table, BaseIdEntity entity, BaseIdEntity parentEntity)
        {
            await ActionAsync(table, EnumTableAction.Add, entity, parentEntity).ConfigureAwait(true);
            await GetDataAsync().ConfigureAwait(false);
        }

        private async Task ActionCopyAsync(EnumTable table, BaseIdEntity entity, BaseIdEntity parentEntity)
        {
            await ActionAsync(table, EnumTableAction.Copy, entity, parentEntity).ConfigureAwait(true);
            await GetDataAsync().ConfigureAwait(false);
        }

        private async Task ActionDeleteAsync(EnumTable table, BaseIdEntity entity, BaseIdEntity parentEntity)
        {
            await ActionAsync(table, EnumTableAction.Delete, entity, parentEntity).ConfigureAwait(true);
            await GetDataAsync().ConfigureAwait(false);
        }

        #endregion
    }
}