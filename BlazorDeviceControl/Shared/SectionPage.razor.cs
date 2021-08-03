// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore;
using BlazorCore.DAL;
using BlazorCore.DAL.DataModels;
using BlazorCore.Models;
using Microsoft.AspNetCore.Components;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BlazorCore.Utils;

namespace BlazorDeviceControl.Shared
{
    public partial class SectionPage
    {
        #region Public and private fields and properties

        private void ShowTooltipGetData(ElementReference elementReference, TooltipOptions options = null) =>
        Tooltip.Open(elementReference, LocalizationStrings.DeviceControl.TableReadData, options);
        ChartCountEntity[] ContragentsChartCreated { get; set; }
        ChartCountEntity[] ContragentsChartModified { get; set; }
        ChartCountEntity[] NomenclaturesChartCreated { get; set; }
        ChartCountEntity[] NomenclaturesChartModified { get; set; }
        [Parameter]
        public EnumTable Table { get; set; }
        [Parameter]
        public BaseIdEntity Entity { get; set; }
        [Parameter]
        public BaseIdEntity[] Entities { get; set; }
        private List<TypeEntity<string>> TemplateCategories { get; set; }
        private string TemplateCategory { get; set; }

        #endregion

        #region Public and private methods

        public override async Task GetDataAsync()
        {
            await GetDataAsync(new Task(delegate
            {
                Entities = null;
                switch (Table)
                {
                    case EnumTable.Hosts:
                        Entities = AppSettings.DataAccess.HostsCrud.GetEntities(
                            new FieldListEntity(new Dictionary<string, object> { { EnumField.Marked.ToString(), false } }),
                            new FieldOrderEntity(EnumField.Name, EnumOrderDirection.Asc));
                        break;
                    case EnumTable.Plu:
                        Entities = AppSettings.DataAccess.PluCrud.GetEntities(
                            new FieldListEntity(new Dictionary<string, object> { { EnumField.Marked.ToString(), false } }),
                            new FieldOrderEntity(EnumField.Name, EnumOrderDirection.Asc));
                        break;
                    case EnumTable.ProductionFacility:
                        Entities = AppSettings.DataAccess.ProductionFacilityCrud.GetEntities(
                            new FieldListEntity(new Dictionary<string, object> { { EnumField.Marked.ToString(), false } }),
                            new FieldOrderEntity(EnumField.Name, EnumOrderDirection.Asc));
                        break;
                    case EnumTable.TemplateResources:
                        Entities = AppSettings.DataAccess.TemplateResourcesCrud.GetEntities(
                            new FieldListEntity(new Dictionary<string, object> { { EnumField.Marked.ToString(), false } }),
                            new FieldOrderEntity(EnumField.Name, EnumOrderDirection.Asc));
                        break;
                    case EnumTable.Templates:
                        if (TemplateCategories == null || TemplateCategories.Count == 0)
                            TemplateCategories = AppSettings.DataSource.GetTemplateCategories();
                        if (string.IsNullOrEmpty(TemplateCategory))
                        {
                            TemplateCategory = TemplateCategories.FirstOrDefault()?.Value;
                            Entities = AppSettings.DataAccess.TemplatesCrud.GetEntities(
                                new FieldListEntity(new Dictionary<string, object> { { EnumField.Marked.ToString(), false } }),
                                new FieldOrderEntity(EnumField.Title, EnumOrderDirection.Asc));
                        }
                        else
                        {
                            Entities = AppSettings.DataAccess.TemplatesCrud.GetEntities(
                                new FieldListEntity(new Dictionary<string, object>
                                {
                            { EnumField.Marked.ToString(), false },
                            { EnumField.CategoryId.ToString(), TemplateCategory },
                                                            }),
                                new FieldOrderEntity(EnumField.Title, EnumOrderDirection.Asc));
                        }
                        break;
                    case EnumTable.WorkShop:
                        Entities = AppSettings.DataAccess.WorkshopCrud.GetEntities(
                            new FieldListEntity(new Dictionary<string, object> { { EnumField.Marked.ToString(), false } }),
                            new FieldOrderEntity(EnumField.Name, EnumOrderDirection.Asc));
                        break;
                    case EnumTable.PrinterType:
                        Entities = AppSettings.DataAccess.ZebraPrinterTypeCrud.GetEntities(null,
                        new FieldOrderEntity(EnumField.Name, EnumOrderDirection.Asc));
                        break;
                }

                // ChartDataCreated.
                ContragentsChartCreated = GetContragentsChartEntities(EnumField.CreateDate);
                // ChartDataModified.
                ContragentsChartModified = GetContragentsChartEntities(EnumField.ModifiedDate);

                // ChartDataCreated.
                NomenclaturesChartCreated = GetNomenclaturesChartEntities(EnumField.CreateDate);
                // ChartDataModified.
                NomenclaturesChartModified = GetNomenclaturesChartEntities(EnumField.ModifiedDate);
            }));
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);

            if (parameters.TryGetValue("Table", out EnumTable table)) { Table = table; }

            await GetDataAsync().ConfigureAwait(true);
        }

        private string GetHeader()
        {
            return Table switch
            {
                EnumTable.BarCodeTypes => LocalizationStrings.DeviceControl.TableTitleBarCodeTypesShort,
                EnumTable.Contragents => LocalizationStrings.DeviceControl.TableTitleContragentsShort,
                EnumTable.Nomenclature => LocalizationStrings.DeviceControl.TableTitleNomenclatureShort,
                EnumTable.Orders => LocalizationStrings.DeviceControl.TableTitleOrderStatusShort,
                EnumTable.OrderStatus => LocalizationStrings.DeviceControl.TableTitleOrderStatusShort,
                EnumTable.OrderTypes => LocalizationStrings.DeviceControl.TableTitleOrderTypesShort,
                EnumTable.Plu => LocalizationStrings.DeviceControl.TableTitlePluShort,
                EnumTable.ProductionFacility => LocalizationStrings.DeviceControl.TableTitleProductionFacilityShort,
                EnumTable.ProductSeries => LocalizationStrings.DeviceControl.TableTitleProductSeriesShort,
                EnumTable.Scales => LocalizationStrings.DeviceControl.TableTitleScalesShort,
                EnumTable.SsccStorage => LocalizationStrings.DeviceControl.TableTitleSsccStorageShort,
                EnumTable.TemplateResources => LocalizationStrings.DeviceControl.TableTitleTemplateResourcesShort,
                EnumTable.Templates => LocalizationStrings.DeviceControl.TableTitleTemplatesShort,
                EnumTable.WeithingFact => LocalizationStrings.DeviceControl.TableTitleWeithingFactShort,
                EnumTable.WorkShop => LocalizationStrings.DeviceControl.TableTitleWorkShopShort,
                _ => string.Empty
            };
        }

        private async Task RowSelectAsync(BaseIdEntity entity,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            try
            {
                Entity = entity;
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
                Entity = entity;
                await ActionEditAsync(Table, Entity, null).ConfigureAwait(true);
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

        private async Task ActionEditAsync(EnumTable table, BaseIdEntity item, BaseIdEntity parentEntity)
        {
            if (AppSettings.IdentityAccessLevel != true)
                return;

            await ActionAsync(table, EnumTableAction.Edit, item, parentEntity).ConfigureAwait(true);
            await GetDataAsync().ConfigureAwait(false);
        }

        private async Task ActionAddAsync(EnumTable table, BaseIdEntity entity, BaseIdEntity parentEntity)
        {
            if (AppSettings.IdentityAccessLevel != true)
                return;

            await ActionAsync(table, EnumTableAction.Add, entity, parentEntity).ConfigureAwait(true);
            await GetDataAsync().ConfigureAwait(false);
        }

        private async Task ActionCopyAsync(EnumTable table, BaseIdEntity entity, BaseIdEntity parentEntity)
        {
            if (AppSettings.IdentityAccessLevel != true)
                return;

            await ActionAsync(table, EnumTableAction.Copy, entity, parentEntity).ConfigureAwait(true);
            await GetDataAsync().ConfigureAwait(false);
        }

        private async Task ActionDeleteAsync(EnumTable table, BaseIdEntity entity, BaseIdEntity parentEntity)
        {
            if (AppSettings.IdentityAccessLevel != true)
                return;

            await ActionAsync(table, EnumTableAction.Delete, entity, parentEntity).ConfigureAwait(true);
            await GetDataAsync().ConfigureAwait(false);
        }

        private async Task ActionMarkedAsync(EnumTable table, BaseIdEntity entity, BaseIdEntity parentEntity)
        {
            if (AppSettings.IdentityAccessLevel != true)
                return;

            await ActionAsync(table, EnumTableAction.Marked, entity, parentEntity).ConfigureAwait(true);
            await GetDataAsync().ConfigureAwait(false);
        }

        private async Task OnChange(object value, string name)
        {
            switch (name)
            {
                case "TemlateCategories":
                    if (value is string strValue)
                    {
                        TemplateCategory = strValue;
                    }
                    break;
            }
            StateHasChanged();
            await GetDataAsync().ConfigureAwait(false);
        }

        #endregion
    }
}