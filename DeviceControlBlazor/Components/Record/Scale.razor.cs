// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DeviceControlCore;
using DeviceControlCore.DAL;
using DeviceControlCore.DAL.DataModels;
using DeviceControlCore.DAL.TableModels;
using DeviceControlCore.Models;
using Microsoft.AspNetCore.Components;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace DeviceControlBlazor.Components.Record
{
    public partial class Scale
    {
        #region Public and private fields and properties

        public string PluTitle { get; set; }
        public PluEntity PluItem { get; set; }
        public List<PluEntity> PluItems { get; set; } = null;
        public List<ZebraPrinterEntity> PrinterItems { get; set; } = null;
        public List<TemplatesEntity> TemplatesDefaultItems { get; set; } = null;
        public List<TemplatesEntity> TemplatesSeriesItems { get; set; } = null;
        public List<WorkshopEntity> WorkshopItems { get; set; } = null;
        public List<TypeEntity<string>> ComPorts { get; set; }
        public List<HostsEntity> HostItems { get; set; } = null;
        [Parameter]
        public ScalesEntity Item { get; set; }

        #endregion

        #region Public and private methods

        public override async Task GetDataAsync()
        {
            await GetDataAsync(new Task(delegate
            {
                ComPorts = new List<TypeEntity<string>>();
                for (int i = 1; i < 256; i++)
                {
                    ComPorts.Add(new TypeEntity<string>($"COM{i}", $"COM{i}"));
                }
                // ScaleFactor
                Item.ScaleFactor ??= 1000;
                PluTitle = $"{Utils.LocalizationStrings.TableTitlePluShort}  [{Utils.LocalizationStrings.DataLoading}]";
                PluItems = AppSettings.DataAccess.PluCrud.GetEntities(new FieldListEntity(new Dictionary<string, object> {
                    { EnumField.Marked.ToString(), false },
                    { "Scale.Id", Item.Id },
                }),
                new FieldOrderEntity(EnumField.Plu, EnumOrderDirection.Asc)).ToList();
                PluTitle = $"{Utils.LocalizationStrings.TableTitlePluShort}  [{PluItems.Count} {Utils.LocalizationStrings.DataRecords}]";
                TemplatesDefaultItems = AppSettings.DataAccess.TemplatesCrud.GetEntities(
                    new FieldListEntity(new Dictionary<string, object> { { EnumField.Marked.ToString(), false } }),
                    null).ToList();
                TemplatesSeriesItems = AppSettings.DataAccess.TemplatesCrud.GetEntities(
                    new FieldListEntity(new Dictionary<string, object> { { EnumField.Marked.ToString(), false } }),
                    null).ToList();
                WorkshopItems = AppSettings.DataAccess.WorkshopCrud.GetEntities(
                    new FieldListEntity(new Dictionary<string, object> { { EnumField.Marked.ToString(), false } }),
                    null).ToList();
                PrinterItems = AppSettings.DataAccess.ZebraPrinterCrud.GetEntities(
                    new FieldListEntity(new Dictionary<string, object> { { EnumField.Marked.ToString(), false } }),
                    null).ToList();
                HostItems = AppSettings.DataAccess.HostsCrud.GetFreeHosts(Item.Host?.Id);
            }));
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);

            await GetDataAsync().ConfigureAwait(true);
        }

        private async Task RowSelectAsync(BaseIdEntity entity,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            try
            {
                if (entity is PluEntity pluEntity)
                {
                    PluItem = pluEntity;
                }
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

        private async Task RowDoubleClickAsync(BaseIdEntity entity,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            try
            {
                if (entity is PluEntity pluEntity)
                {
                    PluItem = pluEntity;
                    await ActionEditAsync(EnumTable.Plu, PluItem, Item).ConfigureAwait(true);
                }
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
                case "DeviceComPort":
                    if (value is string strValue)
                    {
                        Item.DeviceComPort = strValue;
                    }
                    break;
                case "TemplatesDefault":
                    if (value is int idDefault)
                    {
                        if (idDefault <= 0)
                            Item.TemplateDefault = null;
                        else
                        {
                            Item.TemplateDefault = AppSettings.DataAccess.TemplatesCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> { { EnumField.Id.ToString(), idDefault } }),
                                null);
                        }
                    }
                    break;
                case "TemplatesSeries":
                    if (value is int idSeries)
                    {
                        if (idSeries <= 0)
                            Item.TemplateSeries = null;
                        else
                        {
                            Item.TemplateSeries = AppSettings.DataAccess.TemplatesCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> { { EnumField.Id.ToString(), idSeries } }),
                                null);
                        }
                    }
                    break;
                case "WorkShops":
                    if (value is int idWorkShop)
                    {
                        if (idWorkShop <= 0)
                            Item.WorkShop = null;
                        else
                        {
                            Item.WorkShop = AppSettings.DataAccess.WorkshopCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> { { EnumField.Id.ToString(), idWorkShop } }),
                                null);
                        }
                    }
                    break;
                case "Printers":
                    if (value is int idPrinter)
                    {
                        if (idPrinter <= 0)
                            Item.Printer = null;
                        else
                        {
                            Item.Printer = AppSettings.DataAccess.ZebraPrinterCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> { { EnumField.Id.ToString(), idPrinter } }),
                                null);
                        }
                    }
                    break;
                case "Hosts":
                    if (value is int idHost)
                    {
                        if (idHost <= 0)
                            Item.Host = null;
                        else
                        {
                            Item.Host = AppSettings.DataAccess.HostsCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> { { EnumField.Id.ToString(), idHost } }),
                                null);
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

        private async Task ActionMarkedAsync(EnumTable table, BaseIdEntity entity, BaseIdEntity parentEntity)
        {
            await ActionAsync(table, EnumTableAction.Marked, entity, parentEntity).ConfigureAwait(true);
            await GetDataAsync().ConfigureAwait(false);
        }

        #endregion
    }
}