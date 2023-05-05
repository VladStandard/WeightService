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
            var cls when cls == typeof(BarCodeModel) => new BarCodeValidator(),
            var cls when cls == typeof(BoxModel) => new BoxValidator(),
            var cls when cls == typeof(BrandModel) => new BrandValidator(),
            var cls when cls == typeof(BundleModel) => new BundleValidator(),
            var cls when cls == typeof(ClipModel) => new ClipValidator(),
            var cls when cls == typeof(ContragentModel) => new ContragentValidator(),
            var cls when cls == typeof(DeviceModel) => new DeviceValidator(),
            var cls when cls == typeof(WsSqlDeviceScaleFkModel) => new WsSqlDeviceScaleFkValidator(),
            var cls when cls == typeof(WsSqlDeviceTypeFkModel) => new WsSqlDeviceTypeFkValidator(),
            var cls when cls == typeof(DeviceTypeModel) => new DeviceTypeValidator(),
            var cls when cls == typeof(WsSqlLogMemoryModel) => new WsSqlLogMemoryValidator(),
            var cls when cls == typeof(WsSqlLogModel) => new WsSqlLogValidator(),
            var cls when cls == typeof(WsSqlLogTypeModel) => new WsSqlLogTypeValidator(),
            var cls when cls == typeof(WsSqlLogWebFkModel) => new WsSqlLogWebFkValidator(),
            var cls when cls == typeof(WsSqlLogWebModel) => new WsSqlLogWebValidator(),
            var cls when cls == typeof(OrderModel) => new OrderValidator(),
            var cls when cls == typeof(OrderWeighingModel) => new OrderWeighingValidator(),
            var cls when cls == typeof(OrganizationModel) => new OrganizationValidator(),
            var cls when cls == typeof(WsSqlPluBundleFkModel) => new WsSqlPluBundleFkValidator(),
            var cls when cls == typeof(PluCharacteristicModel) => new PluCharacteristicValidator(),
            var cls when cls == typeof(WsSqlPluCharacteristicsFkModel) => new WsSqlPluCharacteristicsFkValidator(),
            var cls when cls == typeof(WsSqlPluClipFkModel) => new WsSqlPluClipFkValidator(),
            var cls when cls == typeof(PluGroupModel) => new PluGroupValidator(),
            var cls when cls == typeof(WsSqlPluLabelModel) => new WsSqlPluLabelValidator(),
            var cls when cls == typeof(WsSqlPluModel) => new WsSqlPluValidator(),
            var cls when cls == typeof(WsSqlPluNestingFkModel) => new WsSqlPluNestingFkValidator(),
            var cls when cls == typeof(PluScaleModel) => new PluScaleValidator(),
            var cls when cls == typeof(PluStorageMethodModel) => new PluStorageMethodValidator(),
            var cls when cls == typeof(WsSqlPluWeighingModel) => new WsSqlPluWeighingValidator(),
            var cls when cls == typeof(PrinterModel) => new PrinterValidator(),
            var cls when cls == typeof(WsSqlPrinterResourceFkModel) => new WsSqlPrinterResourceFkValidator(),
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
            var cls when cls == typeof(WsSqlAccessModel) => new WsSqlAccessValidator(),
            var cls when cls == typeof(WsSqlAppModel) => new WsSqlAppValidator(),
            var cls when cls == typeof(WsSqlPlu1CFkModel) => new WsSqlPlu1CFkValidator(),
            _ => throw new NotImplementedException()
        };

    public static ValidationResult GetValidationResult<T>(T? item) where T : class, new() =>
        item switch
        {
            BarCodeModel barCode => new BarCodeValidator().Validate(barCode),
            BoxModel box => new BoxValidator().Validate(box),
            BrandModel brand => new BrandValidator().Validate(brand),
            BundleModel bundle => new BundleValidator().Validate(bundle),
            ClipModel clip => new ClipValidator().Validate(clip),
            ContragentModel contragent => new ContragentValidator().Validate(contragent),
            DeviceModel device => new DeviceValidator().Validate(device),
            WsSqlDeviceScaleFkModel deviceScaleFk => new WsSqlDeviceScaleFkValidator().Validate(deviceScaleFk),
            WsSqlDeviceTypeFkModel deviceTypeFk => new WsSqlDeviceTypeFkValidator().Validate(deviceTypeFk),
            DeviceTypeModel deviceType => new DeviceTypeValidator().Validate(deviceType),
            WsSqlLogMemoryModel logMemory => new WsSqlLogMemoryValidator().Validate(logMemory),
            WsSqlLogModel log => new WsSqlLogValidator().Validate(log),
            WsSqlLogTypeModel logType => new WsSqlLogTypeValidator().Validate(logType),
            WsSqlLogWebFkModel logWebFk => new WsSqlLogWebFkValidator().Validate(logWebFk),
            WsSqlLogWebModel logWeb => new WsSqlLogWebValidator().Validate(logWeb),
            OrderModel order => new OrderValidator().Validate(order),
            OrderWeighingModel orderWeighing => new OrderWeighingValidator().Validate(orderWeighing),
            OrganizationModel organization => new OrganizationValidator().Validate(organization),
            WsSqlPluBrandFkModel pluBrandFk => new WsSqlPluBrandFkValidator().Validate(pluBrandFk),
            WsSqlPluBundleFkModel pluBundle => new WsSqlPluBundleFkValidator().Validate(pluBundle),
            PluCharacteristicModel nomenclaturesCharacteristics => new PluCharacteristicValidator().Validate(nomenclaturesCharacteristics),
            WsSqlPluCharacteristicsFkModel nomenclatureCharacteristicsFk => new WsSqlPluCharacteristicsFkValidator().Validate(nomenclatureCharacteristicsFk),
            WsSqlPluClipFkModel pluClip => new WsSqlPluClipFkValidator().Validate(pluClip),
            WsSqlPluFkModel pluFk => new WsSqlPluFkValidator().Validate(pluFk),
            WsSqlPluGroupFkModel pluGroupFk => new WsSqlPluGroupFkValidator().Validate(pluGroupFk),
            PluGroupModel nomenclatureGroup => new PluGroupValidator().Validate(nomenclatureGroup),
            WsSqlPluLabelModel pluLabel => new WsSqlPluLabelValidator().Validate(pluLabel),
            WsSqlPluModel plu => new WsSqlPluValidator().Validate(plu),
            WsSqlPluNestingFkModel nestingFk => new WsSqlPluNestingFkValidator().Validate(nestingFk),
            PluScaleModel pluScale => new PluScaleValidator().Validate(pluScale),
            WsSqlPluStorageMethodFkModel plusStorageMethodFk => new WsSqlPluStorageMethodFkValidator().Validate(plusStorageMethodFk),
            PluStorageMethodModel plusStorageMethod => new PluStorageMethodValidator().Validate(plusStorageMethod),
            WsSqlPluTemplateFkModel pluTemplate => new WsSqlPluTemplateFkValidator().Validate(pluTemplate),
            WsSqlPluWeighingModel pluWeighing => new WsSqlPluWeighingValidator().Validate(pluWeighing),
            PrinterModel printer => new PrinterValidator().Validate(printer),
            WsSqlPrinterResourceFkModel printerResource => new WsSqlPrinterResourceFkValidator().Validate(printerResource),
            PrinterTypeModel printerType => new PrinterTypeValidator().Validate(printerType),
            ProductionFacilityModel productionFacility => new ProductionFacilityValidator().Validate(productionFacility),
            ProductSeriesModel productSeries => new ProductSeriesValidator().Validate(productSeries),
            ScaleModel scale => new ScaleValidator().Validate(scale),
            WsSqlScaleScreenShotModel scaleScreenShot => new WsSqlScaleScreenShotValidator().Validate(scaleScreenShot),
            TaskModel task => new TaskValidator().Validate(task),
            TaskTypeModel taskType => new TaskTypeValidator().Validate(taskType),
            TemplateModel template => new TemplateValidator().Validate(template),
            TemplateResourceModel templateResource => new TemplateResourceValidator().Validate(templateResource),
            VersionModel version => new VersionValidator().Validate(version),
            WorkShopModel workShop => new WorkShopValidator().Validate(workShop),
            WsSqlAccessModel access => new WsSqlAccessValidator().Validate(access),
            WsSqlAppModel app => new WsSqlAppValidator().Validate(app),
            WsSqlPlu1CFkModel app => new WsSqlPlu1CFkValidator().Validate(app),
            _ => throw new NotImplementedException()
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