// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Localizations;
using DataCore.Sql.Models;
using FluentValidation.Results;
using Radzen;
using static DataCore.ShareEnums;

namespace BlazorCore.Models;

public class ItemFieldControlEntity
{
    #region Public and private fields, properties, constructor

    private AppSettingsHelper AppSettings { get; } = AppSettingsHelper.Instance;

    #endregion

    #region Public and private methods

    public bool ValidateEntity(NotificationService? notificationService, BaseEntity? item, string field)
    {
        bool result = item != null;
        string detailAddition = Environment.NewLine;
        switch (item)
        {
            case AccessEntity access:
                ValidateAccess(ref result, ref detailAddition, access);
                break;
            case BarCodeTypeV2Entity barCodeType:
                ValidateBarcodeType(ref result, ref detailAddition, barCodeType);
                break;
            case ContragentV2Entity contragent:
                ValidateContragent(ref result, ref detailAddition, contragent);
                break;
            case HostEntity host:
                ValidateHost(ref result, ref detailAddition, host);
                break;
            case LabelEntity label:
                ValidateLabel(ref result, ref detailAddition, label);
                break;
            case NomenclatureEntity nomenclature:
                ValidateNomenclature(ref result, ref detailAddition, nomenclature);
                break;
            case OrderEntity order:
	            result = ValidateOrder(order);
                break;
            case PluEntity plu:
                ValidatePlu(ref result, ref detailAddition, plu);
                break;
            case PrinterEntity printer:
                ValidatePrinter(ref result, ref detailAddition, printer);
                break;
            case PrinterResourceEntity printerResource:
                ValidatePrinterResource(ref result, ref detailAddition, printerResource);
                break;
            case PrinterTypeEntity printerType:
                ValidatePrinterType(ref result, ref detailAddition, printerType);
                break;
            case ProductionFacilityEntity productionFacility:
                ValidateProductionFacility(ref result, ref detailAddition, productionFacility);
                break;
            case ProductSeriesEntity productSeries:
                ValidateProductSeries(ref result, ref detailAddition, productSeries);
                break;
            case ScaleEntity scale:
                ValidateScale(ref result, ref detailAddition, scale);
                break;
            case TaskEntity task:
                ValidateTask(ref result, ref detailAddition, task);
                break;
            case TaskTypeEntity taskType:
                ValidateTaskType(ref result, ref detailAddition, taskType);
                break;
            case TemplateResourceEntity templateResource:
                ValidateTemplateResource(ref result, ref detailAddition, templateResource);
                break;
            case TemplateEntity template:
                ValidateTemplate(ref result, ref detailAddition, template);
                break;
            case WeithingFactEntity weithingFact:
                ValidateWeithingFact(ref result, ref detailAddition, weithingFact);
                break;
            case WorkShopEntity workshop:
                ValidateWorkshop(ref result, ref detailAddition, workshop);
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

    private static void ValidateAccess(ref bool result, ref string detailAddition, AccessEntity access)
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

    private static void ValidateBarcodeType(ref bool result, ref string detailAddition, BarCodeTypeV2Entity barCodeType)
    {
        if (barCodeType.EqualsDefault())
            result = false;
        if (string.IsNullOrEmpty(barCodeType.Name))
        {
            detailAddition += $"{LocaleCore.Table.FieldIsEmpty}: {LocaleCore.Table.FieldName}" + Environment.NewLine;
            result = false;
        }
    }

    private static void ValidateContragent(ref bool result, ref string detailAddition, ContragentV2Entity contragent)
    {
        if (contragent.EqualsDefault())
            result = false;
        if (string.IsNullOrEmpty(contragent.Name))
        {
            detailAddition += $"{LocaleCore.Table.FieldIsEmpty}: {LocaleCore.Table.FieldName}" + Environment.NewLine;
            result = false;
        }
    }

    private static void ValidateHost(ref bool result, ref string detailAddition, HostEntity host)
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

    private static void ValidateLabel(ref bool result, ref string detailAddition, LabelEntity label)
    {
        if (label.EqualsDefault())
            result = false;
        if (label.Label.Length == 0)
        {
            detailAddition += $"{LocaleCore.Table.FieldIsEmpty}: {LocaleCore.Table.FieldLabel}" + Environment.NewLine;
            result = false;
        }
    }

    private static void ValidateNomenclature(ref bool result, ref string detailAddition, NomenclatureEntity nomenclature)
    {
        if (nomenclature.EqualsDefault())
            result = false;
        if (string.IsNullOrEmpty(nomenclature.Name))
        {
            detailAddition += $"{LocaleCore.Table.FieldIsEmpty}: {LocaleCore.Table.FieldName}" + Environment.NewLine;
            result = false;
        }
    }

    private static bool ValidateOrder(OrderEntity order)
    {
	    OrderValidator validator = new();
	    ValidationResult validationResult = validator.Validate(order);
		return validationResult.IsValid;
    }

    private void ValidatePlu(ref bool result, ref string detailAddition, PluEntity plu)
    {
        if (plu.EqualsDefault())
            result = false;
        PluEntity[]? items = AppSettings.DataAccess.Crud.GetEntities<PluEntity>(
            new(new() { new($"Scale.{DbField.IdentityId}", DbComparer.Equal, plu.Scale.IdentityId),
                new(DbField.PluNumber, DbComparer.Equal, plu.PluNumber)
            }));
        if (items != null && items.Any() && !items.Where(x => x.IdentityId.Equals(plu.IdentityId)).Select(x => x).Any())
        {
            detailAddition += $"{LocaleCore.Table.TablePluHavingPlu}: {plu.PluNumber}" + Environment.NewLine;
            result = false;
        }
    }

    private static void ValidatePrinter(ref bool result, ref string detailAddition, PrinterEntity printer)
    {
        if (printer.EqualsDefault())
            result = false;
    }

    private static void ValidatePrinterResource(ref bool result, ref string detailAddition, PrinterResourceEntity printerResource)
    {
        if (printerResource.EqualsDefault())
            result = false;
    }

    private static void ValidatePrinterType(ref bool result, ref string detailAddition, PrinterTypeEntity printerType)
    {
        if (printerType.EqualsDefault())
            result = false;
        if (string.IsNullOrEmpty(printerType.Name))
        {
            detailAddition += $"{LocaleCore.Table.FieldIsEmpty}: {LocaleCore.Table.FieldName}" + Environment.NewLine;
            result = false;
        }
    }

    private static void ValidateProductionFacility(ref bool result, ref string detailAddition, ProductionFacilityEntity productionFacility)
    {
        if (productionFacility.EqualsDefault())
            result = false;
        if (string.IsNullOrEmpty(productionFacility.Name))
        {
            detailAddition += $"{LocaleCore.Table.FieldIsEmpty}: {LocaleCore.Table.FieldName}" + Environment.NewLine;
            result = false;
        }
    }

    private static void ValidateProductSeries(ref bool result, ref string detailAddition, ProductSeriesEntity productSeries)
    {
        if (productSeries.EqualsDefault())
            result = false;
    }

    private static void ValidateScale(ref bool result, ref string detailAddition, ScaleEntity scale)
    {
        if (scale.EqualsDefault())
            result = false;
        if (string.IsNullOrEmpty(scale.Description))
        {
            detailAddition += $"{LocaleCore.Table.FieldIsEmpty}: {LocaleCore.Table.FieldDescription}" + Environment.NewLine;
            result = false;
        }
    }

    private static void ValidateTask(ref bool result, ref string detailAddition, TaskEntity task)
    {
        if (task.EqualsDefault())
            result = false;
    }

    private static void ValidateTaskType(ref bool result, ref string detailAddition, TaskTypeEntity taskType)
    {
        if (taskType.EqualsDefault())
            result = false;
        if (string.IsNullOrEmpty(taskType.Name))
        {
            detailAddition += $"{LocaleCore.Table.FieldIsEmpty}: {LocaleCore.Table.FieldName}" + Environment.NewLine;
            result = false;
        }
    }

    private static void ValidateTemplateResource(ref bool result, ref string detailAddition, TemplateResourceEntity templateResource)
    {
        if (templateResource.EqualsDefault())
            result = false;
        if (string.IsNullOrEmpty(templateResource.Name))
        {
            detailAddition += $"{LocaleCore.Table.FieldIsEmpty}: {LocaleCore.Table.FieldName}" + Environment.NewLine;
            result = false;
        }
    }

    private static void ValidateTemplate(ref bool result, ref string detailAddition, TemplateEntity template)
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

    private static void ValidateWeithingFact(ref bool result, ref string detailAddition, WeithingFactEntity weithingFact)
    {
        if (weithingFact.EqualsDefault())
            result = false;
    }

    private static void ValidateWorkshop(ref bool result, ref string detailAddition, WorkShopEntity workshop)
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
