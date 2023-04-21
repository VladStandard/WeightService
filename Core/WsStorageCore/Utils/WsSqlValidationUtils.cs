// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Utils;

public class WsSqlValidationUtils
{
    #region Public and private methods

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

    public static IValidator GetSqlValidator<T>() where T : WsSqlTableBase, new() =>
        typeof(T) switch
        {
            var cls when cls == typeof(WsSqlAccessModel) => new WsSqlAccessValidator(),
            var cls when cls == typeof(WsSqlAppModel) => new WsSqlAppValidator(),
            var cls when cls == typeof(BarCodeModel) => new BarCodeValidator(),
            var cls when cls == typeof(BoxModel) => new BoxValidator(),
            var cls when cls == typeof(BrandModel) => new BrandValidator(),
            var cls when cls == typeof(BundleModel) => new BundleValidator(),
            var cls when cls == typeof(ClipModel) => new ClipValidator(),
            var cls when cls == typeof(ContragentModel) => new ContragentValidator(),
            var cls when cls == typeof(DeviceModel) => new DeviceValidator(),
            var cls when cls == typeof(DeviceScaleFkModel) => new DeviceScaleFkValidator(),
            var cls when cls == typeof(DeviceTypeFkModel) => new DeviceTypeFkValidator(),
            var cls when cls == typeof(DeviceTypeModel) => new DeviceTypeValidator(),
            var cls when cls == typeof(LogModel) => new LogValidator(),
            var cls when cls == typeof(LogMemoryModel) => new LogMemoryValidator(),
            var cls when cls == typeof(LogTypeModel) => new LogTypeValidator(),
            var cls when cls == typeof(LogWebFkModel) => new LogWebFkValidator(),
            var cls when cls == typeof(LogWebModel) => new LogWebValidator(),
            var cls when cls == typeof(OrderModel) => new OrderValidator(),
            var cls when cls == typeof(OrderWeighingModel) => new OrderWeighingValidator(),
            var cls when cls == typeof(OrganizationModel) => new OrganizationValidator(),
            var cls when cls == typeof(PluBundleFkModel) => new PluBundleFkValidator(),
            var cls when cls == typeof(PluCharacteristicModel) => new PluCharacteristicValidator(),
            var cls when cls == typeof(PluCharacteristicsFkModel) => new PluCharacteristicsFkValidator(),
            var cls when cls == typeof(PluClipFkModel) => new PluClipFkValidator(),
            var cls when cls == typeof(PluGroupModel) => new PluGroupValidator(),
            var cls when cls == typeof(PluLabelModel) => new PluLabelValidator(),
            var cls when cls == typeof(PluModel) => new PluValidator(),
            var cls when cls == typeof(PluNestingFkModel) => new PluNestingFkValidator(),
            var cls when cls == typeof(PluScaleModel) => new PluScaleValidator(),
            var cls when cls == typeof(PluStorageMethodModel) => new PluStorageMethodValidator(),
            var cls when cls == typeof(PluWeighingModel) => new PluWeighingValidator(),
            var cls when cls == typeof(PrinterModel) => new PrinterValidator(),
            var cls when cls == typeof(PrinterResourceFkModel) => new PrinterResourceFkValidator(),
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
            WsSqlAccessModel access => new WsSqlAccessValidator().Validate(access),
            WsSqlAppModel app => new WsSqlAppValidator().Validate(app),
            BarCodeModel barCode => new BarCodeValidator().Validate(barCode),
            BoxModel box => new BoxValidator().Validate(box),
            BrandModel brand => new BrandValidator().Validate(brand),
            BundleModel bundle => new BundleValidator().Validate(bundle),
            ClipModel clip => new ClipValidator().Validate(clip),
            ContragentModel contragent => new ContragentValidator().Validate(contragent),
            DeviceModel device => new DeviceValidator().Validate(device),
            DeviceScaleFkModel deviceScaleFk => new DeviceScaleFkValidator().Validate(deviceScaleFk),
            DeviceTypeFkModel deviceTypeFk => new DeviceTypeFkValidator().Validate(deviceTypeFk),
            DeviceTypeModel deviceType => new DeviceTypeValidator().Validate(deviceType),
            LogModel log => new LogValidator().Validate(log),
            LogMemoryModel logMemory => new LogMemoryValidator().Validate(logMemory),
            LogTypeModel logType => new LogTypeValidator().Validate(logType),
            LogWebFkModel logWebFk => new LogWebFkValidator().Validate(logWebFk),
            LogWebModel logWeb => new LogWebValidator().Validate(logWeb),
            OrderModel order => new OrderValidator().Validate(order),
            OrderWeighingModel orderWeighing => new OrderWeighingValidator().Validate(orderWeighing),
            OrganizationModel organization => new OrganizationValidator().Validate(organization),
            PluBrandFkModel pluBrandFk => new PluBrandFkValidator().Validate(pluBrandFk),
            PluBundleFkModel pluBundle => new PluBundleFkValidator().Validate(pluBundle),
            PluCharacteristicModel nomenclaturesCharacteristics => new PluCharacteristicValidator().Validate(nomenclaturesCharacteristics),
            PluCharacteristicsFkModel nomenclatureCharacteristicsFk => new PluCharacteristicsFkValidator().Validate(nomenclatureCharacteristicsFk),
            PluClipFkModel pluClip => new PluClipFkValidator().Validate(pluClip),
            PluFkModel pluFk => new PluFkValidator().Validate(pluFk),
            PluGroupFkModel pluGroupFk => new PluGroupFkValidator().Validate(pluGroupFk),
            PluGroupModel nomenclatureGroup => new PluGroupValidator().Validate(nomenclatureGroup),
            PluLabelModel pluLabel => new PluLabelValidator().Validate(pluLabel),
            PluModel plu => new PluValidator().Validate(plu),
            PluNestingFkModel nestingFk => new PluNestingFkValidator().Validate(nestingFk),
            PluScaleModel pluScale => new PluScaleValidator().Validate(pluScale),
            PluStorageMethodModel plusStorageMethod => new PluStorageMethodValidator().Validate(plusStorageMethod),
            PluStorageMethodFkModel plusStorageMethodFk => new PluStorageMethodFkValidator().Validate(plusStorageMethodFk),
            PluTemplateFkModel pluTemplate => new PluTemplateFkValidator().Validate(pluTemplate),
            PluWeighingModel pluWeighing => new PluWeighingValidator().Validate(pluWeighing),
            PrinterModel printer => new PrinterValidator().Validate(printer),
            PrinterResourceFkModel printerResource => new PrinterResourceFkValidator().Validate(printerResource),
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

    #endregion
}