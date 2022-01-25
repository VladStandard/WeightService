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
using System.Linq;
using static DataShareCore.ShareEnums;

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
            string detailAddition = Environment.NewLine;
            switch (item)
            {
                case BarcodeTypeEntity barCodeType:
                    if (barCodeType.EqualsDefault())
                        result = false;
                    if (string.IsNullOrEmpty(barCodeType.Name))
                    {
                        detailAddition += $"{LocalizationCore.Strings.FieldIsEmpty}: {LocalizationCore.Strings.FieldName}" + Environment.NewLine;
                        result = false;
                    }
                    break;
                case ContragentEntity contragent:
                    if (contragent.EqualsDefault())
                        result = false;
                    break;
                case HostEntity host:
                    if (host.EqualsDefault())
                        result = false;
                    if (string.IsNullOrEmpty(host.Name))
                    {
                        detailAddition += $"{LocalizationCore.Strings.FieldIsEmpty}: {LocalizationCore.Strings.FieldName}" + Environment.NewLine;
                        result = false;
                    }
                    if (Equals(host.IdRRef, Guid.Empty))
                    {
                        detailAddition += $"{LocalizationCore.Strings.FieldIsEmpty}: {LocalizationCore.Strings.FieldIdRRef}" + Environment.NewLine;
                        result = false;
                    }
                    if (string.IsNullOrEmpty(host.Ip))
                    {
                        detailAddition += $"{LocalizationCore.Strings.FieldIsEmpty}: {LocalizationCore.Strings.FieldIpAddress}" + Environment.NewLine;
                        result = false;
                    }
                    break;
                case LabelEntity label:
                    if (label.EqualsDefault())
                        result = false;
                    break;
                case NomenclatureEntity nomenclature:
                    if (nomenclature.EqualsDefault())
                        result = false;
                    break;
                case OrderEntity order:
                    if (order.EqualsDefault())
                        result = false;
                    break;
                case OrderStatusEntity orderStatus:
                    if (orderStatus.EqualsDefault())
                        result = false;
                    break;
                case OrderTypeEntity orderType:
                    if (orderType.EqualsDefault())
                        result = false;
                    break;
                case PluEntity plu:
                    if (plu.EqualsDefault())
                        result = false;
                    PluEntity[] pluEntities = AppSettings.DataAccess.Crud.GetEntities<PluEntity>(
                        new FieldListEntity(new Dictionary<string, object> {
                            { "Scale.Id", plu.Scale.Id },
                            { ShareEnums.DbField.Plu.ToString(), plu.Plu }
                        }), null);
                    if (pluEntities.Any() && !pluEntities.Where(x => x.Id.Equals(item.Id)).Select(x => x).Any())
                    {
                        detailAddition += $"{LocalizationCore.Strings.TablePluHavingPlu}: {plu.Plu}" + Environment.NewLine;
                        result = false;
                    }
                    break;
                case PrinterEntity printer:
                    if (printer.EqualsDefault())
                        result = false;
                    break;
                case PrinterResourceEntity printerResource:
                    if (printerResource.EqualsDefault())
                        result = false;
                    break;
                case PrinterTypeEntity printerType:
                    if (printerType.EqualsDefault())
                        result = false;
                    if (string.IsNullOrEmpty(printerType.Name))
                    {
                        detailAddition += $"{LocalizationCore.Strings.FieldIsEmpty}: {LocalizationCore.Strings.FieldName}" + Environment.NewLine;
                        result = false;
                    }
                    break;
                case ProductionFacilityEntity productionFacility:
                    if (productionFacility.EqualsDefault())
                        result = false;
                    break;
                case ProductSeriesEntity productSeries:
                    if (productSeries.EqualsDefault())
                        result = false;
                    break;
                case ScaleEntity scale:
                    if (scale.EqualsDefault())
                        result = false;
                    break;
                case TaskTypeEntity taskType:
                    if (taskType.EqualsDefault())
                        result = false;
                    break;
                case TemplateResourceEntity templateResource:
                    if (templateResource.EqualsDefault())
                        result = false;
                    break;
                case TemplateEntity template:
                    if (template.EqualsDefault())
                        result = false;
                    if (string.IsNullOrEmpty(template.CategoryId))
                    {
                        detailAddition += $"{LocalizationCore.Strings.FieldIsEmpty}: {LocalizationCore.Strings.FieldCategory}" + Environment.NewLine;
                        result = false;
                    }
                    break;
                case WeithingFactEntity weithingFact:
                    if (weithingFact.EqualsDefault())
                        result = false;
                    break;
                case WorkshopEntity workshop:
                    if (workshop.EqualsDefault())
                        result = false;
                    break;
            }
            if (!result)
            {
                NotificationMessage msg = new()
                {
                    Severity = NotificationSeverity.Warning,
                    Summary = LocalizationCore.Strings.DataControl,
                    Detail = $"{LocalizationCore.Strings.DataControlField} [{field}]!" + 
                        (Equals(detailAddition, Environment.NewLine) ? string.Empty : detailAddition),
                    Duration = AppSettingsHelper.Delay
                };
                notificationService.Notify(msg);
                return false;
            }
            return true;
        }

        public void BarcodeType(NotificationService notificationService, BarcodeTypeEntity barcodeType, int? id, DbTableAction tableAction)
        {
            bool success = FieldControlDeny(notificationService, barcodeType, "Тип штрихкода");
            if (success)
            {
                if (tableAction == DbTableAction.New)
                {
                    AppSettings.DataAccess.Crud.SaveEntity(barcodeType);
                }
                else
                {
                    if (int.TryParse(id?.ToString(), out int getId))
                        barcodeType.Id = getId;
                    AppSettings.DataAccess.Crud.UpdateEntity(barcodeType);
                }
            }
        }

        public void Host(NotificationService notificationService, HostEntity host, int id, DbTableAction tableAction)
        {
            bool success = FieldControlDeny(notificationService, host, "Хост");
            if (success)
            {
                host.ModifiedDate = DateTime.Now;
                if (tableAction == DbTableAction.New)
                {
                    host.CreateDate = DateTime.Now;
                    AppSettings.DataAccess.Crud.SaveEntity(host);
                }
                else
                {
                    host.Id = id;
                    AppSettings.DataAccess.Crud.UpdateEntity(host);
                }
            }
        }

        public void Plu(NotificationService notificationService, PluEntity plu, int id, DbTableAction tableAction)
        {
            plu.ModifiedDate = DateTime.Now;
            bool success = FieldControlDeny(notificationService, plu, "ПЛУ");
            if (success)
                success = FieldControlDeny(notificationService, plu.Scale, "Устройство");
            if (success)
                success = FieldControlDeny(notificationService, plu.Templates, "Шаблон этикетки");
            if (success)
                success = FieldControlDeny(notificationService, plu.Nomenclature, "Продукт");
            if (success)
            {
                if (tableAction == DbTableAction.New)
                {
                    plu.CreateDate = DateTime.Now;
                    AppSettings.DataAccess.Crud.SaveEntity(plu);
                }
                else
                {
                    plu.Id = id;
                    AppSettings.DataAccess.Crud.UpdateEntity(plu);
                }
            }
        }

        public void Printer(NotificationService notificationService, PrinterEntity printer, int id, DbTableAction tableAction)
        {
            printer.ModifiedDate = DateTime.Now;
            bool success = FieldControlDeny(notificationService, printer, "Принтер");
            if (success)
                success = FieldControlDeny(notificationService, printer.PrinterType, "Тип принтера");
            if (success)
            {
                if (tableAction == DbTableAction.New)
                {
                    printer.CreateDate = DateTime.Now;
                    AppSettings.DataAccess.Crud.SaveEntity(printer);
                }
                else
                {
                    printer.Id = id;
                    AppSettings.DataAccess.Crud.UpdateEntity(printer);
                }
            }
        }

        public void PrinterResource(NotificationService notificationService, PrinterResourceEntity printerResource, int id, DbTableAction tableAction)
        {
            bool success = FieldControlDeny(notificationService, printerResource, "Ресурс принтера");
            if (success)
            {
                printerResource.ModifiedDate = DateTime.Now;
                if (tableAction == DbTableAction.New)
                {
                    printerResource.CreateDate = DateTime.Now;
                    AppSettings.DataAccess.Crud.SaveEntity(printerResource);
                }
                else
                {
                    printerResource.Id = id;
                    AppSettings.DataAccess.Crud.UpdateEntity(printerResource);
                }
            }
        }

        public void PrinterType(NotificationService notificationService, PrinterTypeEntity printerType, int id, DbTableAction tableAction)
        {
            bool success = FieldControlDeny(notificationService, printerType, "Тип принтера");
            if (success)
            {
                if (tableAction == DbTableAction.New)
                {
                    AppSettings.DataAccess.Crud.SaveEntity(printerType);
                }
                else
                {
                    printerType.Id = id;
                    AppSettings.DataAccess.Crud.UpdateEntity(printerType);
                }
            }
        }

        public void Scale(NotificationService notificationService, ScaleEntity scale, int id, DbTableAction tableAction)
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
                if (tableAction == DbTableAction.New)
                {
                    scale.CreateDate = DateTime.Now;
                    if (scale.TemplateSeries != null && scale.TemplateSeries.EqualsDefault())
                        scale.TemplateSeries = null;
                    AppSettings.DataAccess.Crud.SaveEntity(scale);
                }
                else
                {
                    scale.Id = id;
                    AppSettings.DataAccess.Crud.UpdateEntity(scale);
                }
            }
        }

        public void Task(NotificationService notificationService, TaskEntity task, Guid uid, DbTableAction tableAction)
        {
            bool success = FieldControlDeny(notificationService, task, "Модуль задачи");
            if (success)
                success = FieldControlDeny(notificationService, task.TaskType, "Тип задачи");
            if (success)
                success = FieldControlDeny(notificationService, task.Scale, "Устройство");
            if (success)
            {
                if (tableAction == DbTableAction.New)
                {
                    AppSettings.DataAccess.Crud.SaveEntity(task);
                }
                else
                {
                    task.Uid = uid;
                    AppSettings.DataAccess.Crud.UpdateEntity(task);
                }
            }
        }

        public void TaskType(NotificationService notificationService, TaskTypeEntity taskType, Guid uid, DbTableAction tableAction)
        {
            bool success = FieldControlDeny(notificationService, taskType, "Тип модуля задачи");
            if (success)
            {
                if (tableAction == DbTableAction.New)
                {
                    AppSettings.DataAccess.Crud.SaveEntity(taskType);
                }
                else
                {
                    taskType.Uid = uid;
                    AppSettings.DataAccess.Crud.UpdateEntity(taskType);
                }
            }
        }

        public void Template(NotificationService notificationService, TemplateEntity template, int id, DbTableAction tableAction)
        {
            bool success = FieldControlDeny(notificationService, template, "Шаблон");
            if (success)
            {
                if (tableAction == DbTableAction.New)
                {
                    template.ModifiedDate = DateTime.Now;
                    AppSettings.DataAccess.Crud.SaveEntity(template);
                }
                else
                {
                    template.Id = id;
                    AppSettings.DataAccess.Crud.UpdateEntity(template);
                }
            }
        }

        public void Workshop(NotificationService notificationService, WorkshopEntity workshop, int id, DbTableAction tableAction)
        {
            workshop.ModifiedDate = DateTime.Now;
            bool success = FieldControlDeny(notificationService, workshop, "Цех");
            if (success)
            {
                if (tableAction == DbTableAction.New)
                {
                    workshop.CreateDate = DateTime.Now;
                    AppSettings.DataAccess.Crud.SaveEntity(workshop);
                }
                else
                {
                    workshop.Id = id;
                    AppSettings.DataAccess.Crud.UpdateEntity(workshop);
                }
            }
        }

        #endregion
    }
}
