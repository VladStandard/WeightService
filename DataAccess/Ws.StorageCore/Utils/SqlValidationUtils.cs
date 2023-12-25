using System;
using Ws.StorageCore.Entities.SchemaPrint.Labels;
using Ws.StorageCore.Entities.SchemaPrint.Pallets;
using Ws.StorageCore.Entities.SchemaRef.Hosts;
using Ws.StorageCore.Entities.SchemaRef.Printers;

namespace Ws.StorageCore.Utils;

public static class SqlValidationUtils
{
    private static void SetValidationFailureLog(ValidationResult result, ref string detailAddition)
    {
        switch (result.IsValid)
        {
            case false:
                foreach (ValidationFailure failure in result.Errors)
                {
                    detailAddition += $"><u>Ошибка:</u> {failure.ErrorMessage}";
                    break;
                }
                break;
        }
    }
    
    public static ValidationResult GetValidationResult<T>(T? item, bool isCheckIdentity) where T : class, new() =>
        item switch
        {
            SqlLabelEntity label => new SqlLabelValidator(isCheckIdentity).Validate(label),
            SqlBoxEntity box => new SqlBoxValidator(isCheckIdentity).Validate(box),
            SqlBrandEntity brand => new SqlBrandValidator(isCheckIdentity).Validate(brand),
            SqlBundleEntity bundle => new SqlBundleValidator(isCheckIdentity).Validate(bundle),
            SqlClipEntity clip => new SqlClipValidator(isCheckIdentity).Validate(clip),
            SqlHostEntity device => new SqlHostValidator(isCheckIdentity).Validate(device),
            SqlLogEntity log => new SqlLogValidator(isCheckIdentity).Validate(log),
            SqlLogWebEntity logWeb => new SqlLogWebValidator(isCheckIdentity).Validate(logWeb),
            SqlPluFkEntity pluFk => new SqlPluFkValidator(isCheckIdentity).Validate(pluFk),
            SqlPluEntity plu => new SqlPluValidator(isCheckIdentity).Validate(plu),
            SqlPluNestingFkEntity nestingFk => new SqlPluNestingFkValidator(isCheckIdentity).Validate(nestingFk),
            SqlPluScaleEntity pluScale => new SqlPluScaleValidator(isCheckIdentity).Validate(pluScale),
            SqlPluStorageMethodFkEntity plusStorageMethodFk => new SqlPluStorageMethodFkValidator(isCheckIdentity).Validate(plusStorageMethodFk),
            SqlPluStorageMethodEntity plusStorageMethod => new SqlPluStorageMethodValidator(isCheckIdentity).Validate(plusStorageMethod),
            SqlPluTemplateFkEntity pluTemplate => new SqlPluTemplateFkValidator(isCheckIdentity).Validate(pluTemplate),
            SqlPalletEntity pallet => new SqlPalletValidator(isCheckIdentity).Validate(pallet),
            SqlProductionSiteEntity productionFacility => new SqlProductionSiteValidator(isCheckIdentity).Validate(productionFacility),
            SqlLineEntity scale => new SqlLineValidator(isCheckIdentity).Validate(scale),
            SqlTemplateEntity template => new SqlTemplateValidator(isCheckIdentity).Validate(template),
            SqlTemplateResourceEntity templateResource => new SqlTemplateResourceValidator(isCheckIdentity).Validate(templateResource),
            SqlWorkShopEntity workShop => new SqlWorkShopValidator(isCheckIdentity).Validate(workShop),
            SqlAccessEntity access => new SqlAccessValidator(isCheckIdentity).Validate(access),
            SqlPrinterEntity printer => new SqlPrinterValidator(isCheckIdentity).Validate(printer),
            _ => throw new ArgumentOutOfRangeException(nameof(item), item, null)
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
}