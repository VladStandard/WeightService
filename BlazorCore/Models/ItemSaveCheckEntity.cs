// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.DAL.TableScaleModels;
using Radzen;
using System;
using static DataCore.ShareEnums;

namespace BlazorCore.Models
{
    public class ItemSaveCheckEntity
    {
        #region Public and private fields and properties

        public AppSettingsHelper AppSettings { get; private set; } = AppSettingsHelper.Instance;
        private ItemFieldControlEntity FieldControl { get; set; } = new ItemFieldControlEntity();

        #endregion

        #region Public and private methods

        public void BarcodeType(NotificationService notificationService, BarcodeTypeEntity barcodeType, long? id, DbTableAction tableAction)
        {
            if (id == null)
                return;
            bool success = FieldControl.ProcessChecks(notificationService, barcodeType, LocalizationCore.Strings.TableItem.BarcodeType);
            if (success)
            {
                //barcodeType.ModifiedDate = DateTime.Now;
                if (tableAction == DbTableAction.New)
                {
                    //barcodeType.CreateDate = DateTime.Now;
                    AppSettings.DataAccess?.Crud.SaveEntity(barcodeType);
                }
                else
                {
                    barcodeType.Id = (long)id;
                    AppSettings.DataAccess?.Crud.UpdateEntity(barcodeType);
                }
            }
        }

        public void Host(NotificationService notificationService, HostEntity host, long? id, DbTableAction tableAction)
        {
            if (id == null)
                return;
            bool success = FieldControl.ProcessChecks(notificationService, host, LocalizationCore.Strings.TableItem.Host);
            if (success)
            {
                host.ChangeDt = DateTime.Now;
                if (tableAction == DbTableAction.New)
                {
                    host.CreateDt = DateTime.Now;
                    AppSettings.DataAccess?.Crud.SaveEntity(host);
                }
                else
                {
                    host.Id = (long)id;
                    AppSettings.DataAccess?.Crud.UpdateEntity(host);
                }
            }
        }

        public void Nomenclature(NotificationService notificationService, NomenclatureEntity nomenclature, 
            long? id, DbTableAction tableAction)
        {
            if (id == null)
                return;
            bool success = FieldControl.ProcessChecks(notificationService, nomenclature, LocalizationCore.Strings.TableItem.Nomenclature);
            if (success)
            {
                nomenclature.ModifiedDate = DateTime.Now;
                if (tableAction == DbTableAction.New)
                {
                    nomenclature.CreateDate = DateTime.Now;
                    AppSettings.DataAccess?.Crud.SaveEntity(nomenclature);
                }
                else
                {
                    nomenclature.Id = (long)id;
                    AppSettings.DataAccess?.Crud.UpdateEntity(nomenclature);
                }
            }
        }

        public void Plu(NotificationService notificationService, PluEntity plu, long? id, DbTableAction tableAction)
        {
            if (id == null)
                return;
            plu.ModifiedDate = DateTime.Now;
            bool success = FieldControl.ProcessChecks(notificationService, plu, LocalizationCore.Strings.TableItem.Plu);
            if (success)
                success = FieldControl.ProcessChecks(notificationService, plu.Scale, LocalizationCore.Strings.TableItem.Device);
            if (success)
                success = FieldControl.ProcessChecks(notificationService, plu.Template, LocalizationCore.Strings.TableItem.LabelTemplate);
            if (success)
                success = FieldControl.ProcessChecks(notificationService, plu.Nomenclature, LocalizationCore.Strings.TableItem.Product);
            if (success)
            {
                plu.ModifiedDate = DateTime.Now;
                if (tableAction == DbTableAction.New)
                {
                    plu.CreateDate = DateTime.Now;
                    AppSettings.DataAccess?.Crud.SaveEntity(plu);
                }
                else
                {
                    plu.Id = (long)id;
                    AppSettings.DataAccess?.Crud.UpdateEntity(plu);
                }
            }
        }

        public void Printer(NotificationService notificationService, PrinterEntity printer, long? id, DbTableAction tableAction)
        {
            if (id == null)
                return;
            printer.ModifiedDate = DateTime.Now;
            bool success = FieldControl.ProcessChecks(notificationService, printer, LocalizationCore.Strings.TableItem.Printer);
            if (success)
                success = FieldControl.ProcessChecks(notificationService, printer.PrinterType, LocalizationCore.Strings.TableItem.PrinterType);
            if (success)
            {
                printer.ModifiedDate = DateTime.Now;
                if (tableAction == DbTableAction.New)
                {
                    printer.CreateDate = DateTime.Now;
                    AppSettings.DataAccess?.Crud.SaveEntity(printer);
                }
                else
                {
                    printer.Id = (long)id;
                    AppSettings.DataAccess?.Crud.UpdateEntity(printer);
                }
            }
        }

        public void PrinterResource(NotificationService notificationService, PrinterResourceEntity printerResource, 
            long? id, DbTableAction tableAction)
        {
            if (id == null)
                return;
            bool success = FieldControl.ProcessChecks(notificationService, printerResource, LocalizationCore.Strings.TableItem.PrinterResource);
            if (success)
            {
                printerResource.ModifiedDate = DateTime.Now;
                if (tableAction == DbTableAction.New)
                {
                    printerResource.CreateDate = DateTime.Now;
                    AppSettings.DataAccess?.Crud.SaveEntity(printerResource);
                }
                else
                {
                    printerResource.Id = (long)id;
                    AppSettings.DataAccess?.Crud.UpdateEntity(printerResource);
                }
            }
        }

        public void PrinterType(NotificationService notificationService, PrinterTypeEntity printerType, 
            long? id, DbTableAction tableAction)
        {
            if (id == null)
                return;
            bool success = FieldControl.ProcessChecks(notificationService, printerType, LocalizationCore.Strings.TableItem.PrinterType);
            if (success)
            {
                //printerType.ModifiedDate = DateTime.Now;
                if (tableAction == DbTableAction.New)
                {
                    //printerType.CreateDate = DateTime.Now;
                    AppSettings.DataAccess?.Crud.SaveEntity(printerType);
                }
                else
                {
                    printerType.Id = (long)id;
                    AppSettings.DataAccess?.Crud.UpdateEntity(printerType);
                }
            }
        }

        public void ProductionFacility(NotificationService notificationService, ProductionFacilityEntity productionFacility, 
            long? id, DbTableAction tableAction)
        {
            if (id == null)
                return;
            bool success = FieldControl.ProcessChecks(notificationService, productionFacility, LocalizationCore.Strings.TableItem.ProductionFacility);
            if (success)
            {
                productionFacility.ModifiedDate = DateTime.Now;
                if (tableAction == DbTableAction.New)
                {
                    productionFacility.CreateDate = DateTime.Now;
                    AppSettings.DataAccess?.Crud.SaveEntity(productionFacility);
                }
                else
                {
                    productionFacility.Id = (long)id;
                    AppSettings.DataAccess?.Crud.UpdateEntity(productionFacility);
                }
            }
        }

        public void Scale(NotificationService notificationService, ScaleEntity scale, long? id, DbTableAction tableAction)
        {
            if (id == null)
                return;
            scale.ModifiedDate = DateTime.Now;
            bool success = FieldControl.ProcessChecks(notificationService, scale, LocalizationCore.Strings.TableItem.Device);
            if (success)
                success = FieldControl.ProcessChecks(notificationService, scale.Printer, LocalizationCore.Strings.TableItem.Printer);
            if (success)
                success = FieldControl.ProcessChecks(notificationService, scale.Host, LocalizationCore.Strings.TableItem.Host);
            if (success)
                success = FieldControl.ProcessChecks(notificationService, scale.TemplateDefault, LocalizationCore.Strings.TableItem.TemplateDefault);
            if (success)
                success = FieldControl.ProcessChecks(notificationService, scale.WorkShop, LocalizationCore.Strings.TableItem.Workshop);
            if (success)
            {
                scale.ModifiedDate = DateTime.Now;
                if (tableAction == DbTableAction.New)
                {
                    scale.CreateDate = DateTime.Now;
                    if (scale.TemplateSeries != null && scale.TemplateSeries.EqualsDefault())
                        scale.TemplateSeries = null;
                    AppSettings.DataAccess?.Crud.SaveEntity(scale);
                }
                else
                {
                    scale.Id = (long)id;
                    AppSettings.DataAccess?.Crud.UpdateEntity(scale);
                }
            }
        }

        public void Task(NotificationService notificationService, TaskEntity task, Guid uid, DbTableAction tableAction)
        {
            bool success = FieldControl.ProcessChecks(notificationService, task, LocalizationCore.Strings.TableItem.TaskModule);
            if (success)
                success = FieldControl.ProcessChecks(notificationService, task.TaskType, LocalizationCore.Strings.TableItem.TaskType);
            if (success)
                success = FieldControl.ProcessChecks(notificationService, task.Scale, LocalizationCore.Strings.TableItem.Device);
            if (success)
            {
                //task.ModifiedDate = DateTime.Now;
                if (tableAction == DbTableAction.New)
                {
                    //task.CrateDate = DateTime.Now;
                    AppSettings.DataAccess?.Crud.SaveEntity(task);
                }
                else
                {
                    task.Uid = uid;
                    AppSettings.DataAccess?.Crud.UpdateEntity(task);
                }
            }
        }

        public void TaskType(NotificationService notificationService, TaskTypeEntity taskType, Guid uid, DbTableAction tableAction)
        {
            bool success = FieldControl.ProcessChecks(notificationService, taskType, LocalizationCore.Strings.TableItem.TaskModuleType);
            if (success)
            {
                //taskType.ModifiedDate = DateTime.Now;
                if (tableAction == DbTableAction.New)
                {
                    //taskType.CreateDate = DateTime.Now;
                    AppSettings.DataAccess?.Crud.SaveEntity(taskType);
                }
                else
                {
                    taskType.Uid = uid;
                    AppSettings.DataAccess?.Crud.UpdateEntity(taskType);
                }
            }
        }

        public void Template(NotificationService notificationService, TemplateEntity template, long? id, DbTableAction tableAction)
        {
            if (id == null)
                return;
            bool success = FieldControl.ProcessChecks(notificationService, template, LocalizationCore.Strings.TableItem.Template);
            if (success)
            {
                template.ModifiedDate = DateTime.Now;
                if (tableAction == DbTableAction.New)
                {
                    template.CreateDate = DateTime.Now;
                    AppSettings.DataAccess?.Crud.SaveEntity(template);
                }
                else
                {
                    template.Id = (long)id;
                    AppSettings.DataAccess?.Crud.UpdateEntity(template);
                }
            }
        }

        public void Workshop(NotificationService notificationService, WorkshopEntity workshop, long? id, DbTableAction tableAction)
        {
            if (id == null)
                return;
            workshop.ModifiedDate = DateTime.Now;
            bool success = FieldControl.ProcessChecks(notificationService, workshop, LocalizationCore.Strings.TableItem.Workshop);
            if (success)
            {
                workshop.ModifiedDate = DateTime.Now;
                if (tableAction == DbTableAction.New)
                {
                    workshop.CreateDate = DateTime.Now;
                    AppSettings.DataAccess?.Crud.SaveEntity(workshop);
                }
                else
                {
                    workshop.Id = (long)id;
                    AppSettings.DataAccess?.Crud.UpdateEntity(workshop);
                }
            }
        }

        #endregion
    }
}
