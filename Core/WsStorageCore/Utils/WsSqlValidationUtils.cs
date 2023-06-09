// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Common;

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
                    detailAddition += $"<u>{WsLocaleCore.Validator.Property}:</u> {failure.PropertyName} {WsLocaleCore.Validator.FailedValidation}.<br><u>{WsLocaleCore.Validator.Error}:</u> {failure.ErrorMessage}";
                    break;
                }
                break;
        }
    }

    public static IValidator GetSqlValidator<T>() where T : WsSqlTableBase, new() =>
        typeof(T) switch
        {
            var cls when cls == typeof(WsSqlBarCodeModel) => new WsSqlBarCodeValidator(),
            var cls when cls == typeof(WsSqlBoxModel) => new WsSqlBoxValidator(),
            var cls when cls == typeof(WsSqlBrandModel) => new WsSqlBrandValidator(),
            var cls when cls == typeof(WsSqlBundleModel) => new WsSqlBundleValidator(),
            var cls when cls == typeof(WsSqlClipModel) => new WsSqlClipValidator(),
            var cls when cls == typeof(WsSqlContragentModel) => new WsSqlContragentValidator(),
            var cls when cls == typeof(WsSqlDeviceModel) => new WsSqlDeviceValidator(),
            var cls when cls == typeof(WsSqlDeviceScaleFkModel) => new WsSqlDeviceScaleFkValidator(),
            var cls when cls == typeof(WsSqlDeviceTypeFkModel) => new WsSqlDeviceTypeFkValidator(),
            var cls when cls == typeof(WsSqlDeviceTypeModel) => new WsSqlDeviceTypeValidator(),
            var cls when cls == typeof(WsSqlLogMemoryModel) => new WsSqlLogMemoryValidator(),
            var cls when cls == typeof(WsSqlLogModel) => new WsSqlLogValidator(),
            var cls when cls == typeof(WsSqlLogTypeModel) => new WsSqlLogTypeValidator(),
            var cls when cls == typeof(WsSqlLogWebFkModel) => new WsSqlLogWebFkValidator(),
            var cls when cls == typeof(WsSqlLogWebModel) => new WsSqlLogWebValidator(),
            var cls when cls == typeof(WsSqlOrderModel) => new WsSqlOrderValidator(),
            var cls when cls == typeof(WsSqlOrderWeighingModel) => new WsSqlOrderWeighingValidator(),
            var cls when cls == typeof(WsSqlOrganizationModel) => new WsSqlOrganizationValidator(),
            var cls when cls == typeof(WsSqlPluBundleFkModel) => new WsSqlPluBundleFkValidator(),
            var cls when cls == typeof(WsSqlPluCharacteristicModel) => new WsSqlPluCharacteristicValidator(),
            var cls when cls == typeof(WsSqlPluCharacteristicsFkModel) => new WsSqlPluCharacteristicsFkValidator(),
            var cls when cls == typeof(WsSqlPluClipFkModel) => new WsSqlPluClipFkValidator(),
            var cls when cls == typeof(WsSqlPluGroupModel) => new WsSqlPluGroupValidator(),
            var cls when cls == typeof(WsSqlPluLabelModel) => new WsSqlPluLabelValidator(),
            var cls when cls == typeof(WsSqlPluModel) => new WsSqlPluValidator(),
            var cls when cls == typeof(WsSqlPluNestingFkModel) => new WsSqlPluNestingFkValidator(),
            var cls when cls == typeof(WsSqlPluScaleModel) => new WsSqlPluScaleValidator(),
            var cls when cls == typeof(WsSqlPluStorageMethodModel) => new WsSqlPluStorageMethodValidator(),
            var cls when cls == typeof(WsSqlPluWeighingModel) => new WsSqlPluWeighingValidator(),
            var cls when cls == typeof(WsSqlPrinterModel) => new WsSqlPrinterValidator(),
            var cls when cls == typeof(WsSqlPrinterResourceFkModel) => new WsSqlPrinterResourceFkValidator(),
            var cls when cls == typeof(WsSqlPrinterTypeModel) => new WsSqlPrinterTypeValidator(),
            var cls when cls == typeof(WsSqlProductionFacilityModel) => new WsSqlProductionFacilityValidator(),
            var cls when cls == typeof(WsSqlProductSeriesModel) => new WsSqlProductSeriesValidator(),
            var cls when cls == typeof(WsSqlScaleModel) => new WsSqlScaleValidator(),
            var cls when cls == typeof(WsSqlTaskModel) => new WsSqlTaskValidator(),
            var cls when cls == typeof(WsSqlTaskTypeModel) => new WsSqlTaskTypeValidator(),
            var cls when cls == typeof(WsSqlTemplateModel) => new WsSqlTemplateValidator(),
            var cls when cls == typeof(WsSqlTemplateResourceModel) => new WsSqlTemplateResourceValidator(),
            var cls when cls == typeof(WsSqlVersionModel) => new WsSqlVersionValidator(),
            var cls when cls == typeof(WsSqlWorkShopModel) => new WsSqlWorkShopValidator(),
            var cls when cls == typeof(WsSqlAccessModel) => new WsSqlAccessValidator(),
            var cls when cls == typeof(WsSqlAppModel) => new WsSqlAppValidator(),
            var cls when cls == typeof(WsSqlPlu1CFkModel) => new WsSqlPlu1CFkValidator(),
            _ => throw new NotImplementedException()
        };

    public static ValidationResult GetValidationResult<T>(T? item) where T : class, new() =>
        item switch
        {
            WsSqlBarCodeModel barCode => new WsSqlBarCodeValidator().Validate(barCode),
            WsSqlBoxModel box => new WsSqlBoxValidator().Validate(box),
            WsSqlBrandModel brand => new WsSqlBrandValidator().Validate(brand),
            WsSqlBundleModel bundle => new WsSqlBundleValidator().Validate(bundle),
            WsSqlClipModel clip => new WsSqlClipValidator().Validate(clip),
            WsSqlContragentModel contragent => new WsSqlContragentValidator().Validate(contragent),
            WsSqlDeviceModel device => new WsSqlDeviceValidator().Validate(device),
            WsSqlDeviceScaleFkModel deviceScaleFk => new WsSqlDeviceScaleFkValidator().Validate(deviceScaleFk),
            WsSqlDeviceTypeFkModel deviceTypeFk => new WsSqlDeviceTypeFkValidator().Validate(deviceTypeFk),
            WsSqlDeviceTypeModel deviceType => new WsSqlDeviceTypeValidator().Validate(deviceType),
            WsSqlLogMemoryModel logMemory => new WsSqlLogMemoryValidator().Validate(logMemory),
            WsSqlLogModel log => new WsSqlLogValidator().Validate(log),
            WsSqlLogTypeModel logType => new WsSqlLogTypeValidator().Validate(logType),
            WsSqlLogWebFkModel logWebFk => new WsSqlLogWebFkValidator().Validate(logWebFk),
            WsSqlLogWebModel logWeb => new WsSqlLogWebValidator().Validate(logWeb),
            WsSqlOrderModel order => new WsSqlOrderValidator().Validate(order),
            WsSqlOrderWeighingModel orderWeighing => new WsSqlOrderWeighingValidator().Validate(orderWeighing),
            WsSqlOrganizationModel organization => new WsSqlOrganizationValidator().Validate(organization),
            WsSqlPluBrandFkModel pluBrandFk => new WsSqlPluBrandFkValidator().Validate(pluBrandFk),
            WsSqlPluBundleFkModel pluBundle => new WsSqlPluBundleFkValidator().Validate(pluBundle),
            WsSqlPluCharacteristicModel nomenclaturesCharacteristics => new WsSqlPluCharacteristicValidator().Validate(nomenclaturesCharacteristics),
            WsSqlPluCharacteristicsFkModel nomenclatureCharacteristicsFk => new WsSqlPluCharacteristicsFkValidator().Validate(nomenclatureCharacteristicsFk),
            WsSqlPluClipFkModel pluClip => new WsSqlPluClipFkValidator().Validate(pluClip),
            WsSqlPluFkModel pluFk => new WsSqlPluFkValidator().Validate(pluFk),
            WsSqlPluGroupFkModel pluGroupFk => new WsSqlPluGroupFkValidator().Validate(pluGroupFk),
            WsSqlPluGroupModel nomenclatureGroup => new WsSqlPluGroupValidator().Validate(nomenclatureGroup),
            WsSqlPluLabelModel pluLabel => new WsSqlPluLabelValidator().Validate(pluLabel),
            WsSqlPluModel plu => new WsSqlPluValidator().Validate(plu),
            WsSqlPluNestingFkModel nestingFk => new WsSqlPluNestingFkValidator().Validate(nestingFk),
            WsSqlPluScaleModel pluScale => new WsSqlPluScaleValidator().Validate(pluScale),
            WsSqlPluStorageMethodFkModel plusStorageMethodFk => new WsSqlPluStorageMethodFkValidator().Validate(plusStorageMethodFk),
            WsSqlPluStorageMethodModel plusStorageMethod => new WsSqlPluStorageMethodValidator().Validate(plusStorageMethod),
            WsSqlPluTemplateFkModel pluTemplate => new WsSqlPluTemplateFkValidator().Validate(pluTemplate),
            WsSqlPluWeighingModel pluWeighing => new WsSqlPluWeighingValidator().Validate(pluWeighing),
            WsSqlPrinterModel printer => new WsSqlPrinterValidator().Validate(printer),
            WsSqlPrinterResourceFkModel printerResource => new WsSqlPrinterResourceFkValidator().Validate(printerResource),
            WsSqlPrinterTypeModel printerType => new WsSqlPrinterTypeValidator().Validate(printerType),
            WsSqlProductionFacilityModel productionFacility => new WsSqlProductionFacilityValidator().Validate(productionFacility),
            WsSqlProductSeriesModel productSeries => new WsSqlProductSeriesValidator().Validate(productSeries),
            WsSqlScaleModel scale => new WsSqlScaleValidator().Validate(scale),
            WsSqlScaleScreenShotModel scaleScreenShot => new WsSqlScaleScreenShotValidator().Validate(scaleScreenShot),
            WsSqlTaskModel task => new WsSqlTaskValidator().Validate(task),
            WsSqlTaskTypeModel taskType => new WsSqlTaskTypeValidator().Validate(taskType),
            WsSqlTemplateModel template => new WsSqlTemplateValidator().Validate(template),
            WsSqlTemplateResourceModel templateResource => new WsSqlTemplateResourceValidator().Validate(templateResource),
            WsSqlVersionModel version => new WsSqlVersionValidator().Validate(version),
            WsSqlWorkShopModel workShop => new WsSqlWorkShopValidator().Validate(workShop),
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
        if (validationResult.IsValid)
            return true;
        
        SetValidationFailureLog(validationResult, ref detailAddition);
        return false;

    }

    #endregion
}