using Ws.StorageCore.Entities.SchemaDiag.LogsWebs;
using Ws.StorageCore.Entities.SchemaPrint.Labels;
using Ws.StorageCore.Entities.SchemaPrint.Pallets;
using Ws.StorageCore.Entities.SchemaRef.Claims;
using Ws.StorageCore.Entities.SchemaRef.Lines;
using Ws.StorageCore.Entities.SchemaRef.PlusLines;
using Ws.StorageCore.Entities.SchemaRef.Printers;
using Ws.StorageCore.Entities.SchemaRef.ProductionSites;
using Ws.StorageCore.Entities.SchemaRef.Users;
using Ws.StorageCore.Entities.SchemaRef.WorkShops;
using Ws.StorageCore.Entities.SchemaRef1c.Boxes;
using Ws.StorageCore.Entities.SchemaRef1c.Brands;
using Ws.StorageCore.Entities.SchemaRef1c.Bundles;
using Ws.StorageCore.Entities.SchemaRef1c.Clips;
using Ws.StorageCore.Entities.SchemaRef1c.Plus;
using Ws.StorageCore.Entities.SchemaScale.PlusFks;
using Ws.StorageCore.Entities.SchemaScale.PlusNestingFks;
using Ws.StorageCore.Entities.SchemaScale.PlusStorageMethods;
using Ws.StorageCore.Entities.SchemaScale.PlusStorageMethodsFks;
using Ws.StorageCore.Entities.SchemaScale.PlusTemplatesFks;
using Ws.StorageCore.Entities.SchemaScale.Templates;
using Ws.StorageCore.Entities.SchemaScale.TemplatesResources;

namespace Ws.StorageCore.Utils;

public static class SqlValidationUtils
{
    public static ValidationResult GetValidationResult<T>(T? item, bool isCheckIdentity) where T : class, new() =>
        item switch
        {
            SqlLabelEntity label => new SqlLabelValidator(isCheckIdentity).Validate(label),
            SqlBoxEntity box => new SqlBoxValidator(isCheckIdentity).Validate(box),
            SqlBrandEntity brand => new SqlBrandValidator(isCheckIdentity).Validate(brand),
            SqlBundleEntity bundle => new SqlBundleValidator(isCheckIdentity).Validate(bundle),
            SqlClipEntity clip => new SqlClipValidator(isCheckIdentity).Validate(clip),
            SqlLogWebEntity logWeb => new SqlLogWebValidator(isCheckIdentity).Validate(logWeb),
            SqlPluFkEntity pluFk => new SqlPluFkValidator(isCheckIdentity).Validate(pluFk),
            SqlPluEntity plu => new SqlPluValidator(isCheckIdentity).Validate(plu),
            SqlPluNestingFkEntity nestingFk => new SqlPluNestingFkValidator(isCheckIdentity).Validate(nestingFk),
            SqlPluLineEntity pluScale => new SqlPluLineValidator(isCheckIdentity).Validate(pluScale),
            SqlPluStorageMethodFkEntity plusStorageMethodFk => new SqlPluStorageMethodFkValidator(isCheckIdentity).Validate(plusStorageMethodFk),
            SqlPluStorageMethodEntity plusStorageMethod => new SqlPluStorageMethodValidator(isCheckIdentity).Validate(plusStorageMethod),
            SqlPluTemplateFkEntity pluTemplate => new SqlPluTemplateFkValidator(isCheckIdentity).Validate(pluTemplate),
            SqlPalletEntity pallet => new SqlPalletValidator(isCheckIdentity).Validate(pallet),
            SqlProductionSiteEntity productionFacility => new SqlProductionSiteValidator(isCheckIdentity).Validate(productionFacility),
            SqlLineEntity scale => new SqlLineValidator(isCheckIdentity).Validate(scale),
            SqlTemplateEntity template => new SqlTemplateValidator(isCheckIdentity).Validate(template),
            SqlTemplateResourceEntity templateResource => new SqlTemplateResourceValidator(isCheckIdentity).Validate(templateResource),
            SqlWorkShopEntity workShop => new SqlWorkShopValidator(isCheckIdentity).Validate(workShop),
            SqlPrinterEntity printer => new SqlPrinterValidator(isCheckIdentity).Validate(printer),
            SqlUserEntity user => new SqlUserValidator(isCheckIdentity).Validate(user),
            SqlClaimEntity claim => new SqlClaimValidator(isCheckIdentity).Validate(claim),
            _ => throw new ArgumentOutOfRangeException(nameof(item), item, null)
        };
}