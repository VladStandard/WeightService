// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using FluentValidation.Results;
using static DataCore.Sql.SqlQueries.DbScales.Tables;

namespace DataCore.Sql.Models;

/// <summary>
/// Table validation.
/// </summary>
public class BaseValidator : AbstractValidator<BaseEntity>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    protected BaseValidator(ColumnName columnName, bool isCheckCreateDt = true, bool isCheckChangeDt = true)
    {
	    switch (columnName)
	    {
		    case ColumnName.Id:
			    RuleFor(item => item.IdentityId)
				    .NotEmpty()
				    .NotNull()
				    .NotEqual(0);
			    break;
		    case ColumnName.Uid:
				RuleFor(item => item.IdentityUid)
					.NotEmpty()
					.NotNull()
					.NotEqual(Guid.Empty);
				break;
		    default:
			    throw new ArgumentOutOfRangeException(nameof(columnName), columnName, null);
	    }
		if (isCheckCreateDt)
		    RuleFor(item => item.CreateDt)
			    .NotEmpty()
			    .NotNull()
			    .GreaterThanOrEqualTo(new DateTime(2020, 01, 01));
		if (isCheckChangeDt)
		    RuleFor(item => item.ChangeDt)
			    .NotEmpty()
			    .NotNull()
			    .GreaterThanOrEqualTo(new DateTime(2020, 01, 01));
    }

	protected bool PreValidateSubEntity<T>(T? item, ValidationResult result) where T : BaseEntity, new()
    {
	    if (item != null)
	    {
			switch (item)
		    {
				case AccessEntity access:
				    result = new AccessValidator().Validate(access);
				    if (!result.IsValid)
					    return false;
					break;
				case AppEntity app:
				    result = new AppValidator().Validate(app);
				    if (!result.IsValid)
					    return false;
					break;
				case BarCodeEntity barCode:
				    result = new BarCodeValidator().Validate(barCode);
				    if (!result.IsValid)
					    return false;
					break;
				case BarCodeTypeEntity barCodeType:
				    result = new BarCodeTypeValidator().Validate(barCodeType);
				    if (!result.IsValid)
					    return false;
					break;
				case ContragentEntity contragent:
				    result = new ContragentValidator().Validate(contragent);
				    if (!result.IsValid)
					    return false;
					break;
				case HostEntity host:
				    result = new HostValidator().Validate(host);
				    if (!result.IsValid)
					    return false;
					break;
				case LogEntity log:
					result = new LogValidator().Validate(log);
					if (!result.IsValid)
						return false;
					break;
				case LogTypeEntity logType:
					result = new LogTypeValidator().Validate(logType);
					if (!result.IsValid)
						return false;
					break;
				case NomenclatureEntity nomenclature:
					result = new NomenclatureValidator().Validate(nomenclature);
					if (!result.IsValid)
						return false;
					break;
				case OrderEntity order:
					result = new OrderValidator().Validate(order);
					if (!result.IsValid)
						return false;
					break;
				case OrderWeighingEntity orderWeighing:
					result = new OrderWeighingValidator().Validate(orderWeighing);
					if (!result.IsValid)
						return false;
					break;
				case OrganizationEntity organization:
					result = new OrganizationValidator().Validate(organization);
					if (!result.IsValid)
						return false;
					break;
				case PluEntity plu:
					result = new PluValidator().Validate(plu);
					if (!result.IsValid)
						return false;
					break;
				case PluObsoleteEntity pluObsolete:
					result = new PluObsoleteValidator().Validate(pluObsolete);
					if (!result.IsValid)
						return false;
					break;
				case PluLabelEntity pluLabel:
					result = new PluLabelValidator().Validate(pluLabel);
					if (!result.IsValid)
						return false;
					break;
				case PluScaleEntity pluScale:
					result = new PluScaleValidator().Validate(pluScale);
					if (!result.IsValid)
						return false;
					break;
				case PluWeighingEntity pluWeighing:
					result = new PluWeighingValidator().Validate(pluWeighing);
					if (!result.IsValid)
						return false;
					break;
				case PrinterEntity printer:
					result = new PrinterValidator().Validate(printer);
					if (!result.IsValid)
						return false;
					break;
				case PrinterResourceEntity printerResource:
					result = new PrinterResourceValidator().Validate(printerResource);
					if (!result.IsValid)
						return false;
					break;
				case PrinterTypeEntity printerType:
					result = new PrinterTypeValidator().Validate(printerType);
					if (!result.IsValid)
						return false;
					break;
				case ProductionFacilityEntity productionFacility:
					result = new ProductionFacilityValidator().Validate(productionFacility);
					if (!result.IsValid)
						return false;
					break;
				case ProductSeriesEntity productSeries:
					result = new ProductSeriesValidator().Validate(productSeries);
					if (!result.IsValid)
						return false;
					break;
				case ScaleEntity scale:
					result = new ScaleValidator().Validate(scale);
					if (!result.IsValid)
						return false;
					break;
				case TaskEntity task:
					result = new TaskValidator().Validate(task);
					if (!result.IsValid)
						return false;
					break;
				case TaskTypeEntity taskType:
					result = new TaskTypeValidator().Validate(taskType);
					if (!result.IsValid)
						return false;
					break;
				case TemplateEntity template:
				    result = new TemplateValidator().Validate(template);
				    if (!result.IsValid)
					    return false;
					break;
				case TemplateResourceEntity templateResource:
				    result = new TemplateResourceValidator().Validate(templateResource);
				    if (!result.IsValid)
					    return false;
					break;
				case VersionEntity version:
				    result = new VersionValidator().Validate(version);
				    if (!result.IsValid)
					    return false;
					break;
				case WorkShopEntity workShop:
					result = new WorkShopValidator().Validate(workShop);
					if (!result.IsValid)
						return false;
					break;
		    }
	    }
	    return result.IsValid;
    }
}
