// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore.DAL.Models;
using DataProjectsCore.DAL.TableScaleModels;
using DataProjectsCore.DAL.TableSystemModels;
using DataShareCore;
using DataShareCore.DAL.Models;
using Radzen;
using System;

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
            switch (item)
            {
                case BarcodeTypeEntity barCodeTypesEntity:
                    if (barCodeTypesEntity.EqualsDefault())
                        result = false;
                    break;
                case ContragentEntity contragentsEntity:
                    if (contragentsEntity.EqualsDefault())
                        result = false;
                    break;
                case HostEntity hostsEntity:
                    if (hostsEntity.EqualsDefault())
                        result = false;
                    break;
                case LabelEntity labelsEntity:
                    if (labelsEntity.EqualsDefault())
                        result = false;
                    break;
                case NomenclatureEntity nomenclatureEntity:
                    if (nomenclatureEntity.EqualsDefault())
                        result = false;
                    break;
                case OrderEntity ordersEntity:
                    if (ordersEntity.EqualsDefault())
                        result = false;
                    break;
                case OrderStatusEntity orderStatusEntity:
                    if (orderStatusEntity.EqualsDefault())
                        result = false;
                    break;
                case OrderTypeEntity orderTypesEntity:
                    if (orderTypesEntity.EqualsDefault())
                        result = false;
                    break;
                case PluEntity pluEntity:
                    if (pluEntity.EqualsDefault())
                        result = false;
                    break;
                case ProductionFacilityEntity productionFacilityEntity:
                    if (productionFacilityEntity.EqualsDefault())
                        result = false;
                    break;
                case ProductSeriesEntity productSeriesEntity:
                    if (productSeriesEntity.EqualsDefault())
                        result = false;
                    break;
                case ScaleEntity scalesEntity:
                    if (scalesEntity.EqualsDefault())
                        result = false;
                    break;
                case TaskTypeEntity taskTypeEntity:
                    if (taskTypeEntity.EqualsDefault())
                        result = false;
                    break;
                case TemplateResourceEntity templateResourcesEntity:
                    if (templateResourcesEntity.EqualsDefault())
                        result = false;
                    break;
                case TemplateEntity templatesEntity:
                    if (templatesEntity.EqualsDefault())
                        result = false;
                    break;
                case WeithingFactEntity weithingFactEntity:
                    if (weithingFactEntity.EqualsDefault())
                        result = false;
                    break;
                case WorkshopEntity workshopEntity:
                    if (workshopEntity.EqualsDefault())
                        result = false;
                    break;
                case PrinterEntity zebraPrinterEntity:
                    if (zebraPrinterEntity.EqualsDefault())
                        result = false;
                    break;
                case PrinterResourceEntity zebraPrinterResourceRefEntity:
                    if (zebraPrinterResourceRefEntity.EqualsDefault())
                        result = false;
                    break;
                case PrinterTypeEntity zebraPrinterTypeEntity:
                    if (zebraPrinterTypeEntity.EqualsDefault())
                        result = false;
                    break;
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
            bool success = FieldControlDeny(notificationService, host, "Хост");
            if (success)
            {
                host.ModifiedDate = DateTime.Now;
                if (host.Id == 0)
                {
                    host.CreateDate = DateTime.Now;
                    AppSettings.DataAccess.Crud.SaveEntity(host);
                }
                else
                    AppSettings.DataAccess.Crud.UpdateEntity(host);
            }
        }

        public void PrinterResource(NotificationService notificationService, PrinterResourceEntity printerResource)
        {
            bool success = FieldControlDeny(notificationService, printerResource, "Ресурс принтера");
            if (success)
            {
                printerResource.ModifiedDate = DateTime.Now;
                if (printerResource.Id == 0)
                {
                    printerResource.CreateDate = DateTime.Now;
                    AppSettings.DataAccess.Crud.SaveEntity(printerResource);
                }
                else
                    AppSettings.DataAccess.Crud.UpdateEntity(printerResource);
            }
        }

        public void Template(NotificationService notificationService, TemplateEntity template)
        {
            bool success = FieldControlDeny(notificationService, template, "Шаблон");
            if (success)
            {
                if (template.Id == 0)
                {
                    template.ModifiedDate = DateTime.Now;
                    AppSettings.DataAccess.Crud.SaveEntity(template);
                }
                else
                    AppSettings.DataAccess.Crud.UpdateEntity(template);
            }
        }

        public void Workshop(NotificationService notificationService, WorkshopEntity workshop)
        {
            workshop.ModifiedDate = DateTime.Now;
            bool success = FieldControlDeny(notificationService, workshop, "Цех");
            if (success)
            {
                if (workshop.Id == 0)
                {
                    workshop.CreateDate = DateTime.Now;
                    AppSettings.DataAccess.Crud.SaveEntity(workshop);
                }
                else
                    AppSettings.DataAccess.Crud.UpdateEntity(workshop);
            }
        }

        public void Printer(NotificationService notificationService, PrinterEntity printer)
        {
            printer.ModifiedDate = DateTime.Now;
            bool success = FieldControlDeny(notificationService, printer, "Принтер");
            if (success)
                success = FieldControlDeny(notificationService, printer.PrinterType, "Тип принтера");
            if (success)
            {
                if (printer.Id == 0)
                {
                    printer.CreateDate = DateTime.Now;
                    AppSettings.DataAccess.Crud.SaveEntity(printer);
                }
                else
                    AppSettings.DataAccess.Crud.UpdateEntity(printer);
            }
        }

        public void PrinterType(NotificationService notificationService, PrinterTypeEntity printerType)
        {
            bool success = FieldControlDeny(notificationService, printerType, "Тип принтера");
            if (success)
            {
                int idLast = AppSettings.DataAccess.Crud.GetEntity<PrinterTypeEntity>(null,
                    new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
                printerType.Id = idLast + 1;
                if (printerType.Id == 0)
                    AppSettings.DataAccess.Crud.SaveEntity(printerType);
                else
                    AppSettings.DataAccess.Crud.UpdateEntity(printerType);
            }
        }

        public void Scale(NotificationService notificationService, ScaleEntity scale)
        {
            scale.ModifiedDate = DateTime.Now;
            bool success = FieldControlDeny(notificationService, scale, "Устройство");
            if (success)
                success = FieldControlDeny(notificationService, scale.Printer, "Принтер");
            if (success)
                success = FieldControlDeny(notificationService, scale.Host, "Хост");
            if (success)
                success = FieldControlDeny(notificationService, scale.TemplateDefault, "Шаблон по-умолчанию");
            if (success)
                success = FieldControlDeny(notificationService, scale.WorkShop, "Цех");
            if (success)
            {
                if (scale.Id == 0)
                {
                    scale.CreateDate = DateTime.Now;
                    if (scale.TemplateSeries != null && scale.TemplateSeries.EqualsDefault())
                        scale.TemplateSeries = null;
                    AppSettings.DataAccess.Crud.SaveEntity(scale);
                }
                else
                    AppSettings.DataAccess.Crud.UpdateEntity(scale);
            }
        }

        public void Task(NotificationService notificationService, TaskEntity task)
        {
            bool success = FieldControlDeny(notificationService, task, "Модуль задачи");
            if (success)
                success = FieldControlDeny(notificationService, task.TaskType, "Тип задачи");
            if (success)
                success = FieldControlDeny(notificationService, task.Scale, "Устройство");
            if (success)
            {
                if (task.Uid == Guid.Empty)
                    AppSettings.DataAccess.Crud.SaveEntity(task);
                else
                    AppSettings.DataAccess.Crud.UpdateEntity(task);
            }
        }

        public void TaskType(NotificationService notificationService, TaskTypeEntity taskType)
        {
            bool success = FieldControlDeny(notificationService, taskType, "Тип модуля задачи");
            if (success)
            {
                if (taskType.Uid == Guid.Empty)
                    AppSettings.DataAccess.Crud.SaveEntity(taskType);
                else
                    AppSettings.DataAccess.Crud.UpdateEntity(taskType);
            }
        }

        #endregion
    }
}
