using FluentValidation.Results;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Scale;
using Ws.Domain.Services.Features.Box;
using Ws.Domain.Services.Features.Brand;
using Ws.Domain.Services.Features.Bundle;
using Ws.Domain.Services.Features.Clip;
using Ws.Domain.Services.Features.Plu;
using Ws.Domain.Services.Features.StorageMethod;
using Ws.Shared.TypeUtils;
using Ws.WebApiScales.Dto;
using Ws.WebApiScales.Features.Plu.Dto;
using Ws.WebApiScales.Features.Plu.Validators;

namespace Ws.WebApiScales.Features.Plu.Services;


internal sealed class PluApiService(
    ResponseDto responseDto, 
    IPluService pluService, 
    IBoxService boxService,
    IClipService clipService, 
    IBrandService brandService, 
    IBundleService bundleService, 
    IStorageMethodService storageMethodService) : IPluApiService 
{
    #region Private

    private static void SetPluIsMarked(PluEntity plu)
    {
        if (plu.IsNew) return;
        SqlCoreHelper.Instance.Delete(plu);
    }
    
    private ClipEntity SaveOrUpdateClip(PluDto pluDto)
    {
        ClipEntity clipDb = clipService.GetByUid1С(pluDto.ClipTypeGuid);
        if (clipDb.IsNew) return clipService.GetDefault();
        
        clipDb = pluDto.AdaptTo(clipDb);
        SqlCoreHelper.Instance.SaveOrUpdate(clipDb);
        return clipDb;
    }
    
    private BoxEntity SaveOrUpdateBox(PluDto pluDto)
    {
        BoxEntity boxDb = boxService.GetByUid1С(pluDto.BoxTypeGuid);
        if (boxDb.IsNew) return boxService.GetDefault();
        
        boxDb = pluDto.AdaptTo(boxDb);
        SqlCoreHelper.Instance.SaveOrUpdate(boxDb);
        return boxDb;
    }
    
    private BundleEntity SaveOrUpdateBundle(PluDto pluDto)
    {
        BundleEntity bundleDb = bundleService.GetByUid1С(pluDto.PackageTypeGuid);
        if (bundleDb.IsNew) return bundleService.GetDefault();
        
        bundleDb = pluDto.AdaptTo(bundleDb);
        SqlCoreHelper.Instance.SaveOrUpdate(bundleDb);
        return bundleDb;
    }
    
    private void SaveOrUpdatePluFk(PluEntity plu, PluDto pluDto)
    {
        if (Equals(pluDto.ParentGroupGuid, Guid.Empty)) return;
        if (plu.IsNew) return;
        
        PluEntity parentPluDb = pluService.GetByUid1С(pluDto.ParentGroupGuid);
        if (parentPluDb.IsNew) return;
        
        PluEntity categoryDb = pluService.GetByUid1С(pluDto.CategoryGuid);
        PluFkEntity pluFkDb = pluService.GetParent(plu);
        
        pluFkDb.Plu = plu;
        pluFkDb.Parent = parentPluDb;
        pluFkDb.Category = categoryDb.IsExists ? categoryDb : null;
        
        SqlCoreHelper.Instance.SaveOrUpdate(pluFkDb);
    }
    
    private void SaveOrUpdatePlu(PluEntity plu, PluDto pluDto, BundleEntity bundle)
    {
        plu = pluDto.AdaptTo(plu);
        plu.Bundle = bundle;
        plu.Brand = brandService.GetByUid1С(pluDto.BrandGuid);
        plu.StorageMethod = storageMethodService.GetByNameOrDefault(pluDto.StorageMethod);
        SqlCoreHelper.Instance.SaveOrUpdate(plu);
    }
    
    private void SaveOrUpdatePluNesting(PluEntity plu, PluDto pluDto, BoxEntity box)
    {
        PluNestingEntity pluNestingDb = pluService.GetDefaultNesting(plu);

        pluNestingDb.Plu = plu;
        pluNestingDb.Box = box;
        pluNestingDb.BundleCount = pluDto.AttachmentsCount;
        SqlCoreHelper.Instance.SaveOrUpdate(pluNestingDb);
    }

    #endregion
    
    public void Load(PlusWrapper plusWrapper)
    {
        List<PluDto> orderedEnumerable = plusWrapper.Plus.OrderBy(item => item.PluNumber).ToList();
        
        foreach (PluDto pluDto in orderedEnumerable)
        {
            PluEntity pluDb = pluService.GetByUid1С(pluDto.Uid);
            
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
            
            ClipEntity _ = SaveOrUpdateClip(pluDto);
            BoxEntity box = SaveOrUpdateBox(pluDto);
            BundleEntity bundle = SaveOrUpdateBundle(pluDto);
            
            SaveOrUpdatePlu(pluDb, pluDto, bundle);
            SaveOrUpdatePluFk(pluDb, pluDto);
            SaveOrUpdatePluNesting(pluDb, pluDto, box);
            
            responseDto.AddSuccess(pluDto.Uid, $"{IntUtils.ToStringToLen(pluDb.Number, 3)} | {pluDb.Name}");
        }
    }
}