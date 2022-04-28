// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels;
using DataCore.Localizations;
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

        public void Access(NotificationService? notificationService, AccessEntity? access, 
            Guid? uid, DbTableAction tableAction)
        {
            if (access == null || uid == null)
                return;

            bool success = FieldControl.ProcessChecks(notificationService, access, LocaleCore.Strings.AccessRights);
            if (success)
            {
                access.ChangeDt = DateTime.Now;
                if (tableAction == DbTableAction.New)
                {
                    access.CreateDt = DateTime.Now;
                    AppSettings.DataAccess?.Crud.SaveEntity(access);
                }
                else
                {
                    if (uid is Guid guid)
                    {
                        access.IdentityUid = guid;
                        AppSettings.DataAccess?.Crud.UpdateEntity(access);
                    }
                }
            }
        }

        public void BarcodeType(NotificationService? notificationService, BarCodeTypeEntityV2? barcodeType, 
            Guid? uid, DbTableAction tableAction)
        {
            if (barcodeType == null || uid == null)
                return;

            bool success = FieldControl.ProcessChecks(notificationService, barcodeType, LocaleCore.Table.BarcodeType);
            if (success)
            {
                barcodeType.ChangeDt = DateTime.Now;
                if (tableAction == DbTableAction.New)
                {
                    barcodeType.CreateDt = DateTime.Now;
                    AppSettings.DataAccess?.Crud.SaveEntity(barcodeType);
                }
                else
                {
                    if (uid is Guid guid)
                    {
                        barcodeType.IdentityUid = guid;
                        AppSettings.DataAccess?.Crud.UpdateEntity(barcodeType);
                    }
                }
            }
        }

        public void Contragent(NotificationService? notificationService, ContragentEntityV2? contragent, 
            Guid? uid, DbTableAction tableAction)
        {
            if (contragent == null || uid == null)
                return;

            bool success = FieldControl.ProcessChecks(notificationService, contragent, LocaleCore.Table.Contragent);
            if (success)
            {
                contragent.ChangeDt = DateTime.Now;
                if (tableAction == DbTableAction.New)
                {
                    contragent.CreateDt = DateTime.Now;
                    AppSettings.DataAccess?.Crud.SaveEntity(contragent);
                }
                else
                {
                    if (uid is Guid guid)
                    {
                        contragent.IdentityUid = guid;
                        AppSettings.DataAccess?.Crud.UpdateEntity(contragent);
                    }
                }
            }
        }

        public void Host(NotificationService? notificationService, HostEntity? host, long? id, DbTableAction tableAction)
        {
            if (host == null || id == null)
                return;

            bool success = FieldControl.ProcessChecks(notificationService, host, LocaleCore.Table.Host);
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
                    if (id is long lid)
                    {
                        host.IdentityId = lid;
                        AppSettings.DataAccess?.Crud.UpdateEntity(host);
                    }
                }
            }
        }

        public void Nomenclature(NotificationService? notificationService, NomenclatureEntity? nomenclature, 
            long? id, DbTableAction tableAction)
        {
            if (nomenclature == null || id == null)
                return;

            bool success = FieldControl.ProcessChecks(notificationService, nomenclature, LocaleCore.Table.Nomenclature);
            if (success)
            {
                nomenclature.ChangeDt = DateTime.Now;
                if (tableAction == DbTableAction.New)
                {
                    nomenclature.CreateDt = DateTime.Now;
                    AppSettings.DataAccess?.Crud.SaveEntity(nomenclature);
                }
                else
                {
                    if (id is long lid)
                    {
                        nomenclature.IdentityId = lid;
                        AppSettings.DataAccess?.Crud.UpdateEntity(nomenclature);
                    }
                }
            }
        }

        public void Plu(NotificationService? notificationService, PluEntity? plu, long? id, DbTableAction tableAction)
        {
            if (plu == null || id == null)
                return;

            plu.ChangeDt = DateTime.Now;
            bool success = FieldControl.ProcessChecks(notificationService, plu, LocaleCore.Table.Plu);
            if (success)
                success = FieldControl.ProcessChecks(notificationService, plu.Scale, LocaleCore.Table.Device);
            if (success)
                success = FieldControl.ProcessChecks(notificationService, plu.Template, LocaleCore.Table.LabelTemplate);
            if (success)
                success = FieldControl.ProcessChecks(notificationService, plu.Nomenclature, LocaleCore.Table.Product);
            if (success)
            {
                plu.ChangeDt = DateTime.Now;
                if (tableAction == DbTableAction.New)
                {
                    plu.CreateDt = DateTime.Now;
                    AppSettings.DataAccess?.Crud.SaveEntity(plu);
                }
                else
                {
                    if (id is long lid)
                    {
                        plu.IdentityId = lid;
                        AppSettings.DataAccess?.Crud.UpdateEntity(plu);
                    }
                }
            }
        }

        public void Printer(NotificationService? notificationService, PrinterEntity? printer, 
            long? id, DbTableAction tableAction)
        {
            if (printer == null || id == null)
                return;

            printer.ChangeDt = DateTime.Now;
            bool success = FieldControl.ProcessChecks(notificationService, printer, LocaleCore.Table.Printer);
            if (success)
                success = FieldControl.ProcessChecks(notificationService, printer.PrinterType, LocaleCore.Table.PrinterType);
            if (success)
            {
                printer.ChangeDt = DateTime.Now;
                if (tableAction == DbTableAction.New)
                {
                    printer.CreateDt = DateTime.Now;
                    AppSettings.DataAccess?.Crud.SaveEntity(printer);
                }
                else
                {
                    if (id is long lid)
                    {
                        printer.IdentityId = lid;
                        AppSettings.DataAccess?.Crud.UpdateEntity(printer);
                    }
                }
            }
        }

        public void PrinterResource(NotificationService? notificationService, PrinterResourceEntity? printerResource, 
            long? id, DbTableAction tableAction)
        {
            if (printerResource == null || id == null)
                return;

            bool success = FieldControl.ProcessChecks(notificationService, printerResource, LocaleCore.Table.PrinterResource);
            if (success)
            {
                printerResource.ChangeDt = DateTime.Now;
                if (tableAction == DbTableAction.New)
                {
                    printerResource.CreateDt = DateTime.Now;
                    AppSettings.DataAccess?.Crud.SaveEntity(printerResource);
                }
                else
                {
                    if (id is long lid)
                    {
                        printerResource.IdentityId = lid;
                        AppSettings.DataAccess?.Crud.UpdateEntity(printerResource);
                    }
                }
            }
        }

        public void PrinterType(NotificationService? notificationService, PrinterTypeEntity? printerType, 
            long? id, DbTableAction tableAction)
        {
            if (printerType == null || id == null)
                return;

            bool success = FieldControl.ProcessChecks(notificationService, printerType, LocaleCore.Table.PrinterType);
            if (success)
            {
                printerType.ChangeDt = DateTime.Now;
                if (tableAction == DbTableAction.New)
                {
                    printerType.CreateDt = DateTime.Now;
                    AppSettings.DataAccess?.Crud.SaveEntity(printerType);
                }
                else
                {
                    if (id is long lid)
                    {
                        printerType.IdentityId = lid;
                        AppSettings.DataAccess?.Crud.UpdateEntity(printerType);
                    }
                }
            }
        }

        public void ProductionFacility(NotificationService? notificationService, ProductionFacilityEntity? productionFacility, 
            long? id, DbTableAction tableAction)
        {
            if (productionFacility == null || id == null)
                return;

            bool success = FieldControl.ProcessChecks(notificationService, productionFacility, LocaleCore.Table.ProductionFacility);
            if (success)
            {
                productionFacility.ChangeDt = DateTime.Now;
                if (tableAction == DbTableAction.New)
                {
                    productionFacility.CreateDt = DateTime.Now;
                    AppSettings.DataAccess?.Crud.SaveEntity(productionFacility);
                }
                else
                {
                    if (id is long lid)
                    {
                        productionFacility.IdentityId = lid;
                        AppSettings.DataAccess?.Crud.UpdateEntity(productionFacility);
                    }
                }
            }
        }

        public void Scale(NotificationService? notificationService, ScaleEntity? scale, 
            long? id, DbTableAction tableAction)
        {
            if (scale == null || id == null)
                return;

            scale.ChangeDt = DateTime.Now;
            bool success = FieldControl.ProcessChecks(notificationService, scale, LocaleCore.Table.Device);
            if (success)
                success = FieldControl.ProcessChecks(notificationService, scale.PrinterMain, LocaleCore.Table.Printer);
            if (success)
                success = FieldControl.ProcessChecks(notificationService, scale.Host, LocaleCore.Table.Host);
            //if (success)
            //    success = FieldControl.ProcessChecks(notificationService, scale.TemplateDefault, LocalizationCore.Strings.TableItem.TemplateDefault);
            if (success)
                success = FieldControl.ProcessChecks(notificationService, scale.WorkShop, LocaleCore.Table.Workshop);
            if (success)
            {
                scale.ChangeDt = DateTime.Now;
                if (tableAction == DbTableAction.New)
                {
                    scale.CreateDt = DateTime.Now;
                    if (scale.TemplateSeries != null && scale.TemplateSeries.EqualsDefault())
                        scale.TemplateSeries = null;
                    AppSettings.DataAccess?.Crud.SaveEntity(scale);
                }
                else
                {
                    if (id is long lid)
                    {
                        scale.IdentityId = lid;
                        AppSettings.DataAccess?.Crud.UpdateEntity(scale);
                    }
                }
            }
        }

        public void Task(NotificationService? notificationService, TaskEntity? task, 
            Guid? uid, DbTableAction tableAction)
        {
            if (task == null || uid == null)
                return;

            bool success = FieldControl.ProcessChecks(notificationService, task, LocaleCore.Table.TaskModule);
            if (success)
                success = FieldControl.ProcessChecks(notificationService, task.TaskType, LocaleCore.Table.TaskType);
            if (success)
                success = FieldControl.ProcessChecks(notificationService, task.Scale, LocaleCore.Table.Device);
            if (success)
            {
                task.ChangeDt = DateTime.Now;
                if (tableAction == DbTableAction.New)
                {
                    task.CreateDt = DateTime.Now;
                    AppSettings.DataAccess?.Crud.SaveEntity(task);
                }
                else
                {
                    if (uid is Guid guid)
                    {
                        task.IdentityUid = guid;
                        AppSettings.DataAccess?.Crud.UpdateEntity(task);
                    }
                }
            }
        }

        public void TaskType(NotificationService? notificationService, TaskTypeEntity? taskType, 
            Guid? uid, DbTableAction tableAction)
        {
            if (taskType == null || uid == null)
                return;

            bool success = FieldControl.ProcessChecks(notificationService, taskType, LocaleCore.Table.TaskModuleType);
            if (success)
            {
                taskType.ChangeDt = DateTime.Now;
                if (tableAction == DbTableAction.New)
                {
                    taskType.CreateDt = DateTime.Now;
                    AppSettings.DataAccess?.Crud.SaveEntity(taskType);
                }
                else
                {
                    if (uid is Guid guid)
                    {
                        taskType.IdentityUid = guid;
                        AppSettings.DataAccess?.Crud.UpdateEntity(taskType);
                    }
                }
            }
        }

        public void Template(NotificationService? notificationService, TemplateEntity? template, long? id, 
            DbTableAction? parentTableAction)
        {
            if (template == null)
                return;

            bool success = FieldControl.ProcessChecks(notificationService, template, LocaleCore.Table.Template);
            if (success)
            {
                template.ChangeDt = DateTime.Now;
                if (parentTableAction == DbTableAction.New)
                {
                    template.CreateDt = DateTime.Now;
                    AppSettings.DataAccess?.Crud.SaveEntity(template);
                }
                else
                {
                    if (id is long lid)
                    {
                        template.IdentityId = lid;
                        AppSettings.DataAccess?.Crud.UpdateEntity(template);
                    }
                }
            }
        }

        public void Workshop(NotificationService? notificationService, WorkShopEntity? workShop, 
            long? id, DbTableAction tableAction)
        {
            if (workShop == null || id == null)
                return;

            bool success = FieldControl.ProcessChecks(notificationService, workShop, LocaleCore.Table.Workshop);
            if (success)
            {
                workShop.ChangeDt = DateTime.Now;
                if (tableAction == DbTableAction.New)
                {
                    workShop.CreateDt = DateTime.Now;
                    AppSettings.DataAccess?.Crud.SaveEntity(workShop);
                }
                else
                {
                    if (id is long lid)
                    {
                        workShop.IdentityId = lid;
                        AppSettings.DataAccess?.Crud.UpdateEntity(workShop);
                    }
                }
            }
        }

        #endregion
    }
}
