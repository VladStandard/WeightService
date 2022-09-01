// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using FluentValidation.Results;
using static DataCore.ShareEnums;

namespace DataCore.Sql.Core;

public static partial class SqlUtils
{
	#region Public and private fields and properties

	public static SqlConnectFactory SqlConnect { get; } = SqlConnectFactory.Instance;
	public static DataAccessHelper DataAccess { get; } = DataAccessHelper.Instance;
	public static readonly string FilePathToken = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\scalesui.xml";
	public static readonly string FilePathLog = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\scalesui.log";

	#endregion

	#region Public and private methods

	private static void FailureLog(ValidationResult result, ref string detailAddition)
	{
		switch (result.IsValid)
		{
			case false:
				foreach (ValidationFailure failure in result.Errors)
				{
					detailAddition += $"{LocaleCore.Validator.Property} {failure.PropertyName} {LocaleCore.Validator.FailedValidation}. {LocaleCore.Validator.Error}: {failure.ErrorMessage}";
				}
				break;
		}
	}

	public static IValidator<T> GetSqlValidator<T>(T? item) where T : TableModel, new()
	{
		return item switch
		{
			AccessModel => new AccessValidator(),
			AppModel => new AppValidator(),
			BarCodeModel => new BarCodeValidator(),
			BarCodeTypeModel => new BarCodeTypeValidator(),
			ContragentModel => new ContragentValidator(),
			HostModel => new HostValidator(),
			LogModel => new LogValidator(),
			LogTypeModel => new LogTypeValidator(),
			NomenclatureModel => new NomenclatureValidator(),
			OrderModel => new OrderValidator(),
			OrderWeighingModel => new OrderWeighingValidator(),
			OrganizationModel => new OrganizationValidator(),
			PluModel => new PluValidator(),
			PluLabelModel => new PluLabelValidator(),
			PluObsoleteModel => new PluObsoleteValidator(),
			PluScaleModel => new PluScaleValidator(),
			PluWeighingModel => new PluWeighingValidator(),
			PrinterModel => new PrinterValidator(),
			PrinterResourceModel => new PrinterResourceValidator(),
			PrinterTypeModel => new PrinterTypeValidator(),
			ProductionFacilityModel => new ProductionFacilityValidator(),
			ProductSeriesModel => new ProductSeriesValidator(),
			ScaleModel => new ScaleValidator(),
			VersionModel => new VersionValidator(),
			TaskModel => new TaskValidator(),
			TaskTypeModel => new TaskTypeValidator(),
			TemplateModel => new TemplateValidator(),
			TemplateResourceModel => new TemplateResourceValidator(),
			WorkShopModel => new WorkShopValidator(),
			_ => throw new NotImplementedException()
		};
	}

	public static bool IsValidation<T>(T? item, ref string detailAddition) where T : TableModel, new()
	{
		if (item is null)
		{
			detailAddition = $"{nameof(item)} is null!";
			return false;
		}

		IValidator<T> validator = GetSqlValidator(item);
		ValidationResult validationResult = validator.Validate(item);
		if (!validationResult.IsValid)
		{
			FailureLog(validationResult, ref detailAddition);
			return false;
		}

		//switch (item)
		//{
		//	case AccessModel access:
		//		validationResult = validator.Validate(item);
		//		if (!validationResult.IsValid)
		//		{
		//			FailureLog(validationResult, ref detailAddition);
		//			return false;
		//		}
		//		if (access.EqualsDefault())
		//		{
		//			detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
		//			return false;
		//		}
		//		break;
		//	case BarCodeTypeModel barCodeType:
		//		validationResult = new BarCodeTypeValidator().Validate(barCodeType);
		//		if (!validationResult.IsValid)
		//		{
		//			FailureLog(validationResult, ref detailAddition);
		//			return false;
		//		}
		//		if (barCodeType.EqualsDefault())
		//		{
		//			detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
		//			return false;
		//		}
		//		break;
		//	case BarCodeModel barCode:
		//		validationResult = new BarCodeValidator().Validate(barCode);
		//		if (!validationResult.IsValid)
		//		{
		//			FailureLog(validationResult, ref detailAddition);
		//			return false;
		//		}
		//		if (barCode.EqualsDefault())
		//		{
		//			detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
		//			return false;
		//		}
		//		break;
		//	case ContragentModel contragent:
		//		validationResult = new ContragentValidator().Validate(contragent);
		//		if (!validationResult.IsValid)
		//		{
		//			FailureLog(validationResult, ref detailAddition);
		//			return false;
		//		}
		//		if (contragent.EqualsDefault())
		//		{
		//			detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
		//			return false;
		//		}
		//		break;
		//	case HostModel host:
		//		validationResult = new HostValidator().Validate(host);
		//		if (!validationResult.IsValid)
		//		{
		//			FailureLog(validationResult, ref detailAddition);
		//			return false;
		//		}
		//		if (host.EqualsDefault())
		//		{
		//			detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
		//			return false;
		//		}
		//		break;
		//	case LogModel log:
		//		validationResult = new LogValidator().Validate(log);
		//		if (!validationResult.IsValid)
		//		{
		//			FailureLog(validationResult, ref detailAddition);
		//			return false;
		//		}
		//		if (log.EqualsDefault())
		//		{
		//			detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
		//			return false;
		//		}
		//		break;
		//	case LogTypeModel logType:
		//		validationResult = new LogTypeValidator().Validate(logType);
		//		if (!validationResult.IsValid)
		//		{
		//			FailureLog(validationResult, ref detailAddition);
		//			return false;
		//		}
		//		if (logType.EqualsDefault())
		//		{
		//			detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
		//			return false;
		//		}
		//		break;
		//	case NomenclatureModel nomenclature:
		//		validationResult = new NomenclatureValidator().Validate(nomenclature);
		//		if (!validationResult.IsValid)
		//		{
		//			FailureLog(validationResult, ref detailAddition);
		//			return false;
		//		}
		//		if (nomenclature.EqualsDefault())
		//		{
		//			detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
		//			return false;
		//		}
		//		break;
		//	case OrderModel order:
		//		validationResult = new OrderValidator().Validate(order);
		//		if (!validationResult.IsValid)
		//		{
		//			FailureLog(validationResult, ref detailAddition);
		//			return false;
		//		}
		//		if (order.EqualsDefault())
		//		{
		//			detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
		//			return false;
		//		}
		//		break;
		//	case OrderWeighingModel orderWeighing:
		//		validationResult = new OrderWeighingValidator().Validate(orderWeighing);
		//		if (!validationResult.IsValid)
		//		{
		//			FailureLog(validationResult, ref detailAddition);
		//			return false;
		//		}
		//		if (orderWeighing.EqualsDefault())
		//		{
		//			detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
		//			return false;
		//		}
		//		break;
		//	case PluModel plu:
		//		validationResult = new PluValidator().Validate(plu);
		//		if (!validationResult.IsValid)
		//		{
		//			FailureLog(validationResult, ref detailAddition);
		//			return false;
		//		}
		//		if (plu.EqualsDefault())
		//		{
		//			detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
		//			return false;
		//		}
		//		break;
		//	case PluLabelModel pluLabel:
		//		validationResult = new PluLabelValidator().Validate(pluLabel);
		//		if (!validationResult.IsValid)
		//		{
		//			FailureLog(validationResult, ref detailAddition);
		//			return false;
		//		}
		//		if (pluLabel.EqualsDefault())
		//		{
		//			detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
		//			return false;
		//		}
		//		break;
		//	case PluObsoleteModel pluObsolete:
		//		if (pluObsolete.EqualsDefault())
		//		{
		//			detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
		//			return false;
		//		}
		//		break;
		//	case PluScaleModel pluScale:
		//		validationResult = new PluScaleValidator().Validate(pluScale);
		//		if (!validationResult.IsValid)
		//		{
		//			FailureLog(validationResult, ref detailAddition);
		//			return false;
		//		}
		//		if (pluScale.EqualsDefault())
		//		{
		//			detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
		//			return false;
		//		}
		//		break;
		//	case PluWeighingModel pluWeighing:
		//		validationResult = new PluWeighingValidator().Validate(pluWeighing);
		//		if (!validationResult.IsValid)
		//		{
		//			FailureLog(validationResult, ref detailAddition);
		//			return false;
		//		}
		//		if (pluWeighing.EqualsDefault())
		//		{
		//			detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
		//			return false;
		//		}
		//		break;
		//	case PrinterModel printer:
		//		validationResult = new PrinterValidator().Validate(printer);
		//		if (!validationResult.IsValid)
		//		{
		//			FailureLog(validationResult, ref detailAddition);
		//			return false;
		//		}
		//		if (printer.EqualsDefault())
		//		{
		//			detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
		//			return false;
		//		}
		//		break;
		//	case PrinterResourceModel printerResource:
		//		validationResult = new PrinterValidator().Validate(printerResource);
		//		if (!validationResult.IsValid)
		//		{
		//			FailureLog(validationResult, ref detailAddition);
		//			return false;
		//		}
		//		if (printerResource.EqualsDefault())
		//		{
		//			detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
		//			return false;
		//		}
		//		break;
		//	case PrinterTypeModel printerType:
		//		validationResult = new PrinterTypeValidator().Validate(printerType);
		//		if (!validationResult.IsValid)
		//		{
		//			FailureLog(validationResult, ref detailAddition);
		//			return false;
		//		}
		//		if (printerType.EqualsDefault())
		//		{
		//			detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
		//			return false;
		//		}
		//		break;
		//	case ProductionFacilityModel productionFacility:
		//		validationResult = new ProductionFacilityValidator().Validate(productionFacility);
		//		if (!validationResult.IsValid)
		//		{
		//			FailureLog(validationResult, ref detailAddition);
		//			return false;
		//		}
		//		if (productionFacility.EqualsDefault())
		//		{
		//			detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
		//			return false;
		//		}
		//		break;
		//	case ProductSeriesModel productSeries:
		//		validationResult = new ProductSeriesValidator().Validate(productSeries);
		//		if (!validationResult.IsValid)
		//		{
		//			FailureLog(validationResult, ref detailAddition);
		//			return false;
		//		}
		//		if (productSeries.EqualsDefault())
		//		{
		//			detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
		//			return false;
		//		}
		//		break;
		//	case ScaleModel scale:
		//		validationResult = new ScaleValidator().Validate(scale);
		//		if (!validationResult.IsValid)
		//		{
		//			FailureLog(validationResult, ref detailAddition);
		//			return false;
		//		}
		//		if (scale.EqualsDefault())
		//		{
		//			detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
		//			return false;
		//		}
		//		break;
		//	case TemplateModel template:
		//		validationResult = new TemplateValidator().Validate(template);
		//		if (!validationResult.IsValid)
		//		{
		//			FailureLog(validationResult, ref detailAddition);
		//			return false;
		//		}
		//		if (template.EqualsDefault())
		//		{
		//			detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
		//			return false;
		//		}
		//		break;
		//	case TemplateResourceModel templateResource:
		//		validationResult = new TemplateResourceValidator().Validate(templateResource);
		//		if (!validationResult.IsValid)
		//		{
		//			FailureLog(validationResult, ref detailAddition);
		//			return false;
		//		}
		//		if (templateResource.EqualsDefault())
		//		{
		//			detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
		//			return false;
		//		}
		//		break;
		//	case VersionModel version:
		//		validationResult = new VersionValidator().Validate(version);
		//		if (!validationResult.IsValid)
		//		{
		//			FailureLog(validationResult, ref detailAddition);
		//			return false;
		//		}
		//		if (version.EqualsDefault())
		//		{
		//			detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
		//			return false;
		//		}
		//		break;
		//	case WorkShopModel workShop:
		//		validationResult = new WorkShopValidator().Validate(workShop);
		//		if (!validationResult.IsValid)
		//		{
		//			FailureLog(validationResult, ref detailAddition);
		//			return false;
		//		}
		//		if (workShop.EqualsDefault())
		//		{
		//			detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
		//			return false;
		//		}
		//		break;
		//}
		return true;
	}

	public static SqlCrudConfigModel GetCrudConfigIsMarked() => GetCrudConfig(null, null, 0, false, false);

	public static SqlCrudConfigModel GetCrudConfig(List<FieldFilterModel>? filters, FieldOrderModel? order, int maxResults, bool isShowMarked, bool isShowOnlyTop)
	{
		maxResults = isShowOnlyTop ? DataAccess.JsonSettingsLocal.SelectTopRowsCount : maxResults;
		SqlCrudConfigModel sqlCrudConfig = new(filters, order, maxResults);
		List<FieldFilterModel> filtersMarked = new() { new(DbField.IsMarked, DbComparer.Equal, false) };
		if (!isShowMarked)
		{
			switch (sqlCrudConfig.Filters)
			{
				case null:
					sqlCrudConfig.Filters = filtersMarked;
					break;
				default:
					sqlCrudConfig.Filters.AddRange(filtersMarked);
					break;
			}
		}

		return sqlCrudConfig;
	}

	#endregion
}
