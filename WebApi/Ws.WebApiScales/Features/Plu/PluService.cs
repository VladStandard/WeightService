using FluentValidation.Results;
using Ws.Shared.TypeUtils;
using Ws.StorageCore.Entities.SchemaRef.StorageMethods;
using Ws.StorageCore.Entities.SchemaRef1c.Boxes;
using Ws.StorageCore.Entities.SchemaRef1c.Brands;
using Ws.StorageCore.Entities.SchemaRef1c.Bundles;
using Ws.StorageCore.Entities.SchemaRef1c.Clips;
using Ws.StorageCore.Entities.SchemaRef1c.Plus;
using Ws.StorageCore.Entities.SchemaScale.PlusFks;
using Ws.StorageCore.Entities.SchemaScale.PlusNestingFks;
using Ws.WebApiScales.Dto;
using Ws.WebApiScales.Features.Plu.Dto;
using Ws.WebApiScales.Features.Plu.Validators;
using Ws.WebApiScales.Utils;

namespace Ws.WebApiScales.Features.Plu;


public class PluService(ResponseDto responseDto) : IPluService
{
    #region Private

    private static void SetPluIsMarked(SqlPluEntity plu)
    {
        if (plu.IsNew) return;
        SqlCoreHelper.Instance.Delete(plu);
    }

    private static SqlStorageMethodEntity GetStorageMethodEntity(PluDto pluDto)
    {
        SqlStorageMethodEntity storageDb = new SqlStorageMethodRepository().GetItemByName(pluDto.StorageMethod);

        if (!storageDb.IsNew)
            return storageDb;
        
        storageDb = new SqlStorageMethodRepository().GetItemByName(DefaultNamesUtils.DefaultStorage);
        
        if (!storageDb.IsNew)
            return storageDb;
        
        storageDb.Name = DefaultNamesUtils.DefaultStorage;
        SqlCoreHelper.Instance.SaveOrUpdate(storageDb);

        return storageDb;
    }
    
    private static SqlClipEntity SaveOrUpdateClip(PluDto pluDto)
    {
        SqlClipEntity clipDb = new SqlClipRepository().GetItemByUid1C(pluDto.ClipTypeGuid);
        
        clipDb = pluDto.AdaptTo(clipDb);

        if (clipDb.Uid1C == Guid.Empty)
        {
            clipDb.Weight = 0;
            clipDb.Name = DefaultNamesUtils.DefaultClip;
        }
        
        SqlCoreHelper.Instance.SaveOrUpdate(clipDb);
        return clipDb;
    }
    
    private static SqlBoxEntity SaveOrUpdateBox(PluDto pluDto)
    {
        SqlBoxEntity boxDb = new SqlBoxRepository().GetItemByUid1C(pluDto.BoxTypeGuid);
        
        boxDb = pluDto.AdaptTo(boxDb);
        
        if (boxDb.Uid1C == Guid.Empty)
        {
            boxDb.Name = DefaultNamesUtils.DefaultBox;
            boxDb.Weight = 0;
        }
        
        SqlCoreHelper.Instance.SaveOrUpdate(boxDb);
        return boxDb;
    }
    
    private static SqlBundleEntity SaveOrUpdateBundle(PluDto pluDto)
    {
        SqlBundleEntity bundleDb = new SqlBundleRepository().GetItemByUid1C(pluDto.PackageTypeGuid);
        
        bundleDb = pluDto.AdaptTo(bundleDb);
        
        if (bundleDb.Uid1C == Guid.Empty)
        {
            bundleDb.Name = DefaultNamesUtils.DefaultBundle;
            bundleDb.Weight = 0;
        }
        
        SqlCoreHelper.Instance.SaveOrUpdate(bundleDb);
        return bundleDb;
    }
    
    private static void SaveOrUpdatePluFk(SqlPluEntity plu, PluDto pluDto)
    {
        if (Equals(pluDto.ParentGroupGuid, Guid.Empty)) return;
        if (plu.IsNew) return;
        
        SqlPluEntity parentPluDb = new SqlPluRepository().GetItemByUid1C(pluDto.ParentGroupGuid);
        if (parentPluDb.IsNew) return;
        
        SqlPluEntity categoryDb = new SqlPluRepository().GetItemByUid1C(pluDto.CategoryGuid);
        SqlPluFkEntity pluFkDb = new SqlPluFkRepository().GetByPlu(plu);
        
        pluFkDb.Plu = plu;
        pluFkDb.Parent = parentPluDb;
        pluFkDb.Category = categoryDb.IsExists ? categoryDb : null;
        
        SqlCoreHelper.Instance.SaveOrUpdate(pluFkDb);
    }
    
    private static void SaveOrUpdatePlu(SqlPluEntity plu, PluDto pluDto, SqlBundleEntity bundle)
    {
        plu = pluDto.AdaptTo(plu);
        plu.Bundle = bundle;
        plu.Brand = new SqlBrandRepository().GetItemByUid1C(pluDto.BrandGuid);
        plu.StorageMethod = GetStorageMethodEntity(pluDto);
        SqlCoreHelper.Instance.SaveOrUpdate(plu);
    }
    
    private static void SaveOrUpdatePluNesting(SqlPluEntity plu, PluDto pluDto, SqlBoxEntity box)
    {
        SqlPluNestingFkEntity pluNestingDb = new SqlPluNestingFkRepository().GetDefaultByPlu(plu);

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
            SqlPluEntity pluDb = new SqlPluRepository().GetItemByUid1C(pluDto.Uid);
            
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
            
            SqlClipEntity clip = SaveOrUpdateClip(pluDto);
            SqlBoxEntity box = SaveOrUpdateBox(pluDto);
            SqlBundleEntity bundle = SaveOrUpdateBundle(pluDto);
            
            SaveOrUpdatePlu(pluDb, pluDto, bundle);
            SaveOrUpdatePluFk(pluDb, pluDto);
            SaveOrUpdatePluNesting(pluDb, pluDto, box);
            
            responseDto.AddSuccess(pluDto.Uid, $"{IntUtils.ToStringToLen(pluDb.Number, 3)} | {pluDb.Name}");
        }
    }
}