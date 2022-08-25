// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using FluentValidation.Results;

namespace DataCore.Sql.Models;

public class SqlBaseUtils
{
	private static void FailureLog(ValidationResult result, ref string detailAddition)
	{
		switch (result.IsValid)
		{
			case false:
			{
				foreach (ValidationFailure failure in result.Errors)
				{
					detailAddition += $"{LocaleCore.Validator.Property} {failure.PropertyName} {LocaleCore.Validator.FailedValidation}. {LocaleCore.Validator.Error}: {failure.ErrorMessage}";
				}
				break;
			}
		}
	}
	
	public static bool IsValidation(BaseEntity item, ref string detailAddition)
	{
		ValidationResult validationResult;
		switch (item)
		{
			case AccessEntity access:
				validationResult = new AccessValidator().Validate(access);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (access.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case BarCodeTypeEntity barCodeType:
				validationResult = new BarCodeTypeValidator().Validate(barCodeType);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (barCodeType.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case BarCodeEntity barCode:
				validationResult = new BarCodeValidator().Validate(barCode);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (barCode.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case ContragentEntity contragent:
				validationResult = new ContragentValidator().Validate(contragent);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (contragent.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case HostEntity host:
				validationResult = new HostValidator().Validate(host);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (host.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case LogEntity log:
				validationResult = new LogValidator().Validate(log);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (log.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case LogTypeEntity logType:
				validationResult = new LogTypeValidator().Validate(logType);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (logType.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case NomenclatureEntity nomenclature:
				validationResult = new NomenclatureValidator().Validate(nomenclature);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (nomenclature.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case OrderEntity order:
				validationResult = new OrderValidator().Validate(order);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (order.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case OrderWeighingEntity orderWeighing:
				validationResult = new OrderWeighingValidator().Validate(orderWeighing);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (orderWeighing.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case PluEntity plu:
				validationResult = new PluValidator().Validate(plu);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (plu.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case PluLabelEntity pluLabel:
				validationResult = new PluLabelValidator().Validate(pluLabel);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (pluLabel.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case PluObsoleteEntity pluObsolete:
				if (pluObsolete.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case PluScaleEntity pluScale:
				validationResult = new PluScaleValidator().Validate(pluScale);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (pluScale.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case PluWeighingEntity pluWeighing:
				validationResult = new PluWeighingValidator().Validate(pluWeighing);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (pluWeighing.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case PrinterEntity printer:
				validationResult = new PrinterValidator().Validate(printer);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (printer.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case PrinterResourceEntity printerResource:
				validationResult = new PrinterValidator().Validate(printerResource);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (printerResource.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case PrinterTypeEntity printerType:
				validationResult = new PrinterTypeValidator().Validate(printerType);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (printerType.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case ProductionFacilityEntity productionFacility:
				validationResult = new ProductionFacilityValidator().Validate(productionFacility);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (productionFacility.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case ProductSeriesEntity productSeries:
				validationResult = new ProductSeriesValidator().Validate(productSeries);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (productSeries.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case ScaleEntity scale:
				validationResult = new ScaleValidator().Validate(scale);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (scale.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case TemplateEntity template:
				validationResult = new TemplateValidator().Validate(template);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (template.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case TemplateResourceEntity templateResource:
				validationResult = new TemplateResourceValidator().Validate(templateResource);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (templateResource.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case VersionEntity version:
				validationResult = new VersionValidator().Validate(version);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (version.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case WorkShopEntity workShop:
				validationResult = new WorkShopValidator().Validate(workShop);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (workShop.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
		}
		return true;
	}


}
