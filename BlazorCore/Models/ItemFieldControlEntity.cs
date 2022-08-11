// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Localizations;
using DataCore.Sql.Models;
using Radzen;
using static DataCore.ShareEnums;

namespace BlazorCore.Models;

public class ItemFieldControlEntity
{
    #region Public and private fields, properties, constructor

    private AppSettingsHelper AppSettings { get; } = AppSettingsHelper.Instance;

    #endregion

    #region Public and private methods

    public bool ProcessChecks(NotificationService? notificationService, BaseEntity? item, string field)
    {
        bool result = item != null;
        string detailAddition = Environment.NewLine;
        switch (item)
        {
            case AccessEntity access:
                Access(ref result, ref detailAddition, access);
                break;
            case BarCodeTypeV2Entity barCodeType:
                BarcodeType(ref result, ref detailAddition, barCodeType);
                break;
            case ContragentV2Entity contragent:
                Contragent(ref result, ref detailAddition, contragent);
                break;
            case HostEntity host:
                Host(ref result, ref detailAddition, host);
                break;
            case LabelEntity label:
                Label(ref result, ref detailAddition, label);
                break;
            case NomenclatureEntity nomenclature:
                Nomenclature(ref result, ref detailAddition, nomenclature);
                break;
            case OrderEntity order:
                Order(ref result, ref detailAddition, order);
                break;
            case OrderStatusEntity orderStatus:
                OrderStatus(ref result, ref detailAddition, orderStatus);
                break;
            case OrderTypeEntity orderType:
                OrderType(ref result, ref detailAddition, orderType);
                break;
            case PluEntity plu:
                Plu(ref result, ref detailAddition, plu);
                break;
            case PrinterEntity printer:
                Printer(ref result, ref detailAddition, printer);
                break;
            case PrinterResourceEntity printerResource:
                PrinterResource(ref result, ref detailAddition, printerResource);
                break;
            case PrinterTypeEntity printerType:
                PrinterType(ref result, ref detailAddition, printerType);
                break;
            case ProductionFacilityEntity productionFacility:
                ProductionFacility(ref result, ref detailAddition, productionFacility);
                break;
            case ProductSeriesEntity productSeries:
                ProductSeries(ref result, ref detailAddition, productSeries);
                break;
            case ScaleEntity scale:
                Scale(ref result, ref detailAddition, scale);
                break;
            case TaskEntity task:
                Task(ref result, ref detailAddition, task);
                break;
            case TaskTypeEntity taskType:
                TaskType(ref result, ref detailAddition, taskType);
                break;
            case TemplateResourceEntity templateResource:
                TemplateResource(ref result, ref detailAddition, templateResource);
                break;
            case TemplateEntity template:
                Template(ref result, ref detailAddition, template);
                break;
            case WeithingFactEntity weithingFact:
                WeithingFact(ref result, ref detailAddition, weithingFact);
                break;
            case WorkShopEntity workshop:
                Workshop(ref result, ref detailAddition, workshop);
                break;
        }
        if (!result)
        {
            NotificationMessage msg = new()
            {
                Severity = NotificationSeverity.Warning,
                Summary = LocaleCore.Action.ActionDataControl,
                Detail = $"{LocaleCore.Action.ActionDataControlField} [{field}]!" +
                    (Equals(detailAddition, Environment.NewLine) ? string.Empty : detailAddition),
                Duration = AppSettingsHelper.Delay
            };
            notificationService?.Notify(msg);
            return false;
        }
        return true;
    }

    private static void Access(ref bool result, ref string detailAddition, AccessEntity access)
    {
        if (access.EqualsDefault())
            result = false;
        if (string.IsNullOrEmpty(access.User))
        {
            detailAddition += $"{LocaleCore.Table.FieldIsEmpty}: {LocaleCore.Table.FieldUser}" + Environment.NewLine;
            result = false;
        }
        if (access.Rights > 3)
        {
            detailAddition += $"{LocaleCore.Table.FieldIsNotInRange}: {LocaleCore.Strings.AccessRights}" + Environment.NewLine;
            result = false;
        }
    }

    private static void BarcodeType(ref bool result, ref string detailAddition, BarCodeTypeV2Entity barCodeType)
    {
        if (barCodeType.EqualsDefault())
            result = false;
        if (string.IsNullOrEmpty(barCodeType.Name))
        {
            detailAddition += $"{LocaleCore.Table.FieldIsEmpty}: {LocaleCore.Table.FieldName}" + Environment.NewLine;
            result = false;
        }
    }

    private static void Contragent(ref bool result, ref string detailAddition, ContragentV2Entity contragent)
    {
        if (contragent.EqualsDefault())
            result = false;
        if (string.IsNullOrEmpty(contragent.Name))
        {
            detailAddition += $"{LocaleCore.Table.FieldIsEmpty}: {LocaleCore.Table.FieldName}" + Environment.NewLine;
            result = false;
        }
    }

    private static void Host(ref bool result, ref string detailAddition, HostEntity host)
    {
        if (host.EqualsDefault())
            result = false;
        if (string.IsNullOrEmpty(host.Name))
        {
            detailAddition += $"{LocaleCore.Table.FieldIsEmpty}: {LocaleCore.Table.FieldName}" + Environment.NewLine;
            result = false;
        }
        //if (Equals(host.IdRRef, Guid.Empty))
        //{
        //    detailAddition += $"{LocaleCore.Table.FieldIsEmpty}: {LocaleCore.Table.FieldIdRRef}" + Environment.NewLine;
        //    result = false;
        //}
        if (string.IsNullOrEmpty(host.Ip))
        {
            detailAddition += $"{LocaleCore.Table.FieldIsEmpty}: {LocaleCore.Table.FieldIpAddress}" + Environment.NewLine;
            result = false;
        }
    }

    private static void Label(ref bool result, ref string detailAddition, LabelEntity label)
    {
        if (label.EqualsDefault())
            result = false;
        if (label.Label.Length == 0)
        {
            detailAddition += $"{LocaleCore.Table.FieldIsEmpty}: {LocaleCore.Table.FieldLabel}" + Environment.NewLine;
            result = false;
        }
    }

    private static void Nomenclature(ref bool result, ref string detailAddition, NomenclatureEntity nomenclature)
    {
        if (nomenclature.EqualsDefault())
            result = false;
        if (string.IsNullOrEmpty(nomenclature.Name))
        {
            detailAddition += $"{LocaleCore.Table.FieldIsEmpty}: {LocaleCore.Table.FieldName}" + Environment.NewLine;
            result = false;
        }
    }

    private static void Order(ref bool result, ref string detailAddition, OrderEntity order)
    {
        if (order.EqualsDefault())
            result = false;
    }

    private static void OrderStatus(ref bool result, ref string detailAddition, OrderStatusEntity orderStatus)
    {
        if (orderStatus.EqualsDefault())
            result = false;
    }

    private static void OrderType(ref bool result, ref string detailAddition, OrderTypeEntity orderType)
    {
        if (orderType.EqualsDefault())
            result = false;
    }

    private void Plu(ref bool result, ref string detailAddition, PluEntity plu)
    {
        if (plu.EqualsDefault())
            result = false;
        PluEntity[]? items = AppSettings.DataAccess.Crud.GetEntities<PluEntity>(
            new(new() { new($"Scale.{DbField.IdentityId}", DbComparer.Equal, plu.Scale.IdentityId),
                new(DbField.PluNumber, DbComparer.Equal, plu.PluNumber)
            }), null);
        if (items != null && items.Any() && !items.Where(x => x.IdentityId.Equals(plu.IdentityId)).Select(x => x).Any())
        {
            detailAddition += $"{LocaleCore.Table.TablePluHavingPlu}: {plu.PluNumber}" + Environment.NewLine;
            result = false;
        }
    }

    private static void Printer(ref bool result, ref string detailAddition, PrinterEntity printer)
    {
        if (printer.EqualsDefault())
            result = false;
    }

    private static void PrinterResource(ref bool result, ref string detailAddition, PrinterResourceEntity printerResource)
    {
        if (printerResource.EqualsDefault())
            result = false;
    }

    private static void PrinterType(ref bool result, ref string detailAddition, PrinterTypeEntity printerType)
    {
        if (printerType.EqualsDefault())
            result = false;
        if (string.IsNullOrEmpty(printerType.Name))
        {
            detailAddition += $"{LocaleCore.Table.FieldIsEmpty}: {LocaleCore.Table.FieldName}" + Environment.NewLine;
            result = false;
        }
    }

    private static void ProductionFacility(ref bool result, ref string detailAddition, ProductionFacilityEntity productionFacility)
    {
        if (productionFacility.EqualsDefault())
            result = false;
        if (string.IsNullOrEmpty(productionFacility.Name))
        {
            detailAddition += $"{LocaleCore.Table.FieldIsEmpty}: {LocaleCore.Table.FieldName}" + Environment.NewLine;
            result = false;
        }
    }

    private static void ProductSeries(ref bool result, ref string detailAddition, ProductSeriesEntity productSeries)
    {
        if (productSeries.EqualsDefault())
            result = false;
    }

    private static void Scale(ref bool result, ref string detailAddition, ScaleEntity scale)
    {
        if (scale.EqualsDefault())
            result = false;
        if (string.IsNullOrEmpty(scale.Description))
        {
            detailAddition += $"{LocaleCore.Table.FieldIsEmpty}: {LocaleCore.Table.FieldDescription}" + Environment.NewLine;
            result = false;
        }
    }

    private static void Task(ref bool result, ref string detailAddition, TaskEntity task)
    {
        if (task.EqualsDefault())
            result = false;
    }

    private static void TaskType(ref bool result, ref string detailAddition, TaskTypeEntity taskType)
    {
        if (taskType.EqualsDefault())
            result = false;
        if (string.IsNullOrEmpty(taskType.Name))
        {
            detailAddition += $"{LocaleCore.Table.FieldIsEmpty}: {LocaleCore.Table.FieldName}" + Environment.NewLine;
            result = false;
        }
    }

    private static void TemplateResource(ref bool result, ref string detailAddition, TemplateResourceEntity templateResource)
    {
        if (templateResource.EqualsDefault())
            result = false;
        if (string.IsNullOrEmpty(templateResource.Name))
        {
            detailAddition += $"{LocaleCore.Table.FieldIsEmpty}: {LocaleCore.Table.FieldName}" + Environment.NewLine;
            result = false;
        }
    }

    private static void Template(ref bool result, ref string detailAddition, TemplateEntity template)
    {
        if (template.EqualsDefault())
            result = false;
        if (string.IsNullOrEmpty(template.Title))
        {
            detailAddition += $"{LocaleCore.Table.FieldIsEmpty}: {LocaleCore.Table.FieldTitle}" + Environment.NewLine;
            result = false;
        }
        if (string.IsNullOrEmpty(template.CategoryId))
        {
            detailAddition += $"{LocaleCore.Table.FieldIsEmpty}: {LocaleCore.Table.FieldCategory}" + Environment.NewLine;
            result = false;
        }
    }

    private static void WeithingFact(ref bool result, ref string detailAddition, WeithingFactEntity weithingFact)
    {
        if (weithingFact.EqualsDefault())
            result = false;
    }

    private static void Workshop(ref bool result, ref string detailAddition, WorkShopEntity workshop)
    {
        if (workshop.EqualsDefault())
            result = false;
        if (string.IsNullOrEmpty(workshop.Name))
        {
            detailAddition += $"{LocaleCore.Table.FieldIsEmpty}: {LocaleCore.Table.FieldName}" + Environment.NewLine;
            result = false;
        }
    }

    #endregion
}
