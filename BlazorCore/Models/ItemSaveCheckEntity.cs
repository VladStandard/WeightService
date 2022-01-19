// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore.DAL.Models;
using DataProjectsCore.DAL.TableScaleModels;
using DataProjectsCore.DAL.TableSystemModels;
using DataShareCore;
using DataShareCore.DAL.Models;
using Radzen;
using System;
using System.Collections.Generic;

namespace BlazorCore.Models
{
    public class ItemSaveCheckEntity
    {
        #region Public and private fields and properties

        public AppSettingsHelper AppSettings { get; private set; } = AppSettingsHelper.Instance;

        #endregion

        #region Public and private methods

        public bool FieldControlDeny(NotificationService notificationService, BaseEntity item, string field)
        {
            bool result = item != null;
            if (item is BarcodeTypeEntity barCodeTypesEntity)
            {
                if (barCodeTypesEntity.EqualsDefault())
                    result = false;
            }
            else if (item is ContragentEntity contragentsEntity)
            {
                if (contragentsEntity.EqualsDefault())
                    result = false;
            }
            else if (item is HostEntity hostsEntity)
            {
                if (hostsEntity.EqualsDefault())
                    result = false;
            }
            else if (item is LabelEntity labelsEntity)
            {
                if (labelsEntity.EqualsDefault())
                    result = false;
            }
            else if (item is NomenclatureEntity nomenclatureEntity)
            {
                if (nomenclatureEntity.EqualsDefault())
                    result = false;
            }
            else if (item is OrderEntity ordersEntity)
            {
                if (ordersEntity.EqualsDefault())
                    result = false;
            }
            else if (item is OrderStatusEntity orderStatusEntity)
            {
                if (orderStatusEntity.EqualsDefault())
                    result = false;
            }
            else if (item is OrderTypeEntity orderTypesEntity)
            {
                if (orderTypesEntity.EqualsDefault())
                    result = false;
            }
            else if (item is PluEntity pluEntity)
            {
                if (pluEntity.EqualsDefault())
                    result = false;
            }
            else if (item is ProductionFacilityEntity productionFacilityEntity)
            {
                if (productionFacilityEntity.EqualsDefault())
                    result = false;
            }
            else if (item is ProductSeriesEntity productSeriesEntity)
            {
                if (productSeriesEntity.EqualsDefault())
                    result = false;
            }
            else if (item is ScaleEntity scalesEntity)
            {
                if (scalesEntity.EqualsDefault())
                    result = false;
            }
            else if (item is TaskTypeEntity taskTypeEntity)
            {
                if (taskTypeEntity.EqualsDefault())
                    result = false;
            }
            else if (item is TemplateResourceEntity templateResourcesEntity)
            {
                if (templateResourcesEntity.EqualsDefault())
                    result = false;
            }
            else if (item is TemplateEntity templatesEntity)
            {
                if (templatesEntity.EqualsDefault())
                    result = false;
            }
            else if (item is WeithingFactEntity weithingFactEntity)
            {
                if (weithingFactEntity.EqualsDefault())
                    result = false;
            }
            else if (item is WorkshopEntity workshopEntity)
            {
                if (workshopEntity.EqualsDefault())
                    result = false;
            }
            else if (item is PrinterEntity zebraPrinterEntity)
            {
                if (zebraPrinterEntity.EqualsDefault())
                    result = false;
            }
            else if (item is PrinterResourceEntity zebraPrinterResourceRefEntity)
            {
                if (zebraPrinterResourceRefEntity.EqualsDefault())
                    result = false;
            }
            else if (item is PrinterTypeEntity zebraPrinterTypeEntity)
            {
                if (zebraPrinterTypeEntity.EqualsDefault())
                    result = false;
            }
            if (!result)
            {
                NotificationMessage msg = new()
                {
                    Severity = NotificationSeverity.Warning,
                    Summary = LocalizationCore.Strings.DataControl,
                    Detail = $"{LocalizationCore.Strings.DataControlField} [{field}]!",
                    Duration = AppSettingsHelper.Delay
                };
                notificationService.Notify(msg);
                return false;
            }
            return true;
        }
        
        public void Host(NotificationService notificationService, HostEntity host)
        {
            if (host.Id == 0)
            {
                AppSettings.DataAccess.Crud.SaveEntity(host);
                host.CreateDate = DateTime.Now;
                host.ModifiedDate = DateTime.Now;
            }
            else
            {
                host.ModifiedDate = DateTime.Now;
                bool _ = AppSettings.DataAccess.Crud.ExistsEntity<HostEntity>(
                    new FieldListEntity(new Dictionary<string, object>
                        {{ShareEnums.DbField.Id.ToString(), host.Id}}),
                    new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc));
                AppSettings.DataAccess.Crud.UpdateEntity(host);
            }
        }

        public void PrinterResource(NotificationService notificationService, PrinterResourceEntity printerResource)
        {
            printerResource.CreateDate = DateTime.Now;
            printerResource.ModifiedDate = DateTime.Now;
            if (printerResource.Id == 0)
            {
                AppSettings.DataAccess.PrinterResourcesCrud.SaveEntity(printerResource);
            }
            else
            {
                bool _ = AppSettings.DataAccess.PrinterResourcesCrud.ExistsEntity<PrinterResourceEntity>(
                    new FieldListEntity(new Dictionary<string, object>
                        {{ShareEnums.DbField.Id.ToString(), printerResource.Id}}),
                    new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc));
                //if (existsEntity)
                //{
                //    int idLast = AppSettings.DataAccess.PrinterResourcesCrud.GetEntity(
                //        new FieldListEntity(new Dictionary<string, object>
                //            { { "Printer.Id", printerResourceItem.Printer.Id }}),
                //        new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
                //    printerResourceItem.Id = idLast + 1;
                //}
                AppSettings.DataAccess.PrinterResourcesCrud.UpdateEntity(printerResource);
            }
        }

        public void Template(NotificationService notificationService, TemplateEntity template)
        {
            if (template.Id == 0)
                AppSettings.DataAccess.TemplatesCrud.SaveEntity(template);
            else
                AppSettings.DataAccess.TemplatesCrud.UpdateEntity(template);
        }

        public void Workshop(NotificationService notificationService, WorkshopEntity workshop)
        {
            workshop.CreateDate ??= DateTime.Now;
            workshop.ModifiedDate = DateTime.Now;
            if (workshop.Id == 0)
                AppSettings.DataAccess.WorkshopsCrud.SaveEntity(workshop);
            else
                AppSettings.DataAccess.WorkshopsCrud.UpdateEntity(workshop);
        }

        public void Printer(NotificationService notificationService, PrinterEntity item)
        {
            item.CreateDate = DateTime.Now;
            item.ModifiedDate = DateTime.Now;
            bool success = FieldControlDeny(notificationService, item.PrinterType, "Тип принтера");
            if (success)
            {
                if (item.Id == 0)
                    AppSettings.DataAccess.PrintersCrud.SaveEntity(item);
                else
                    AppSettings.DataAccess.PrintersCrud.UpdateEntity(item);
            }
        }

        public void PrinterType(NotificationService notificationService, PrinterTypeEntity item)
        {
            int idLast = AppSettings.DataAccess.Crud.GetEntity<PrinterTypeEntity>(null,
                new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            item.Id = idLast + 1;
            if (item.Id == 0)
                AppSettings.DataAccess.Crud.SaveEntity(item);
            else
                AppSettings.DataAccess.Crud.UpdateEntity(item);
        }

        public void Scale(NotificationService notificationService, ScaleEntity item)
        {
            item.CreateDate = DateTime.Now;
            item.ModifiedDate = DateTime.Now;
            bool success = FieldControlDeny(notificationService, item.Printer, "Принтер");
            if (success)
                success = FieldControlDeny(notificationService, item.Host, "Хост");
            if (success)
                success = FieldControlDeny(notificationService, item.TemplateDefault, "Шаблон по-умолчанию");
            if (success)
                success = FieldControlDeny(notificationService, item.WorkShop, "Цех");
            if (success)
            {
                if (item.Id == 0)
                {
                    if (item.TemplateSeries != null && item.TemplateSeries.EqualsDefault())
                        item.TemplateSeries = null;
                    AppSettings.DataAccess.ScalesCrud.SaveEntity(item);
                }
                else
                    AppSettings.DataAccess.ScalesCrud.UpdateEntity(item);
            }
        }

        public void Task(NotificationService notificationService, TaskEntity item)
        {
            bool success = FieldControlDeny(notificationService, item.TaskType, "Тип задачи");
            if (success)
                success = FieldControlDeny(notificationService, item.Scale, "Устройство");
            if (success)
            {
                if (item.Uid == Guid.Empty)
                    AppSettings.DataAccess.Crud.SaveEntity(item);
                else
                    AppSettings.DataAccess.Crud.UpdateEntity(item);
            }
        }

        public void TaskType(NotificationService notificationService, TaskTypeEntity item)
        {
            if (item.Uid == Guid.Empty)
                AppSettings.DataAccess.Crud.SaveEntity(item);
            else
                AppSettings.DataAccess.Crud.UpdateEntity(item);
        }

        #endregion
    }
}
