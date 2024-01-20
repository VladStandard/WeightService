using Ws.Domain.Models.Entities.Diag;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Scale;
using Ws.Domain.Models.Entities.SchemaScale;
using Ws.StorageCore.Entities.Diag.LogWebs;
using Ws.StorageCore.Entities.Print.Labels;
using Ws.StorageCore.Entities.Print.Pallets;
using Ws.StorageCore.Entities.Ref.Claims;
using Ws.StorageCore.Entities.Ref.Lines;
using Ws.StorageCore.Entities.Ref.PlusLines;
using Ws.StorageCore.Entities.Ref.Printers;
using Ws.StorageCore.Entities.Ref.ProductionSites;
using Ws.StorageCore.Entities.Ref.StorageMethods;
using Ws.StorageCore.Entities.Ref.Users;
using Ws.StorageCore.Entities.Ref.Warehouses;
using Ws.StorageCore.Entities.Ref1c.Boxes;
using Ws.StorageCore.Entities.Ref1c.Brands;
using Ws.StorageCore.Entities.Ref1c.Bundles;
using Ws.StorageCore.Entities.Ref1c.Clips;
using Ws.StorageCore.Entities.Ref1c.Plus;
using Ws.StorageCore.Entities.Scales.PlusFks;
using Ws.StorageCore.Entities.Scales.PlusNestingFks;
using Ws.StorageCore.Entities.Scales.PlusTemplatesFks;
using Ws.StorageCore.Entities.Scales.Templates;
using Ws.StorageCore.Entities.Scales.TemplatesResources;

namespace Ws.StorageCore.Utils;

public static class SqlValidationUtils
{
    public static ValidationResult GetValidationResult<T>(T? item, bool isCheckIdentity) where T : class, new() =>
        item switch
        {
            LabelEntity label => new SqlLabelValidator(isCheckIdentity).Validate(label),
            BoxEntity box => new SqlBoxValidator(isCheckIdentity).Validate(box),
            BrandEntity brand => new SqlBrandValidator(isCheckIdentity).Validate(brand),
            BundleEntity bundle => new SqlBundleValidator(isCheckIdentity).Validate(bundle),
            ClipEntity clip => new SqlClipValidator(isCheckIdentity).Validate(clip),
            LogWebEntity logWeb => new SqlLogWebValidator(isCheckIdentity).Validate(logWeb),
            PluFkEntity pluFk => new SqlPluFkValidator(isCheckIdentity).Validate(pluFk),
            PluEntity plu => new SqlPluValidator(isCheckIdentity).Validate(plu),
            PluNestingEntity nestingFk => new SqlPluNestingFkValidator(isCheckIdentity).Validate(nestingFk),
            PluLineEntity pluScale => new SqlPluLineValidator(isCheckIdentity).Validate(pluScale),
            StorageMethodEntity plusStorageMethod => new SqlStorageMethodValidator(isCheckIdentity).Validate(plusStorageMethod),
            PluTemplateFkEntity pluTemplate => new SqlPluTemplateFkValidator(isCheckIdentity).Validate(pluTemplate),
            PalletEntity pallet => new SqlPalletValidator(isCheckIdentity).Validate(pallet),
            ProductionSiteEntity productionFacility => new SqlProductionSiteValidator(isCheckIdentity).Validate(productionFacility),
            LineEntity scale => new SqlLineValidator(isCheckIdentity).Validate(scale),
            TemplateEntity template => new SqlTemplateValidator(isCheckIdentity).Validate(template),
            TemplateResourceEntity templateResource => new SqlTemplateResourceValidator(isCheckIdentity).Validate(templateResource),
            WarehouseEntity workShop => new SqlWarehouseValidator(isCheckIdentity).Validate(workShop),
            PrinterEntity printer => new SqlPrinterValidator(isCheckIdentity).Validate(printer),
            UserEntity user => new SqlUserValidator(isCheckIdentity).Validate(user),
            ClaimEntity claim => new SqlClaimValidator(isCheckIdentity).Validate(claim),
            _ => throw new ArgumentOutOfRangeException(nameof(item), item, null)
        };
}