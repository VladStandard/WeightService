using FluentValidation.Results;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Scale;
using Ws.Shared.TypeUtils;
using Ws.StorageCore.Entities.Ref.StorageMethods;
using Ws.StorageCore.Entities.Ref1c.Boxes;
using Ws.StorageCore.Entities.Ref1c.Brands;
using Ws.StorageCore.Entities.Ref1c.Bundles;
using Ws.StorageCore.Entities.Ref1c.Clips;
using Ws.StorageCore.Entities.Ref1c.Plus;
using Ws.StorageCore.Entities.Scales.PlusFks;
using Ws.StorageCore.Entities.Scales.PlusNestingFks;
using Ws.WebApiScales.Dto;
using Ws.WebApiScales.Features.Plu.Dto;
using Ws.WebApiScales.Features.Plu.Validators;
using Ws.WebApiScales.Utils;

namespace Ws.WebApiScales.Features.Plu;


public class PluService(ResponseDto responseDto) : IPluService
{
    #region Private

    private static void SetPluIsMarked(PluEntity plu)
    {
        if (plu.IsNew) return;
        SqlCoreHelper.Instance.Delete(plu);
    }

    private static StorageMethodEntity GetStorageMethodEntity(PluDto pluDto)
    {
        StorageMethodEntity storageDb = new SqlStorageMethodRepository().GetItemByName(pluDto.StorageMethod);

        if (!storageDb.IsNew)
            return storageDb;
        
        storageDb = new SqlStorageMethodRepository().GetItemByName(DefaultNamesUtils.DefaultStorage);
        
        if (!storageDb.IsNew)
            return storageDb;
        
        storageDb.Name = DefaultNamesUtils.DefaultStorage;
        SqlCoreHelper.Instance.SaveOrUpdate(storageDb);

        return storageDb;
    }
    
    private static ClipEntity SaveOrUpdateClip(PluDto pluDto)
    {
        ClipEntity clipDb = new SqlClipRepository().GetItemByUid1C(pluDto.ClipTypeGuid);
        
        clipDb = pluDto.AdaptTo(clipDb);

        if (clipDb.Uid1C == Guid.Empty)
        {
            clipDb.Weight = 0;
            clipDb.Name = DefaultNamesUtils.DefaultClip;
        }
        
        SqlCoreHelper.Instance.SaveOrUpdate(clipDb);
        return clipDb;
    }
    
    private static BoxEntity SaveOrUpdateBox(PluDto pluDto)
    {
        BoxEntity boxDb = new SqlBoxRepository().GetItemByUid1C(pluDto.BoxTypeGuid);
        
        boxDb = pluDto.AdaptTo(boxDb);
        
        if (boxDb.Uid1C == Guid.Empty)
        {
            boxDb.Name = DefaultNamesUtils.DefaultBox;
            boxDb.Weight = 0;
        }
        
        SqlCoreHelper.Instance.SaveOrUpdate(boxDb);
        return boxDb;
    }
    
    private static BundleEntity SaveOrUpdateBundle(PluDto pluDto)
    {
        BundleEntity bundleDb = new SqlBundleRepository().GetItemByUid1C(pluDto.PackageTypeGuid);
        
        bundleDb = pluDto.AdaptTo(bundleDb);
        
        if (bundleDb.Uid1C == Guid.Empty)
        {
            bundleDb.Name = DefaultNamesUtils.DefaultBundle;
            bundleDb.Weight = 0;
        }
        
        SqlCoreHelper.Instance.SaveOrUpdate(bundleDb);
        return bundleDb;
    }
    
    private static void SaveOrUpdatePluFk(PluEntity plu, PluDto pluDto)
    {
        if (Equals(pluDto.ParentGroupGuid, Guid.Empty)) return;
        if (plu.IsNew) return;
        
        PluEntity parentPluDb = new SqlPluRepository().GetItemByUid1C(pluDto.ParentGroupGuid);
        if (parentPluDb.IsNew) return;
        
        PluEntity categoryDb = new SqlPluRepository().GetItemByUid1C(pluDto.CategoryGuid);
        PluFkEntity pluFkDb = new SqlPluFkRepository().GetByPlu(plu);
        
        pluFkDb.Plu = plu;
        pluFkDb.Parent = parentPluDb;
        pluFkDb.Category = categoryDb.IsExists ? categoryDb : null;
        
        SqlCoreHelper.Instance.SaveOrUpdate(pluFkDb);
    }
    
    private static void SaveOrUpdatePlu(PluEntity plu, PluDto pluDto, BundleEntity bundle)
    {
        plu = pluDto.AdaptTo(plu);
        plu.Bundle = bundle;
        plu.Brand = new SqlBrandRepository().GetItemByUid1C(pluDto.BrandGuid);
        plu.StorageMethod = GetStorageMethodEntity(pluDto);
        SqlCoreHelper.Instance.SaveOrUpdate(plu);
    }
    
    private static void SaveOrUpdatePluNesting(PluEntity plu, PluDto pluDto, BoxEntity box)
    {
        PluNestingEntity pluNestingDb = new SqlPluNestingFkRepository().GetDefaultByPlu(plu);

        pluNestingDb.Plu = plu;
        pluNestingDb.Box = box;
        pluNestingDb.IsDefault = true;
        pluNestingDb.BundleCount = pluDto.AttachmentsCount;
        SqlCoreHelper.Instance.SaveOrUpdate(pluNestingDb);
    }

    #endregion
    
    public void Load(PlusWrapper plusWrapper)
    {
        List<PluDto> orderedEnumerable = plusWrapper.Plus.OrderBy(item => item.PluNumber).ToList();
        
        foreach (PluDto pluDto in orderedEnumerable)
        {
            PluEntity pluDb = new SqlPluRepository().GetItemByUid1C(pluDto.Uid);
            
            if (pluDto.IsMarked)
            {
                SetPluIsMarked(pluDb);
                continue;
            }
            
            ValidationResult validationResult = new ValidatorPluDto().Validate(pluDto);
            
            if (!validationResult.IsValid)
            {
                List<string> errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                responseDto.AddError(pluDto.Uid, $"{pluDto.PluNumber} | {pluDto.Name} | {string.Join(" | ", errors)}");
                continue;
            }
            
            ClipEntity clip = SaveOrUpdateClip(pluDto);
            BoxEntity box = SaveOrUpdateBox(pluDto);
            BundleEntity bundle = SaveOrUpdateBundle(pluDto);
            
            SaveOrUpdatePlu(pluDb, pluDto, bundle);
            SaveOrUpdatePluFk(pluDb, pluDto);
            SaveOrUpdatePluNesting(pluDb, pluDto, box);
            
            responseDto.AddSuccess(pluDto.Uid, $"{IntUtils.ToStringToLen(pluDb.Number, 3)} | {pluDb.Name}");
        }
    }
}