using Ws.Database.Core.Entities.Diag.LogWebs;
using Ws.Database.Core.Entities.Print.Labels;
using Ws.Database.Core.Entities.Print.Pallets;
using Ws.Database.Core.Entities.Ref.Claims;
using Ws.Database.Core.Entities.Ref.Lines;
using Ws.Database.Core.Entities.Ref.PalletMen;
using Ws.Database.Core.Entities.Ref.PlusLines;
using Ws.Database.Core.Entities.Ref.Printers;
using Ws.Database.Core.Entities.Ref.ProductionSites;
using Ws.Database.Core.Entities.Ref.StorageMethods;
using Ws.Database.Core.Entities.Ref.Users;
using Ws.Database.Core.Entities.Ref.Warehouses;
using Ws.Database.Core.Entities.Ref1c.Boxes;
using Ws.Database.Core.Entities.Ref1c.Brands;
using Ws.Database.Core.Entities.Ref1c.Bundles;
using Ws.Database.Core.Entities.Ref1c.Clips;
using Ws.Database.Core.Entities.Ref1c.Plus;
using Ws.Database.Core.Entities.Scales.PlusFks;
using Ws.Database.Core.Entities.Scales.PlusNestingFks;
using Ws.Database.Core.Entities.Scales.PlusTemplatesFks;
using Ws.Database.Core.Entities.Scales.Templates;
using Ws.Database.Core.Entities.Scales.TemplatesResources;
using Ws.Domain.Models.Entities.Diag;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Scale;

namespace Ws.Database.Core.Utils;

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
            PalletManEntity man => new SqlPalletManValidator(isCheckIdentity).Validate(man),
            TemplateEntity template => new SqlTemplateValidator(isCheckIdentity).Validate(template),
            TemplateResourceEntity templateResource => new SqlTemplateResourceValidator(isCheckIdentity).Validate(templateResource),
            WarehouseEntity workShop => new SqlWarehouseValidator(isCheckIdentity).Validate(workShop),
            PrinterEntity printer => new SqlPrinterValidator(isCheckIdentity).Validate(printer),
            UserEntity user => new SqlUserValidator(isCheckIdentity).Validate(user),
            ClaimEntity claim => new SqlClaimValidator(isCheckIdentity).Validate(claim),
            _ => throw new ArgumentOutOfRangeException(nameof(item), item, null)
        };
}