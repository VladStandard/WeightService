// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore;
using BlazorCore.DAL;
using BlazorCore.DAL.DataModels;
using BlazorCore.DAL.TableModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared
{
    public partial class EntityPage
    {
        #region Public and private fields and properties

        [Parameter] public EnumTable Table { get; set; }
        [Parameter] public BaseIdEntity Item { get; set; }
        [Parameter] public EnumTableAction TableAction { get; set; }
        [Parameter] public EventCallback CallbackActionSaveAsync { get; set; }
        [Parameter] public EventCallback CallbackActionCancelAsync { get; set; }

        #endregion

        #region Public and private methods

        private bool FieldControlDeny(BaseIdEntity entity, string field)
        {
            bool result = entity != null;
            if (entity is BarCodeTypesEntity barCodeTypesEntity)
            {
                if (barCodeTypesEntity.EqualsDefault())
                    result = false;
            }
            else if (entity is ContragentsEntity contragentsEntity)
            {
                if (contragentsEntity.EqualsDefault())
                    result = false;
            }
            else if (entity is HostsEntity hostsEntity)
            {
                if (hostsEntity.EqualsDefault())
                    result = false;
            }
            else if (entity is LabelsEntity labelsEntity)
            {
                if (labelsEntity.EqualsDefault())
                    result = false;
            }
            else if (entity is NomenclatureEntity nomenclatureEntity)
            {
                if (nomenclatureEntity.EqualsDefault())
                    result = false;
            }
            else if (entity is OrdersEntity ordersEntity)
            {
                if (ordersEntity.EqualsDefault())
                    result = false;
            }
            else if (entity is OrderStatusEntity orderStatusEntity)
            {
                if (orderStatusEntity.EqualsDefault())
                    result = false;
            }
            else if (entity is OrderTypesEntity orderTypesEntity)
            {
                if (orderTypesEntity.EqualsDefault())
                    result = false;
            }
            else if (entity is PluEntity pluEntity)
            {
                if (pluEntity.EqualsDefault())
                    result = false;
            }
            else if (entity is ProductionFacilityEntity productionFacilityEntity)
            {
                if (productionFacilityEntity.EqualsDefault())
                    result = false;
            }
            else if (entity is ProductSeriesEntity productSeriesEntity)
            {
                if (productSeriesEntity.EqualsDefault())
                    result = false;
            }
            else if (entity is ScalesEntity scalesEntity)
            {
                if (scalesEntity.EqualsDefault())
                    result = false;
            }
            else if (entity is TemplateResourcesEntity templateResourcesEntity)
            {
                if (templateResourcesEntity.EqualsDefault())
                    result = false;
            }
            else if (entity is TemplatesEntity templatesEntity)
            {
                if (templatesEntity.EqualsDefault())
                    result = false;
            }
            else if (entity is WeithingFactEntity weithingFactEntity)
            {
                if (weithingFactEntity.EqualsDefault())
                    result = false;
            }
            else if (entity is WorkshopEntity workshopEntity)
            {
                if (workshopEntity.EqualsDefault())
                    result = false;
            }
            else if (entity is ZebraPrinterEntity zebraPrinterEntity)
            {
                if (zebraPrinterEntity.EqualsDefault())
                    result = false;
            }
            else if (entity is ZebraPrinterResourceRefEntity zebraPrinterResourceRefEntity)
            {
                if (zebraPrinterResourceRefEntity.EqualsDefault())
                    result = false;
            }
            else if (entity is ZebraPrinterTypeEntity zebraPrinterTypeEntity)
            {
                if (zebraPrinterTypeEntity.EqualsDefault())
                    result = false;
            }
            if (!result)
            {
                NotificationMessage msg = new()
                {
                    Severity = NotificationSeverity.Warning,
                    Summary = "Контроль данных",
                    Detail = $"Необходимо заполнить поле [{field}]!",
                    Duration = BlazorCore.Models.AppSettingsEntity.Delay
                };
                Notification.Notify(msg);
                return false;
            }
            return true;
        }

        private async Task SaveAsync(MouseEventArgs args,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            bool success = true;
            try
            {
                switch (Table)
                {
                    case EnumTable.BarCodeTypes:
                        break;
                    case EnumTable.Hosts:
                        HostsEntity hosts = (HostsEntity)Item;
                        hosts.CreateDate ??= DateTime.Now;
                        hosts.ModifiedDate = DateTime.Now;
                        if (TableAction == EnumTableAction.Add)
                            AppSettings.DataAccess.HostsCrud.SaveEntity(hosts);
                        else
                            AppSettings.DataAccess.HostsCrud.UpdateEntity(hosts);
                        break;
                    case EnumTable.Contragents:
                        ContragentsEntity contragents = (ContragentsEntity)Item;
                        contragents.CreateDate ??= DateTime.Now;
                        contragents.ModifiedDate = DateTime.Now;
                        if (TableAction == EnumTableAction.Add)
                            AppSettings.DataAccess.ContragentsCrud.SaveEntity(contragents);
                        else
                            AppSettings.DataAccess.ContragentsCrud.UpdateEntity(contragents);
                        break;
                    case EnumTable.Nomenclature:
                        NomenclatureEntity nomenclature = (NomenclatureEntity)Item;
                        nomenclature.CreateDate ??= DateTime.Now;
                        nomenclature.ModifiedDate = DateTime.Now;
                        if (TableAction == EnumTableAction.Add)
                            AppSettings.DataAccess.NomenclatureCrud.SaveEntity(nomenclature);
                        else
                            AppSettings.DataAccess.NomenclatureCrud.UpdateEntity(nomenclature);
                        break;
                    case EnumTable.Orders:
                        break;
                    case EnumTable.OrderStatus:
                        break;
                    case EnumTable.OrderTypes:
                        break;
                    case EnumTable.Plu:
                        PluEntity plu = (PluEntity)Item;
                        plu.ModifiedDate = DateTime.Now;
                        success = FieldControlDeny(plu.Scale, "Устройство");
                        if (success)
                            success = FieldControlDeny(plu.Templates, "Шаблон");
                        if (success)
                            success = FieldControlDeny(plu.Nomenclature, "Номенклатура");
                        if (success)
                        {
                            // Контроль номера PLU.
                            PluEntity[] pluEntities = AppSettings.DataAccess.PluCrud.GetEntities(
                                new FieldListEntity(new Dictionary<string, object>
                                {
                                { "Scale.Id", plu.Scale.Id },
                                { EnumField.Plu.ToString(), plu.Plu },
                                                                    }),
                                null);
                            if (pluEntities.Any() && !pluEntities.Where(x => x.Id.Equals(Item.Id)).Select(x => x).Any())
                            {
                                NotificationMessage msg = new()
                                {
                                    Severity = NotificationSeverity.Warning,
                                    Summary = "Контроль данных",
                                    Detail = $"Таблица PLU уже имеет такой номер [{plu.Plu}]!",
                                    Duration = BlazorCore.Models.AppSettingsEntity.Delay
                                };
                                Notification.Notify(msg);
                            }
                            else
                            {
                                if (TableAction == EnumTableAction.Add)
                                    AppSettings.DataAccess.PluCrud.SaveEntity(plu);
                                else
                                    AppSettings.DataAccess.PluCrud.UpdateEntity(plu);
                            }
                        }
                        break;
                    case EnumTable.ProductionFacility:
                        ProductionFacilityEntity productionFacility = (ProductionFacilityEntity)Item;
                        productionFacility.CreateDate ??= DateTime.Now;
                        productionFacility.ModifiedDate = DateTime.Now;
                        if (TableAction == EnumTableAction.Add)
                            AppSettings.DataAccess.ProductionFacilityCrud.SaveEntity(productionFacility);
                        else
                            AppSettings.DataAccess.ProductionFacilityCrud.UpdateEntity(productionFacility);
                        break;
                    case EnumTable.ProductSeries:
                        ProductSeriesEntity productSeries = (ProductSeriesEntity)Item;
                        productSeries.CreateDate ??= DateTime.Now;
                        if (TableAction == EnumTableAction.Add)
                            AppSettings.DataAccess.ProductSeriesCrud.SaveEntity(productSeries);
                        else
                            AppSettings.DataAccess.ProductSeriesCrud.UpdateEntity(productSeries);
                        break;
                    case EnumTable.Scales:
                        ScalesEntity scalesEntity = (ScalesEntity)Item;
                        scalesEntity.CreateDate = DateTime.Now;
                        scalesEntity.ModifiedDate = DateTime.Now;
                        success = FieldControlDeny(scalesEntity.Printer, "Принтер");
                        if (success)
                            success = FieldControlDeny(scalesEntity.Host, "Хост");
                        if (success)
                            success = FieldControlDeny(scalesEntity.TemplateDefault, "Шаблон по-умолчанию");
                        if (success)
                            success = FieldControlDeny(scalesEntity.WorkShop, "Цех");
                        if (success)
                        {
                            if (TableAction == EnumTableAction.Add)
                            {
                                if (scalesEntity.TemplateSeries != null && scalesEntity.TemplateSeries.EqualsDefault())
                                    scalesEntity.TemplateSeries = null;
                                AppSettings.DataAccess.ScalesCrud.SaveEntity(scalesEntity);
                            }
                            else
                            {
                                AppSettings.DataAccess.ScalesCrud.UpdateEntity(scalesEntity);
                            }
                        }
                        break;
                    case EnumTable.TemplateResources:
                        TemplateResourcesEntity templateResourcesEntity = (TemplateResourcesEntity)Item;
                        templateResourcesEntity.CreateDate ??= DateTime.Now;
                        templateResourcesEntity.ModifiedDate = DateTime.Now;
                        if (TableAction == EnumTableAction.Add)
                            AppSettings.DataAccess.TemplateResourcesCrud.SaveEntity(templateResourcesEntity);
                        else
                            AppSettings.DataAccess.TemplateResourcesCrud.UpdateEntity(templateResourcesEntity);
                        break;
                    case EnumTable.Templates:
                        TemplatesEntity templateEntity = (TemplatesEntity)Item;
                        if (string.IsNullOrEmpty(templateEntity.CategoryId))
                        {
                            success = false;
                            NotificationMessage msg = new()
                            {
                                Severity = NotificationSeverity.Warning,
                                Summary = "Контроль данных",
                                Detail = "Необходимо заполнить поле [Категория]!",
                                Duration = BlazorCore.Models.AppSettingsEntity.Delay
                            };
                            Notification.Notify(msg);
                        }
                        if (success)
                        {
                            templateEntity.CreateDate ??= DateTime.Now;
                            templateEntity.ModifiedDate = DateTime.Now;
                            if (TableAction is EnumTableAction.Add or EnumTableAction.Copy)
                            {
                                AppSettings.DataAccess.TemplatesCrud.SaveEntity(templateEntity);
                            }
                            else
                            {
                                AppSettings.DataAccess.TemplatesCrud.UpdateEntity(templateEntity);
                            }
                        }
                        break;
                    case EnumTable.WeithingFact:
                        break;
                    case EnumTable.WorkShop:
                        WorkshopEntity workshopEntity = (WorkshopEntity)Item;
                        workshopEntity.CreateDate ??= DateTime.Now;
                        workshopEntity.ModifiedDate = DateTime.Now;
                        if (TableAction == EnumTableAction.Add)
                            AppSettings.DataAccess.WorkshopCrud.SaveEntity(workshopEntity);
                        else
                            AppSettings.DataAccess.WorkshopCrud.UpdateEntity(workshopEntity);
                        break;
                    case EnumTable.Printer:
                        ZebraPrinterEntity zebraPrinter = (ZebraPrinterEntity)Item;
                        zebraPrinter.CreateDate = DateTime.Now;
                        zebraPrinter.ModifiedDate = DateTime.Now;
                        success = FieldControlDeny(zebraPrinter.PrinterType, "Тип принтера");
                        if (success)
                        {
                            if (TableAction == EnumTableAction.Add)
                                AppSettings.DataAccess.ZebraPrinterCrud.SaveEntity(zebraPrinter);
                            else
                                AppSettings.DataAccess.ZebraPrinterCrud.UpdateEntity(zebraPrinter);
                        }
                        break;
                    case EnumTable.PrinterResourceRef:
                        ZebraPrinterResourceRefEntity zebraPrinterResourceRefEntity = (ZebraPrinterResourceRefEntity)Item;
                        zebraPrinterResourceRefEntity.CreateDate = DateTime.Now;
                        zebraPrinterResourceRefEntity.ModifiedDate = DateTime.Now;
                        if (TableAction == EnumTableAction.Add)
                        {
                            AppSettings.DataAccess.ZebraPrinterResourceRefCrud.SaveEntity(zebraPrinterResourceRefEntity);
                        }
                        else
                        {
                            bool existsEntity = AppSettings.DataAccess.ZebraPrinterResourceRefCrud.ExistsEntity(
                                new FieldListEntity(new Dictionary<string, object>
                                {{EnumField.Id.ToString(), zebraPrinterResourceRefEntity.Id}}),
                                new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc));
                            if (existsEntity)
                            {
                                int idLast = AppSettings.DataAccess.ZebraPrinterResourceRefCrud.GetEntity(
                                    new FieldListEntity(new Dictionary<string, object>
                                    { { "Printer.Id", zebraPrinterResourceRefEntity.Printer.Id }}),
                                    new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
                                //zebraPrinterResourceRefEntity.Id = idLast + 1;
                                AppSettings.DataAccess.ZebraPrinterResourceRefCrud.UpdateEntity(zebraPrinterResourceRefEntity);
                            }
                            else
                            {
                                AppSettings.DataAccess.ZebraPrinterResourceRefCrud.UpdateEntity(zebraPrinterResourceRefEntity);
                            }
                        }
                        break;
                    case EnumTable.PrinterType:
                        ZebraPrinterTypeEntity printerTypeEntity = (ZebraPrinterTypeEntity)Item;
                        if (TableAction == EnumTableAction.Add)
                        {
                            int idLast = AppSettings.DataAccess.ZebraPrinterTypeCrud.GetEntity(null,
                                new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
                            printerTypeEntity.Id = idLast + 1;
                            AppSettings.DataAccess.ZebraPrinterTypeCrud.SaveEntity(printerTypeEntity);
                        }
                        else
                            AppSettings.DataAccess.ZebraPrinterTypeCrud.UpdateEntity(printerTypeEntity);
                        break;
                }
            }
            catch (Exception ex)
            {
                NotificationMessage msg = new()
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Ошибка метода [{memberName}]!",
                    Detail = ex.Message,
                    Duration = BlazorCore.Models.AppSettingsEntity.Delay
                };
                Notification.Notify(msg);
                Console.WriteLine(ex.Message);
                Console.WriteLine($"{nameof(filePath)}: {filePath}. {nameof(lineNumber)}: {lineNumber}. {nameof(memberName)}: {memberName}.");
                AppSettings.DataAccess.LogExceptionToSql(ex, filePath, lineNumber, memberName);
            }
            finally
            {
                if (success)
                    Dialog.Close(true);
            }
        }

        private async Task CancelAsync(MouseEventArgs args)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            Dialog.Close(false);
        }

        #endregion
    }
}