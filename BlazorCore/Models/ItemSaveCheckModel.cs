// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Localizations;
using DataCore.Sql.Core;
using DataCore.Sql.Tables;
using Radzen;

namespace BlazorCore.Models;

public class ItemSaveCheckModel
{
    #region Public and private fields, properties, constructor

    private AppSettingsHelper AppSettings { get; } = AppSettingsHelper.Instance;
    private ItemFieldControlModel FieldControl { get; } = new ItemFieldControlModel();

    #endregion

    #region Public and private methods

    public void Access(NotificationService? notificationService, AccessModel? item, SqlTableActionEnum tableAction)
    {
        if (item == null) return;
        item.ChangeDt = DateTime.Now;

        bool success = FieldControl.ValidateModel(notificationService, item, LocaleCore.Strings.AccessRights);
        if (success)
        {
            if (tableAction == SqlTableActionEnum.New)
            {
                item.CreateDt = DateTime.Now;
                AppSettings.DataAccess.Save(item);
            }
            else
            {
                AppSettings.DataAccess.Update(item);
            }
        }
    }

    public void BarcodeType(NotificationService? notificationService, BarCodeTypeModel? item, SqlTableActionEnum tableAction)
    {
        if (item == null) return;
        item.ChangeDt = DateTime.Now;

        bool success = FieldControl.ValidateModel(notificationService, item, LocaleCore.Table.BarcodeType);
        if (success)
        {
            if (tableAction == SqlTableActionEnum.New)
            {
                item.CreateDt = DateTime.Now;
                AppSettings.DataAccess.Save(item);
            }
            else
            {
                AppSettings.DataAccess.Update(item);
            }
        }
    }

    public void Contragent(NotificationService? notificationService, ContragentModel? item, SqlTableActionEnum tableAction)
    {
        if (item == null) return;
        item.ChangeDt = DateTime.Now;

        bool success = FieldControl.ValidateModel(notificationService, item, LocaleCore.Table.Contragent);
        if (success)
        {
            if (tableAction == SqlTableActionEnum.New)
            {
                item.CreateDt = DateTime.Now;
                AppSettings.DataAccess.Save(item);
            }
            else
            {
                AppSettings.DataAccess.Update(item);
            }
        }
    }

    public void Host(NotificationService? notificationService, HostModel? item, SqlTableActionEnum tableAction)
    {
        if (item == null) return;
        item.ChangeDt = DateTime.Now;

        bool success = FieldControl.ValidateModel(notificationService, item, LocaleCore.Table.Host);
        if (success)
        {
            if (tableAction == SqlTableActionEnum.New)
            {
                item.CreateDt = DateTime.Now;
                AppSettings.DataAccess.Save(item);
            }
            else
            {
                AppSettings.DataAccess.Update(item);
            }
        }
    }

    public void Nomenclature(NotificationService? notificationService, NomenclatureModel? item, SqlTableActionEnum tableAction)
    {
        if (item == null) return;
        item.ChangeDt = DateTime.Now;

        bool success = FieldControl.ValidateModel(notificationService, item, LocaleCore.Table.Nomenclature);
        if (success)
        {
            if (tableAction == SqlTableActionEnum.New)
            {
                item.CreateDt = DateTime.Now;
                AppSettings.DataAccess.Save(item);
            }
            else
            {
                AppSettings.DataAccess.Update(item);
            }
        }
    }

    public void PluObsolete(NotificationService? notificationService, PluObsoleteModel? item, SqlTableActionEnum tableAction)
    {
        if (item == null) return;
        item.ChangeDt = DateTime.Now;

        bool success = FieldControl.ValidateModel(notificationService, item, LocaleCore.Table.Plu);
        if (success)
            success = FieldControl.ValidateModel(notificationService, item.Scale, LocaleCore.Table.Device);
        if (success)
            success = FieldControl.ValidateModel(notificationService, item.Template, LocaleCore.Table.LabelTemplate);
        if (success)
            success = FieldControl.ValidateModel(notificationService, item.Nomenclature, LocaleCore.Table.Product);
        if (success)
        {
            if (tableAction == SqlTableActionEnum.New)
            {
                item.CreateDt = DateTime.Now;
                AppSettings.DataAccess.Save(item);
            }
            else
            {
                AppSettings.DataAccess.Update(item);
            }
        }
    }

    public void Plu(NotificationService? notificationService, PluModel? item, SqlTableActionEnum tableAction)
    {
        if (item == null) return;
        item.ChangeDt = DateTime.Now;

        bool success = FieldControl.ValidateModel(notificationService, item, LocaleCore.Table.Plu);
        if (success)
            success = FieldControl.ValidateModel(notificationService, item.Template, LocaleCore.Table.LabelTemplate);
        if (success)
            success = FieldControl.ValidateModel(notificationService, item.Nomenclature, LocaleCore.Table.Product);
        if (success)
        {
            if (tableAction == SqlTableActionEnum.New)
            {
                item.CreateDt = DateTime.Now;
                AppSettings.DataAccess.Save(item);
            }
            else
            {
                AppSettings.DataAccess.Update(item);
            }
        }
    }

    public void PluScale(NotificationService? notificationService, PluScaleModel? item, SqlTableActionEnum tableAction)
    {
        if (item == null) return;
        item.ChangeDt = DateTime.Now;

        bool success = FieldControl.ValidateModel(notificationService, item, LocaleCore.Table.PluScale);
        if (success)
            success = FieldControl.ValidateModel(notificationService, item.Plu, LocaleCore.Table.Plu);
        if (success)
            success = FieldControl.ValidateModel(notificationService, item.Scale, LocaleCore.Table.Scale);
        if (success)
        {
            if (tableAction == SqlTableActionEnum.New)
            {
                item.CreateDt = DateTime.Now;
                AppSettings.DataAccess.Save(item);
            }
            else
            {
                AppSettings.DataAccess.Update(item);
            }
        }
    }

    public void Printer(NotificationService? notificationService, PrinterModel? item, SqlTableActionEnum tableAction)
    {
        if (item == null) return;
        item.ChangeDt = DateTime.Now;

        bool success = FieldControl.ValidateModel(notificationService, item, LocaleCore.Table.Printer);
        if (success)
            success = FieldControl.ValidateModel(notificationService, item.PrinterType, LocaleCore.Table.PrinterType);
        if (success)
        {
            if (tableAction == SqlTableActionEnum.New)
            {
                item.CreateDt = DateTime.Now;
                AppSettings.DataAccess.Save(item);
            }
            else
            {
                AppSettings.DataAccess.Update(item);
            }
        }
    }

    public void PrinterResource(NotificationService? notificationService, PrinterResourceModel? item, SqlTableActionEnum tableAction)
    {
        if (item == null) return;
        item.ChangeDt = DateTime.Now;

        bool success = FieldControl.ValidateModel(notificationService, item, LocaleCore.Table.PrinterResource);
        if (success)
        {
            if (tableAction == SqlTableActionEnum.New)
            {
                item.CreateDt = DateTime.Now;
                AppSettings.DataAccess.Save(item);
            }
            else
            {
                AppSettings.DataAccess.Update(item);
            }
        }
    }

    public void PrinterType(NotificationService? notificationService, PrinterTypeModel? item, SqlTableActionEnum tableAction)
    {
        if (item == null) return;
        item.ChangeDt = DateTime.Now;

        bool success = FieldControl.ValidateModel(notificationService, item, LocaleCore.Table.PrinterType);
        if (success)
        {
            if (tableAction == SqlTableActionEnum.New)
            {
                item.CreateDt = DateTime.Now;
                AppSettings.DataAccess.Save(item);
            }
            else
            {
                AppSettings.DataAccess.Update(item);
            }
        }
    }

    public void ProductionFacility(NotificationService? notificationService, ProductionFacilityModel? item, SqlTableActionEnum tableAction)
    {
        if (item == null) return;
        item.ChangeDt = DateTime.Now;

        bool success = FieldControl.ValidateModel(notificationService, item, LocaleCore.Table.ProductionFacility);
        if (success)
        {
            if (tableAction == SqlTableActionEnum.New)
            {
                item.CreateDt = DateTime.Now;
                AppSettings.DataAccess.Save(item);
            }
            else
            {
                AppSettings.DataAccess.Update(item);
            }
        }
    }

    public void Scale(NotificationService? notificationService, SqlTableBase? item, SqlTableActionEnum tableAction)
    {
        if (item == null) return;
        item.ChangeDt = DateTime.Now;

        if (item is ScaleModel scale)
        {
            // Check PrinterMain is null.
            bool success = FieldControl.ValidateModel(notificationService, scale, LocaleCore.Table.Device);
            if (success)
            {
                if (scale.Host?.Identity.Id != 0)
                {
                    scale.Host = AppSettings.DataAccess.GetItemById<HostModel>(scale.Host?.Identity.Id);
                    success = FieldControl.ValidateModel(notificationService, scale.Host, LocaleCore.Table.Host);
                }
                else
                    scale.Host = new();
            }
            if (success)
            {
                if (scale.PrinterMain?.Identity.Id != 0)
                {
                    scale.PrinterMain = AppSettings.DataAccess.GetItemById<PrinterModel>(scale.PrinterMain?.Identity.Id);
                    success = FieldControl.ValidateModel(notificationService, scale.PrinterMain, LocaleCore.Table.Printer);
                }
                else
                    scale.PrinterMain = null;
            }
            if (success)
            {
                if (scale.PrinterShipping?.Identity.Id != 0)
                {
                    scale.PrinterShipping = AppSettings.DataAccess.GetItemById<PrinterModel>(scale.PrinterShipping?.Identity.Id);
                    success = FieldControl.ValidateModel(notificationService, scale.PrinterShipping, LocaleCore.Table.Printer);
                }
                else
                    scale.PrinterShipping = null;
            }
            if (success)
            {
                if (scale.TemplateDefault?.Identity.Id != 0)
                {
                    scale.TemplateDefault = AppSettings.DataAccess.GetItemById<TemplateModel>(scale.TemplateDefault?.Identity.Id);
                    success = FieldControl.ValidateModel(notificationService, scale.TemplateDefault, LocaleCore.Table.Template);
                }
                else
                    scale.TemplateDefault = null;
            }
            if (success)
            {
                if (scale.TemplateSeries?.Identity.Id != 0)
                {
                    scale.TemplateSeries = AppSettings.DataAccess.GetItemById<TemplateModel>(scale.TemplateSeries?.Identity.Id);
                    success = FieldControl.ValidateModel(notificationService, scale.TemplateSeries, LocaleCore.Table.Template);
                }
                else
                    scale.TemplateSeries = null;
            }
            if (success)
            {
                if (scale.WorkShop?.Identity.Id != 0)
                {
                    scale.WorkShop = AppSettings.DataAccess.GetItemById<WorkShopModel>(scale.WorkShop?.Identity.Id);
                    success = FieldControl.ValidateModel(notificationService, scale.WorkShop, LocaleCore.Table.Template);
                }
                else
                    scale.WorkShop = null;
            }
            if (success)
            {
                scale.ChangeDt = DateTime.Now;
                switch (tableAction)
                {
                    case SqlTableActionEnum.New:
                        {
                            scale.CreateDt = DateTime.Now;
                            if (scale.TemplateSeries != null && scale.TemplateSeries.EqualsDefault())
                                scale.TemplateSeries = null;
                            AppSettings.DataAccess.Save(scale);
                            break;
                        }
                    case SqlTableActionEnum.Save:
                        AppSettings.DataAccess.Update(scale);
                        break;
                }
            }
        }
    }

    public void Task(NotificationService? notificationService, TaskModel? item, SqlTableActionEnum tableAction)
    {
        if (item == null) return;
        item.ChangeDt = DateTime.Now;

        bool success = FieldControl.ValidateModel(notificationService, item, LocaleCore.Table.TaskModule);
        if (success)
            success = FieldControl.ValidateModel(notificationService, item.TaskType, LocaleCore.Table.TaskType);
        if (success)
            success = FieldControl.ValidateModel(notificationService, item.Scale, LocaleCore.Table.Device);
        if (success)
        {
            if (tableAction == SqlTableActionEnum.New)
            {
                item.CreateDt = DateTime.Now;
                AppSettings.DataAccess.Save(item);
            }
            else
            {
                AppSettings.DataAccess.Update(item);
            }
        }
    }

    public void TaskType(NotificationService? notificationService, TaskTypeModel? item, SqlTableActionEnum tableAction)
    {
        if (item == null) return;
        item.ChangeDt = DateTime.Now;

        bool success = FieldControl.ValidateModel(notificationService, item, LocaleCore.Table.TaskModuleType);
        if (success)
        {
            if (tableAction == SqlTableActionEnum.New)
            {
                item.CreateDt = DateTime.Now;
                AppSettings.DataAccess.Save(item);
            }
            else
            {
                AppSettings.DataAccess.Update(item);
            }
        }
    }

    public void Template(NotificationService? notificationService, TemplateModel? item, SqlTableActionEnum? parentTableAction)
    {
        if (item == null) return;
        item.ChangeDt = DateTime.Now;

        bool success = FieldControl.ValidateModel(notificationService, item, LocaleCore.Table.Template);
        if (success)
        {
            if (parentTableAction == SqlTableActionEnum.New)
            {
                item.CreateDt = DateTime.Now;
                AppSettings.DataAccess.Save(item);
            }
            else
            {
                AppSettings.DataAccess.Update(item);
            }
        }
    }

    public void TemplateResource(NotificationService? notificationService, TemplateResourceModel? item, SqlTableActionEnum? parentTableAction)
    {
        if (item == null) return;
        item.ChangeDt = DateTime.Now;

        bool success = FieldControl.ValidateModel(notificationService, item, LocaleCore.Table.TemplateResource);
        if (success)
        {
            if (parentTableAction == SqlTableActionEnum.New)
            {
                item.CreateDt = DateTime.Now;
                AppSettings.DataAccess.Save(item);
            }
            else
            {
                AppSettings.DataAccess.Update(item);
            }
        }
    }

    public void Workshop(NotificationService? notificationService, WorkShopModel? item, SqlTableActionEnum tableAction)
    {
        if (item == null) return;
        item.ChangeDt = DateTime.Now;

        bool success = FieldControl.ValidateModel(notificationService, item, LocaleCore.Table.Workshop);
        if (success)
        {
            if (tableAction == SqlTableActionEnum.New)
            {
                item.CreateDt = DateTime.Now;
                AppSettings.DataAccess.Save(item);
            }
            else
            {
                AppSettings.DataAccess.Update(item);
            }
        }
    }

    #endregion
}
