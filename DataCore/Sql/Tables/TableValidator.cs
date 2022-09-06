// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core;
using FluentValidation.Results;

namespace DataCore.Sql.Tables;

/// <summary>
/// Table validation.
/// </summary>
public class TableValidator : AbstractValidator<TableBaseModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    protected TableValidator(bool isCheckCreateDt = true, bool isCheckChangeDt = true)
    {
	    RuleFor(item => item.Identity).SetValidator(new FieldIdentityValidator());
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

    protected bool PreValidateSubEntity<T>(T? item, ValidationResult result) where T : TableBaseModel, new()
    {
        if (item != null)
        {
	        IValidator<T> validator = SqlUtils.GetSqlValidator(item);
	        result = validator.Validate(item);
			if (!result.IsValid) return result.IsValid;
			//switch (item)
   //         {
   //             case AccessModel access:
   //                 result = new AccessValidator().Validate(access);
   //                 if (!result.IsValid) return result.IsValid;
   //                 break;
   //             case AppModel app:
   //                 result = new AppValidator().Validate(app);
   //                 if (!result.IsValid) return result.IsValid;
   //                 break;
   //             case BarCodeModel barCode:
   //                 result = new BarCodeValidator().Validate(barCode);
   //                 if (!result.IsValid)
   //                     return false;
   //                 break;
   //             case BarCodeTypeModel barCodeType:
   //                 result = new BarCodeTypeValidator().Validate(barCodeType);
   //                 if (!result.IsValid)
   //                     return false;
   //                 break;
   //             case ContragentModel contragent:
   //                 result = new ContragentValidator().Validate(contragent);
   //                 if (!result.IsValid)
   //                     return false;
   //                 break;
   //             case HostModel host:
   //                 result = new HostValidator().Validate(host);
   //                 if (!result.IsValid)
   //                     return false;
   //                 break;
   //             case LogModel log:
   //                 result = new LogValidator().Validate(log);
   //                 if (!result.IsValid)
   //                     return false;
   //                 break;
   //             case LogTypeModel logType:
   //                 result = new LogTypeValidator().Validate(logType);
   //                 if (!result.IsValid)
   //                     return false;
   //                 break;
   //             case NomenclatureModel nomenclature:
   //                 result = new NomenclatureValidator().Validate(nomenclature);
   //                 if (!result.IsValid)
   //                     return false;
   //                 break;
   //             case OrderModel order:
   //                 result = new OrderValidator().Validate(order);
   //                 if (!result.IsValid)
   //                     return false;
   //                 break;
   //             case OrderWeighingModel orderWeighing:
   //                 result = new OrderWeighingValidator().Validate(orderWeighing);
   //                 if (!result.IsValid)
   //                     return false;
   //                 break;
   //             case OrganizationModel organization:
   //                 result = new OrganizationValidator().Validate(organization);
   //                 if (!result.IsValid)
   //                     return false;
   //                 break;
   //             case PluModel plu:
   //                 result = new PluValidator().Validate(plu);
   //                 if (!result.IsValid)
   //                     return false;
   //                 break;
   //             case PluObsoleteModel pluObsolete:
   //                 result = new PluObsoleteValidator().Validate(pluObsolete);
   //                 if (!result.IsValid)
   //                     return false;
   //                 break;
   //             case PluLabelModel pluLabel:
   //                 result = new PluLabelValidator().Validate(pluLabel);
   //                 if (!result.IsValid)
   //                     return false;
   //                 break;
   //             case PluScaleModel pluScale:
   //                 result = new PluScaleValidator().Validate(pluScale);
   //                 if (!result.IsValid)
   //                     return false;
   //                 break;
   //             case PluWeighingModel pluWeighing:
   //                 result = new PluWeighingValidator().Validate(pluWeighing);
   //                 if (!result.IsValid)
   //                     return false;
   //                 break;
   //             case PrinterModel printer:
   //                 result = new PrinterValidator().Validate(printer);
   //                 if (!result.IsValid)
   //                     return false;
   //                 break;
   //             case PrinterResourceModel printerResource:
   //                 result = new PrinterResourceValidator().Validate(printerResource);
   //                 if (!result.IsValid)
   //                     return false;
   //                 break;
   //             case PrinterTypeModel printerType:
   //                 result = new PrinterTypeValidator().Validate(printerType);
   //                 if (!result.IsValid)
   //                     return false;
   //                 break;
   //             case ProductionFacilityModel productionFacility:
   //                 result = new ProductionFacilityValidator().Validate(productionFacility);
   //                 if (!result.IsValid)
   //                     return false;
   //                 break;
   //             case ProductSeriesModel productSeries:
   //                 result = new ProductSeriesValidator().Validate(productSeries);
   //                 if (!result.IsValid)
   //                     return false;
   //                 break;
   //             case ScaleModel scale:
   //                 result = new ScaleValidator().Validate(scale);
   //                 if (!result.IsValid)
   //                     return false;
   //                 break;
   //             case TaskModel task:
   //                 result = new TaskValidator().Validate(task);
   //                 if (!result.IsValid)
   //                     return false;
   //                 break;
   //             case TaskTypeModel taskType:
   //                 result = new TaskTypeValidator().Validate(taskType);
   //                 if (!result.IsValid)
   //                     return false;
   //                 break;
   //             case TemplateModel template:
   //                 result = new TemplateValidator().Validate(template);
   //                 if (!result.IsValid)
   //                     return false;
   //                 break;
   //             case TemplateResourceModel templateResource:
   //                 result = new TemplateResourceValidator().Validate(templateResource);
   //                 if (!result.IsValid)
   //                     return false;
   //                 break;
   //             case VersionModel version:
   //                 result = new VersionValidator().Validate(version);
   //                 if (!result.IsValid)
   //                     return false;
   //                 break;
   //             case WorkShopModel workShop:
   //                 result = new WorkShopValidator().Validate(workShop);
   //                 if (!result.IsValid)
   //                     return false;
   //                 break;
   //         }
        }
        return result.IsValid;
    }
}
