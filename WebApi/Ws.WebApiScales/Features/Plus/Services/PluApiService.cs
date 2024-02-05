using FluentValidation.Results;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Scale;
using Ws.Domain.Services.Features.Box;
using Ws.Domain.Services.Features.Brand;
using Ws.Domain.Services.Features.Bundle;
using Ws.Domain.Services.Features.Clip;
using Ws.Domain.Services.Features.Plu;
using Ws.Domain.Services.Features.StorageMethod;
using Ws.WebApiScales.Dto;
using Ws.WebApiScales.Features.Plus.Dto;
using Ws.WebApiScales.Features.Plus.Validators;

namespace Ws.WebApiScales.Features.Plus.Services;


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
    
    private bool SaveOrUpdatePlu(PluDto pluDto)
    {
        ClipEntity clip = pluDto.ClipUid.Equals(Guid.Empty) ? 
            clipService.GetDefault() : clipService.GetItemByUid1С(pluDto.ClipUid);
            
        if (clip.IsNew)
        { 
            responseDto.AddError(pluDto.Uid, $"{pluDto.Number} | {pluDto.Name} | клипса не найдена");
            return false;
        }
        
        BoxEntity box = boxService.GetItemByUid1С(pluDto.BoxUid);
        if (box.IsNew)
        { 
            responseDto.AddError(pluDto.Uid, $"{pluDto.Number} | {pluDto.Name} | коробка не найдена");
            return false;
        }
            
        BundleEntity bundle = bundleService.GetItemByUid1С(pluDto.BundleUid);
        if (bundle.IsNew)
        { 
            responseDto.AddError(pluDto.Uid, $"{pluDto.Number} | {pluDto.Name} | пакет не найден");
            return false;
        }
        
        BrandEntity brand = brandService.GetItemByUid1С(pluDto.BrandUid);
        brand = brand.IsNew ? brandService.GetDefault() : brand;
        
        PluEntity pluDb = pluService.GetItemByUid1С(pluDto.Uid);
        pluDb = pluDto.AdaptTo(pluDb);
        
        pluDb.Bundle = bundle;
        pluDb.Brand = brand;
        pluDb.Clip = clip;
        pluDb.StorageMethod = storageMethodService.GetByNameOrDefault(pluDto.StorageMethod);
        
        SqlCoreHelper.Instance.SaveOrUpdate(pluDb);
        SaveOrUpdateDefaultNesting(pluDb, box, pluDto.BundleCount);
        
        return true;
    }
    
    private void SaveOrUpdateDefaultNesting(PluEntity plu, BoxEntity box, short bundleCount)
    {
        PluNestingEntity pluNestingDb = pluService.GetDefaultNesting(plu);

        pluNestingDb.Plu = plu;
        pluNestingDb.Box = box;
        pluNestingDb.BundleCount = bundleCount;
        SqlCoreHelper.Instance.SaveOrUpdate(pluNestingDb);
    }

    #endregion
    
    public void Load(PlusWrapper plusWrapper)
    {
        List<PluDto> orderedEnumerable = plusWrapper.Plus.OrderBy(item => item.Number).ToList();
        
        foreach (PluDto pluDto in orderedEnumerable)
        {
            PluEntity pluDb = pluService.GetItemByUid1С(pluDto.Uid);
            
            if (pluDto.IsDelete)
            {
                pluService.DeleteAllPluNestings(pluDb);
                responseDto.AddSuccess(pluDto.Uid, $"{pluDb.DisplayName} | Помечена на удаление");
                continue;
            }
            
            ValidationResult validationResult = new ValidatorPluDto().Validate(pluDto);
            
            if (!validationResult.IsValid)
            {
                List<string> errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                responseDto.AddError(pluDto.Uid, $" {pluDto.Name} | {pluDto.Number} | {string.Join(" | ", errors)}");
                continue;
            }
            
            if (SaveOrUpdatePlu(pluDto))
                responseDto.AddSuccess(pluDto.Uid, $"{pluDb.DisplayName} | {pluDb.Name}");
        }
    }
}