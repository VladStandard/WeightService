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

    public bool ValidateEntity(NotificationService notificationService, BaseEntity? item, string field)
    {
        bool result = item != null;
        string detailAddition = Environment.NewLine;
        switch (item)
        {
            case AccessEntity access:
	            result = ValidateAccess(ref detailAddition, access);
                break;
            case BarCodeTypeV2Entity barCodeType:
	            result = ValidateBarcodeType(ref detailAddition, barCodeType);
                break;
            case ContragentV2Entity contragent:
	            result = ValidateContragent(ref detailAddition, contragent);
                break;
            case HostEntity host:
	            result = ValidateHost(ref detailAddition, host);
                break;
            case LabelEntity label:
	            result = ValidateLabel(ref detailAddition, label);
                break;
            case NomenclatureEntity nomenclature:
	            result = ValidateNomenclature(ref detailAddition, nomenclature);
                break;
            case OrderEntity order:
	            result = ValidateOrder(order);
                break;
            case PluObsoleteEntity pluObsolete:
                result = ValidatePluObsolete(ref detailAddition, pluObsolete);
                break;
            case PluScaleEntity pluScale:
	            result = ValidatePluScale(ref detailAddition, pluScale);
                break;
            case PrinterEntity printer:
                result = ValidatePrinter(ref detailAddition, printer);
                break;
            case PrinterResourceEntity printerResource:
	            result = ValidatePrinterResource(ref detailAddition, printerResource);
                break;
            case PrinterTypeEntity printerType:
	            result = ValidatePrinterType(ref detailAddition, printerType);
                break;
            case ProductionFacilityEntity productionFacility:
	            result = ValidateProductionFacility(ref detailAddition, productionFacility);
                break;
            case ProductSeriesEntity productSeries:
	            result = ValidateProductSeries(ref detailAddition, productSeries);
                break;
            case ScaleEntity scale:
	            result = ValidateScale(ref detailAddition, scale);
                break;
            case TaskEntity task:
	            result = ValidateTask(ref detailAddition, task);
                break;
            case TaskTypeEntity taskType:
	            result = ValidateTaskType(ref detailAddition, taskType);
                break;
            case TemplateResourceEntity templateResource:
	            result = ValidateTemplateResource(ref detailAddition, templateResource);
                break;
            case TemplateEntity template:
	            result = ValidateTemplate(ref detailAddition, template);
                break;
            case WeithingFactEntity weithingFact:
	            result = ValidateWeithingFact(ref detailAddition, weithingFact);
                break;
            case WorkShopEntity workshop:
	            result = ValidateWorkshop(ref detailAddition, workshop);
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
            notificationService.Notify(msg);
            return false;
        }
        return true;
    }

    private static bool ValidateAccess(ref string detailAddition, AccessEntity access)
    {
        if (access.EqualsDefault())
            return false;
        if (string.IsNullOrEmpty(access.User))
        {
            detailAddition += $"{LocaleCore.Table.FieldIsEmpty}: {LocaleCore.Table.FieldUser}" + Environment.NewLine;
            return false;
        }
        if (access.Rights > 3)
        {
            detailAddition += $"{LocaleCore.Table.FieldIsNotInRange}: {LocaleCore.Strings.AccessRights}" + Environment.NewLine;
            return false;
        }
        return true;
    }

    private static bool ValidateBarcodeType(ref string detailAddition, BarCodeTypeV2Entity barCodeType)
    {
        if (barCodeType.EqualsDefault())
            return false;
        if (string.IsNullOrEmpty(barCodeType.Name))
        {
            detailAddition += $"{LocaleCore.Table.FieldIsEmpty}: {LocaleCore.Table.FieldName}" + Environment.NewLine;
            return false;
        }
        return true; 
    }

    private static bool ValidateContragent(ref string detailAddition, ContragentV2Entity contragent)
    {
        if (contragent.EqualsDefault())
            return false;
        if (string.IsNullOrEmpty(contragent.Name))
        {
            detailAddition += $"{LocaleCore.Table.FieldIsEmpty}: {LocaleCore.Table.FieldName}" + Environment.NewLine;
            return false;
        }
        return true;
	}

    private static bool ValidateHost(ref string detailAddition, HostEntity host)
    {
        if (host.EqualsDefault())
            return false;
        if (string.IsNullOrEmpty(host.Name))
        {
            detailAddition += $"{LocaleCore.Table.FieldIsEmpty}: {LocaleCore.Table.FieldName}" + Environment.NewLine;
            return false;
        }
        //if (Equals(host.IdRRef, Guid.Empty))
        //{
        //    detailAddition += $"{LocaleCore.Table.FieldIsEmpty}: {LocaleCore.Table.FieldIdRRef}" + Environment.NewLine;
        //    return false;
        //}
        if (string.IsNullOrEmpty(host.Ip))
        {
            detailAddition += $"{LocaleCore.Table.FieldIsEmpty}: {LocaleCore.Table.FieldIpAddress}" + Environment.NewLine;
            return false;
        }
        return true;
	}

    private static bool ValidateLabel(ref string detailAddition, LabelEntity label)
    {
        if (label.EqualsDefault())
            return false;
        if (label.Label?.Length == 0)
        {
            detailAddition += $"{LocaleCore.Table.FieldIsEmpty}: {LocaleCore.Table.FieldLabel}" + Environment.NewLine;
            return false;
        }
        return true;
	}

    private static bool ValidateNomenclature(ref string detailAddition, NomenclatureEntity nomenclature)
    {
        if (nomenclature.EqualsDefault())
            return false;
        if (string.IsNullOrEmpty(nomenclature.Name))
        {
            detailAddition += $"{LocaleCore.Table.FieldIsEmpty}: {LocaleCore.Table.FieldName}" + Environment.NewLine;
            return false;
        }
        return true;
	}

    private static bool ValidateOrder(OrderEntity order)
    {
	    OrderValidator validator = new();
	    ValidationResult validationResult = validator.Validate(order);
		return validationResult.IsValid;
    }

    private bool ValidatePluObsolete(ref string detailAddition, PluObsoleteEntity pluObsolete)
    {
	    //PluObsoleteValidator validator = new();
	    //ValidationResult validationResult = validator.Validate(pluObsolete);
	    //if (!validationResult.IsValid)
		   // return;
	    if (pluObsolete.EqualsDefault())
            return false;
        PluObsoleteEntity[]? items = AppSettings.DataAccess.Crud.GetEntities<PluObsoleteEntity>(
            new(new() { new($"Scale.{DbField.IdentityId}", DbComparer.Equal, pluObsolete.Scale.IdentityId),
                new(DbField.PluNumber, DbComparer.Equal, pluObsolete.PluNumber)
            }));
        if (items != null && items.Any() && !items.Where(x => x.IdentityId.Equals(pluObsolete.IdentityId)).Select(x => x).Any())
        {
            detailAddition += $"{LocaleCore.Table.TablePluHavingPlu}: {pluObsolete.PluNumber}" + Environment.NewLine;
            return false;
        }
        return true;
    }

    private bool ValidatePluScale(ref string detailAddition, PluScaleEntity pluScale)
    {
	  //  PlusScaleValidator validator = new();
	  //  ValidationResult validationResult = validator.Validate(pluScale);
	  //  if (!validationResult.IsValid)
			//return false;
        
	    if (pluScale.EqualsDefault())
            return false;
        PluScaleEntity[]? items = AppSettings.DataAccess.Crud.GetEntities<PluScaleEntity>(
            new(new() { new($"Scale.{DbField.IdentityId}", DbComparer.Equal, pluScale.Scale.IdentityId),
                new(DbField.Number, DbComparer.Equal, pluScale.Plu.Number)
            }));
        if (items != null && items.Any() && !items.Where(x => x.IdentityId.Equals(pluScale.IdentityId)).Select(x => x).Any())
        {
            detailAddition += $"{LocaleCore.Table.TablePluHavingPlu}: {pluScale.Plu.Number}" + Environment.NewLine;
            return false;
        }
        return true;
    }

    private static bool ValidatePrinter(ref string detailAddition, PrinterEntity printer)
    {
        if (printer.EqualsDefault())
            return false;
        return true;
    }

    private static bool ValidatePrinterResource(ref string detailAddition, PrinterResourceEntity printerResource)
    {
        if (printerResource.EqualsDefault())
            return false;
        return true;
	}

    private static bool ValidatePrinterType(ref string detailAddition, PrinterTypeEntity printerType)
    {
        if (printerType.EqualsDefault())
            return false;
        if (string.IsNullOrEmpty(printerType.Name))
        {
            detailAddition += $"{LocaleCore.Table.FieldIsEmpty}: {LocaleCore.Table.FieldName}" + Environment.NewLine;
            return false;
        }
        return true;
	}

    private static bool ValidateProductionFacility(ref string detailAddition, ProductionFacilityEntity productionFacility)
    {
        if (productionFacility.EqualsDefault())
            return false;
        if (string.IsNullOrEmpty(productionFacility.Name))
        {
            detailAddition += $"{LocaleCore.Table.FieldIsEmpty}: {LocaleCore.Table.FieldName}" + Environment.NewLine;
            return false;
        }
        return true;
	}

    private static bool ValidateProductSeries(ref string detailAddition, ProductSeriesEntity productSeries)
    {
        if (productSeries.EqualsDefault())
            return false;
        return true;
	}

    private static bool ValidateScale(ref string detailAddition, ScaleEntity scale)
    {
        if (scale.EqualsDefault())
            return false;
        if (string.IsNullOrEmpty(scale.Description))
        {
            detailAddition += $"{LocaleCore.Table.FieldIsEmpty}: {LocaleCore.Table.FieldDescription}" + Environment.NewLine;
            return false;
        }
        return true;
	}

    private static bool ValidateTask(ref string detailAddition, TaskEntity task)
    {
        if (task.EqualsDefault())
            return false;
        return true;
	}

    private static bool ValidateTaskType(ref string detailAddition, TaskTypeEntity taskType)
    {
        if (taskType.EqualsDefault())
            return false;
        if (string.IsNullOrEmpty(taskType.Name))
        {
            detailAddition += $"{LocaleCore.Table.FieldIsEmpty}: {LocaleCore.Table.FieldName}" + Environment.NewLine;
            return false;
        }
        return true;
	}

    private static bool ValidateTemplateResource(ref string detailAddition, TemplateResourceEntity templateResource)
    {
        if (templateResource.EqualsDefault())
            return false;
        if (string.IsNullOrEmpty(templateResource.Name))
        {
            detailAddition += $"{LocaleCore.Table.FieldIsEmpty}: {LocaleCore.Table.FieldName}" + Environment.NewLine;
            return false;
        }
        return true;
	}

    private static bool ValidateTemplate(ref string detailAddition, TemplateEntity template)
    {
        if (template.EqualsDefault())
            return false;
        if (string.IsNullOrEmpty(template.Title))
        {
            detailAddition += $"{LocaleCore.Table.FieldIsEmpty}: {LocaleCore.Table.FieldTitle}" + Environment.NewLine;
            return false;
        }
        if (string.IsNullOrEmpty(template.CategoryId))
        {
            detailAddition += $"{LocaleCore.Table.FieldIsEmpty}: {LocaleCore.Table.FieldCategory}" + Environment.NewLine;
            return false;
        }
        return true;
	}

    private static bool ValidateWeithingFact(ref string detailAddition, WeithingFactEntity weithingFact)
    {
        if (weithingFact.EqualsDefault())
            return false;
        return true;
	}

    private static bool ValidateWorkshop(ref string detailAddition, WorkShopEntity workshop)
    {
        if (workshop.EqualsDefault())
            return false;
        if (string.IsNullOrEmpty(workshop.Name))
        {
            detailAddition += $"{LocaleCore.Table.FieldIsEmpty}: {LocaleCore.Table.FieldName}" + Environment.NewLine;
            return false;
        }
        return true;
	}

    #endregion
}
