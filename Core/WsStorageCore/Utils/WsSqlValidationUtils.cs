using WsStorageCore.Tables.TableRef1cModels.Brands;
using WsStorageCore.Tables.TableRefModels.ProductionSites;
using WsStorageCore.Tables.TableRefModels.WorkShops;

namespace WsStorageCore.Utils;

public static class WsSqlValidationUtils
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
    
    public static ValidationResult GetValidationResult<T>(T? item, bool isCheckIdentity) where T : class, new() =>
        item switch
        {
            WsSqlBarCodeModel barCode => new WsSqlBarCodeValidator(isCheckIdentity).Validate(barCode),
            WsSqlBoxModel box => new WsSqlBoxValidator(isCheckIdentity).Validate(box),
            WsSqlBrandModel brand => new WsSqlBrandValidator(isCheckIdentity).Validate(brand),
            WsSqlBundleModel bundle => new WsSqlBundleValidator(isCheckIdentity).Validate(bundle),
            WsSqlClipModel clip => new WsSqlClipValidator(isCheckIdentity).Validate(clip),
            WsSqlDeviceModel device => new WsSqlDeviceValidator(isCheckIdentity).Validate(device),
            WsSqlDeviceSettingsModel deviceSettings => new WsSqlDeviceSettingsValidator(isCheckIdentity).Validate(deviceSettings),
            WsSqlDeviceSettingsFkModel deviceSettingsFks => new WsSqlDeviceSettingsFkValidator(isCheckIdentity).Validate(deviceSettingsFks),
            WsSqlDeviceTypeFkModel deviceTypeFk => new WsSqlDeviceTypeFkValidator(isCheckIdentity).Validate(deviceTypeFk),
            WsSqlDeviceTypeModel deviceType => new WsSqlDeviceTypeValidator(isCheckIdentity).Validate(deviceType),
            WsSqlLogModel log => new WsSqlLogValidator(isCheckIdentity).Validate(log),
            WsSqlLogTypeModel logType => new WsSqlLogTypeValidator(isCheckIdentity).Validate(logType),
            WsSqlLogWebModel logWeb => new WsSqlLogWebValidator(isCheckIdentity).Validate(logWeb),
            WsSqlOrderModel order => new WsSqlOrderValidator(isCheckIdentity).Validate(order),
            WsSqlOrderWeighingModel orderWeighing => new WsSqlOrderWeighingValidator(isCheckIdentity).Validate(orderWeighing),
            WsSqlOrganizationModel organization => new WsSqlOrganizationValidator(isCheckIdentity).Validate(organization),
            WsSqlPluClipFkModel pluClip => new WsSqlPluClipFkValidator(isCheckIdentity).Validate(pluClip),
            WsSqlPluFkModel pluFk => new WsSqlPluFkValidator(isCheckIdentity).Validate(pluFk),
            WsSqlPluLabelModel pluLabel => new WsSqlPluLabelValidator(isCheckIdentity).Validate(pluLabel),
            WsSqlPluModel plu => new WsSqlPluValidator(isCheckIdentity).Validate(plu),
            WsSqlPluNestingFkModel nestingFk => new WsSqlPluNestingFkValidator(isCheckIdentity).Validate(nestingFk),
            WsSqlPluScaleModel pluScale => new WsSqlPluScaleValidator(isCheckIdentity).Validate(pluScale),
            WsSqlPluStorageMethodFkModel plusStorageMethodFk => new WsSqlPluStorageMethodFkValidator(isCheckIdentity).Validate(plusStorageMethodFk),
            WsSqlPluStorageMethodModel plusStorageMethod => new WsSqlPluStorageMethodValidator(isCheckIdentity).Validate(plusStorageMethod),
            WsSqlPluTemplateFkModel pluTemplate => new WsSqlPluTemplateFkValidator(isCheckIdentity).Validate(pluTemplate),
            WsSqlPluWeighingModel pluWeighing => new WsSqlPluWeighingValidator(isCheckIdentity).Validate(pluWeighing),
            WsSqlPrinterModel printer => new WsSqlPrinterValidator(isCheckIdentity).Validate(printer),
            WsSqlPrinterTypeModel printerType => new WsSqlPrinterTypeValidator(isCheckIdentity).Validate(printerType),
            WsSqlProductionSiteModel productionFacility => new WsSqlProductionSiteValidator(isCheckIdentity).Validate(productionFacility),
            WsSqlScaleModel scale => new WsSqlScaleValidator(isCheckIdentity).Validate(scale),
            WsSqlTaskModel task => new WsSqlTaskValidator(isCheckIdentity).Validate(task),
            WsSqlTaskTypeModel taskType => new WsSqlTaskTypeValidator(isCheckIdentity).Validate(taskType),
            WsSqlTemplateModel template => new WsSqlTemplateValidator(isCheckIdentity).Validate(template),
            WsSqlTemplateResourceModel templateResource => new WsSqlTemplateResourceValidator(isCheckIdentity).Validate(templateResource),
            WsSqlVersionModel version => new WsSqlVersionValidator(isCheckIdentity).Validate(version),
            WsSqlWorkShopModel workShop => new WsSqlWorkShopValidator(isCheckIdentity).Validate(workShop),
            WsSqlAccessModel access => new WsSqlAccessValidator(isCheckIdentity).Validate(access),
            WsSqlAppModel app => new WsSqlAppValidator(isCheckIdentity).Validate(app),
            WsSqlPlu1CFkModel app => new WsSqlPlu1CFkValidator(isCheckIdentity).Validate(app),
            _ => throw new NotImplementedException()
        };

    public static bool IsValidation<T>(T? item, ref string detailAddition, bool isCheckIdentity) where T : class, new()
    {
        if (item is null)
        {
            detailAddition = $"{nameof(item)} is null!";
            return false;
        }

        ValidationResult validationResult = GetValidationResult(item, isCheckIdentity);
        if (validationResult.IsValid)
            return true;
        
        SetValidationFailureLog(validationResult, ref detailAddition);
        return false;

    }

    #endregion
}