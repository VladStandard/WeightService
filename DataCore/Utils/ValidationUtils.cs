// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.CssStyles;
using DataCore.Sql.Tables;
using FluentValidation;
using FluentValidation.Results;

namespace DataCore.Utils;

public class ValidationUtils
{
	private static void SetValidationFailureLog(ValidationResult result, ref string detailAddition)
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

	public static IValidator GetSqlValidator<T>() where T : SqlTableBase, new() =>
		typeof(T) switch
		{
			var cls when cls == typeof(AccessModel) => new AccessValidator(),
			var cls when cls == typeof(AppModel) => new AppValidator(),
			var cls when cls == typeof(BarCodeModel) => new BarCodeValidator(),
			var cls when cls == typeof(ContragentModel) => new ContragentValidator(),
			var cls when cls == typeof(DeviceModel) => new DeviceValidator(),
			var cls when cls == typeof(DeviceScaleFkModel) => new DeviceScaleFkValidator(),
			var cls when cls == typeof(DeviceTypeFkModel) => new DeviceTypeFkValidator(),
			var cls when cls == typeof(DeviceTypeModel) => new DeviceTypeValidator(),
			var cls when cls == typeof(LogModel) => new LogValidator(),
			var cls when cls == typeof(LogTypeModel) => new LogTypeValidator(),
			var cls when cls == typeof(NomenclatureModel) => new NomenclatureValidator(),
			var cls when cls == typeof(OrderModel) => new OrderValidator(),
			var cls when cls == typeof(OrderWeighingModel) => new OrderWeighingValidator(),
			var cls when cls == typeof(OrganizationModel) => new OrganizationValidator(),
			var cls when cls == typeof(PackageModel) => new PackageValidator(),
			var cls when cls == typeof(PluLabelModel) => new PluLabelValidator(),
			var cls when cls == typeof(PluModel) => new PluValidator(),
			var cls when cls == typeof(PluScaleModel) => new PluScaleValidator(),
			var cls when cls == typeof(PluWeighingModel) => new PluWeighingValidator(),
			var cls when cls == typeof(PrinterModel) => new PrinterValidator(),
			var cls when cls == typeof(PrinterResourceModel) => new PrinterResourceValidator(),
			var cls when cls == typeof(PrinterTypeModel) => new PrinterTypeValidator(),
			var cls when cls == typeof(ProductionFacilityModel) => new ProductionFacilityValidator(),
			var cls when cls == typeof(ProductSeriesModel) => new ProductSeriesValidator(),
			var cls when cls == typeof(ScaleModel) => new ScaleValidator(),
			var cls when cls == typeof(TaskModel) => new TaskValidator(),
			var cls when cls == typeof(TaskTypeModel) => new TaskTypeValidator(),
			var cls when cls == typeof(TemplateModel) => new TemplateValidator(),
			var cls when cls == typeof(TemplateResourceModel) => new TemplateResourceValidator(),
			var cls when cls == typeof(VersionModel) => new VersionValidator(),
			var cls when cls == typeof(WorkShopModel) => new WorkShopValidator(),
			_ => throw new NotImplementedException()
		};

	public static ValidationResult GetValidationResult<T>(T? item) where T : class, new() =>
        item switch
        {
            // CssStyle
            CssStyleRadzenColumnModel cssStyleRadzenColumn => new CssStyleRadzenColumnValidator().Validate(cssStyleRadzenColumn),
            CssStyleTableBodyModel cssStyleTableBody => new CssStyleTableBodyValidator().Validate(cssStyleTableBody),
            CssStyleTableHeadModel cssStyleTableHead => new CssStyleTableHeadValidator().Validate(cssStyleTableHead),
            // SqlTable
            AccessModel access => new AccessValidator().Validate(access),
            AppModel app => new AppValidator().Validate(app),
            BarCodeModel barCode => new BarCodeValidator().Validate(barCode),
            ContragentModel contragent => new ContragentValidator().Validate(contragent),
            LogModel log => new LogValidator().Validate(log),
            LogTypeModel logType => new LogTypeValidator().Validate(logType),
            NomenclatureModel nomenclature => new NomenclatureValidator().Validate(nomenclature),
            OrderModel order => new OrderValidator().Validate(order),
            OrderWeighingModel orderWeighing => new OrderWeighingValidator().Validate(orderWeighing),
            OrganizationModel organization => new OrganizationValidator().Validate(organization),
            PackageModel package => new PackageValidator().Validate(package),
            PluLabelModel pluLabel => new PluLabelValidator().Validate(pluLabel),
            PluModel plu => new PluValidator().Validate(plu),
            PluPackageModel package => new PluPackageValidator().Validate(package),
            PluScaleModel pluScale => new PluScaleValidator().Validate(pluScale),
            PluWeighingModel pluWeighing => new PluWeighingValidator().Validate(pluWeighing),
            PrinterModel printer => new PrinterValidator().Validate(printer),
            PrinterResourceModel printerResource => new PrinterResourceValidator().Validate(printerResource),
            PrinterTypeModel printerType => new PrinterTypeValidator().Validate(printerType),
            ProductionFacilityModel productionFacility => new ProductionFacilityValidator().Validate(productionFacility),
            ProductSeriesModel productSeries => new ProductSeriesValidator().Validate(productSeries),
            ScaleModel scale => new ScaleValidator().Validate(scale),
            ScaleScreenShotModel scaleScreenShot => new ScaleScreenShotValidator().Validate(scaleScreenShot),
            TaskModel task => new TaskValidator().Validate(task),
            TaskTypeModel taskType => new TaskTypeValidator().Validate(taskType),
            TemplateModel template => new TemplateValidator().Validate(template),
            TemplateResourceModel templateResource => new TemplateResourceValidator().Validate(templateResource),
            VersionModel version => new VersionValidator().Validate(version),
            WorkShopModel workShop => new WorkShopValidator().Validate(workShop),
			DeviceModel device => new DeviceValidator().Validate(device),
			DeviceScaleFkModel deviceScaleFk => new DeviceScaleFkValidator().Validate(deviceScaleFk),
			DeviceTypeFkModel deviceTypeFk => new DeviceTypeFkValidator().Validate(deviceTypeFk),
			DeviceTypeModel deviceType => new DeviceTypeValidator().Validate(deviceType),
            _ => throw new NullReferenceException(nameof(item))
        };

    public static bool IsValidation<T>(T? item, ref string detailAddition) where T : class, new()
	{
		if (item is null)
		{
			detailAddition = $"{nameof(item)} is null!";
			return false;
		}

		ValidationResult validationResult = GetValidationResult(item);
		if (!validationResult.IsValid)
		{
			SetValidationFailureLog(validationResult, ref detailAddition);
			return false;
		}

		return true;
	}
}
