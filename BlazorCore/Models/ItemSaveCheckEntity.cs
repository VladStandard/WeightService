// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore.DAL.TableScaleModels;
using DataProjectsCore.DAL.TableSystemModels;
using DataShareCore;
using DataShareCore.DAL.Models;
using Radzen;
using System;
using static DataShareCore.ShareEnums;

namespace BlazorCore.Models
{
    public class ItemSaveCheckEntity
    {
        #region Public and private fields and properties

        public AppSettingsHelper AppSettings { get; private set; } = AppSettingsHelper.Instance;
        private ItemFieldControlEntity FieldControl { get; set; } = new ItemFieldControlEntity();

        #endregion

        #region Public and private methods

        public void BarcodeType(NotificationService notificationService, BarcodeTypeEntity barcodeType, int? id, DbTableAction tableAction)
        {
            bool success = FieldControl.ProcessChecks(notificationService, barcodeType, "Тип штрихкода");
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
            bool success = FieldControl.ProcessChecks(notificationService, host, "Хост");
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
            bool success = FieldControl.ProcessChecks(notificationService, plu, "ПЛУ");
            if (success)
                success = FieldControl.ProcessChecks(notificationService, plu.Scale, "Устройство");
            if (success)
                success = FieldControl.ProcessChecks(notificationService, plu.Templates, "Шаблон этикетки");
            if (success)
                success = FieldControl.ProcessChecks(notificationService, plu.Nomenclature, "Продукт");
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
            bool success = FieldControl.ProcessChecks(notificationService, printer, "Принтер");
            if (success)
                success = FieldControl.ProcessChecks(notificationService, printer.PrinterType, "Тип принтера");
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
            bool success = FieldControl.ProcessChecks(notificationService, printerResource, "Ресурс принтера");
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
            bool success = FieldControl.ProcessChecks(notificationService, printerType, "Тип принтера");
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
            bool success = FieldControl.ProcessChecks(notificationService, scale, "Устройство");
            if (success)
                success = FieldControl.ProcessChecks(notificationService, scale.Printer, "Принтер");
            if (success)
                success = FieldControl.ProcessChecks(notificationService, scale.Host, "Хост");
            if (success)
                success = FieldControl.ProcessChecks(notificationService, scale.TemplateDefault, "Шаблон по-умолчанию");
            if (success)
                success = FieldControl.ProcessChecks(notificationService, scale.WorkShop, "Цех");
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
            bool success = FieldControl.ProcessChecks(notificationService, task, "Модуль задачи");
            if (success)
                success = FieldControl.ProcessChecks(notificationService, task.TaskType, "Тип задачи");
            if (success)
                success = FieldControl.ProcessChecks(notificationService, task.Scale, "Устройство");
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
            bool success = FieldControl.ProcessChecks(notificationService, taskType, "Тип модуля задачи");
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
            bool success = FieldControl.ProcessChecks(notificationService, template, "Шаблон");
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
            bool success = FieldControl.ProcessChecks(notificationService, workshop, "Цех");
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
