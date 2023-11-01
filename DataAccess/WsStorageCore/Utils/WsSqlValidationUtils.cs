using WsStorageCore.Entities.SchemaRef.Hosts;
using WsStorageCore.Entities.SchemaRef.Printers;
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
            WsSqlBarCodeEntity barCode => new WsSqlBarCodeValidator(isCheckIdentity).Validate(barCode),
            WsSqlBoxEntity box => new WsSqlBoxValidator(isCheckIdentity).Validate(box),
            WsSqlBrandEntity brand => new WsSqlBrandValidator(isCheckIdentity).Validate(brand),
            WsSqlBundleEntity bundle => new WsSqlBundleValidator(isCheckIdentity).Validate(bundle),
            WsSqlClipEntity clip => new WsSqlClipValidator(isCheckIdentity).Validate(clip),
            WsSqlHostEntity device => new WsSqlHostValidator(isCheckIdentity).Validate(device),
            WsSqlLogEntity log => new WsSqlLogValidator(isCheckIdentity).Validate(log),
            WsSqlLogWebEntity logWeb => new WsSqlLogWebValidator(isCheckIdentity).Validate(logWeb),
            WsSqlPluClipFkEntity pluClip => new WsSqlPluClipFkValidator(isCheckIdentity).Validate(pluClip),
            WsSqlPluFkEntity pluFk => new WsSqlPluFkValidator(isCheckIdentity).Validate(pluFk),
            WsSqlPluLabelEntity pluLabel => new WsSqlPluLabelValidator(isCheckIdentity).Validate(pluLabel),
            WsSqlPluEntity plu => new WsSqlPluValidator(isCheckIdentity).Validate(plu),
            WsSqlPluNestingFkEntity nestingFk => new WsSqlPluNestingFkValidator(isCheckIdentity).Validate(nestingFk),
            WsSqlPluScaleEntity pluScale => new WsSqlPluScaleValidator(isCheckIdentity).Validate(pluScale),
            WsSqlPluStorageMethodFkEntity plusStorageMethodFk => new WsSqlPluStorageMethodFkValidator(isCheckIdentity).Validate(plusStorageMethodFk),
            WsSqlPluStorageMethodEntity plusStorageMethod => new WsSqlPluStorageMethodValidator(isCheckIdentity).Validate(plusStorageMethod),
            WsSqlPluTemplateFkEntity pluTemplate => new WsSqlPluTemplateFkValidator(isCheckIdentity).Validate(pluTemplate),
            WsSqlPluWeighingEntity pluWeighing => new WsSqlPluWeighingValidator(isCheckIdentity).Validate(pluWeighing),
            WsSqlProductionSiteEntity productionFacility => new WsSqlProductionSiteValidator(isCheckIdentity).Validate(productionFacility),
            WsSqlScaleEntity scale => new WsSqlScaleValidator(isCheckIdentity).Validate(scale),
            WsSqlTemplateEntity template => new WsSqlTemplateValidator(isCheckIdentity).Validate(template),
            WsSqlTemplateResourceEntity templateResource => new WsSqlTemplateResourceValidator(isCheckIdentity).Validate(templateResource),
            WsSqlVersionEntity version => new WsSqlVersionValidator(isCheckIdentity).Validate(version),
            WsSqlWorkShopEntity workShop => new WsSqlWorkShopValidator(isCheckIdentity).Validate(workShop),
            WsSqlAccessEntity access => new WsSqlAccessValidator(isCheckIdentity).Validate(access),
            WsSqlAppEntity app => new WsSqlAppValidator(isCheckIdentity).Validate(app),
            WsSqlPrinterEntity printer => new WsSqlPrinterValidator(isCheckIdentity).Validate(printer),
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