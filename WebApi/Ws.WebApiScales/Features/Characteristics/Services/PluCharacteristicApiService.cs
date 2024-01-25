using FluentValidation.Results;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Scale;
using Ws.Domain.Services.Features.Box;
using Ws.Domain.Services.Features.Plu;
using Ws.WebApiScales.Dto;
using Ws.WebApiScales.Features.Nesting.Dto;
using Ws.WebApiScales.Features.Nesting.Validators;

namespace Ws.WebApiScales.Features.Nesting.Services;

internal sealed class PluCharacteristicApiService(
    ResponseDto responseDto,
    IPluService pluService,
    IBoxService boxService) : IPluCharacteristicApiService
{
    #region Private

    private IEnumerable<PluEntity> GetPluEntities(IEnumerable<PluCharacteristicDto> characteristics)
    {
        List<Guid> uniquePluGuids = characteristics.Select(i => i.PluGuid).Distinct().ToList();
        return pluService.GetInRange(uniquePluGuids).ToList();
    }
    
    private void SetCharacteristicIsMarked(PluEntity plu, PluCharacteristicDto pluCharacteristicDto)
    {
        string pluNameStr = $"{plu.Number} | {plu.Name}";
        PluNestingEntity pluNestingDefault = pluService.GetDefaultNesting(plu);
        
        if (pluNestingDefault.IsExists && pluNestingDefault.BundleCount.Equals((short)pluCharacteristicDto.AttachmentsCountAsInt))
        {
            responseDto.AddError(pluCharacteristicDto.Guid, $"{pluNameStr} - характеристика совпадает со вложенностью по-молчанию!");
            return;
        }
        
        pluService.DeleteNestingByUid1C(plu, pluCharacteristicDto.Guid);
        responseDto.AddSuccess(pluCharacteristicDto.Guid, $"{pluNameStr} - вложенность {pluCharacteristicDto.AttachmentsCountAsInt} удалена!");
    }
    
    private bool IsPluValid(PluEntity plu, PluCharacteristicDto pluCharacteristicDto)
    {
        if (plu.IsNew)
        {
            responseDto.AddError(pluCharacteristicDto.Guid, $"{pluCharacteristicDto.Name} | не найдено!");
            return false;
        }

        if (!plu.IsCheckWeight)
            return true;
        
        responseDto.AddError(pluCharacteristicDto.Guid, $"{plu.Number} | {plu.Name} - Весовая");
        return false;
    }

    private void PluCharacteristicSaveOrUpdate(PluEntity plu, PluCharacteristicDto pluCharacteristicDto)
    {
        PluNestingEntity pluNestingDefault = pluService.GetDefaultNesting(plu);
        
        if (pluNestingDefault.IsExists && pluNestingDefault.BundleCount.Equals((short)pluCharacteristicDto.AttachmentsCountAsInt))
        {
            responseDto.AddError(pluCharacteristicDto.Guid, $"{plu.Number} | {plu.Name} - характеристика совпадает со вложенностью по-молчанию!");
            return;
        }
        
        PluNestingEntity nesting = pluService.GetNestingByUid1C(plu, pluCharacteristicDto.Guid);
        
        nesting.Plu = plu;
        nesting.Box = boxService.GetDefaultForCharacteristic();
        nesting = pluCharacteristicDto.AdaptTo(nesting);
        
        SqlCoreHelper.Instance.SaveOrUpdate(nesting);
        responseDto.AddSuccess(pluCharacteristicDto.Guid, $"{plu.Number} | {plu.Name} | Кол-во вложений: {nesting.BundleCount}");
    }

    #endregion
    
    public void Load(PluCharacteristicsWrapper pluCharacteristics)
    {
        List<PluEntity> plusCache = GetPluEntities(pluCharacteristics.Characteristics).ToList();
        
        List<Guid> pluBlackList = [];
        
        List<PluCharacteristicDto> pluCharacteristicDtos = 
            pluCharacteristics.Characteristics.OrderBy(item => item.PluGuid).ToList();
        
        foreach (PluCharacteristicDto pluCharacteristicDto in pluCharacteristicDtos)
        {
            if (pluBlackList.Contains(pluCharacteristicDto.PluGuid)) continue;
            
            PluEntity pluDb = plusCache.FirstOrDefault(i => i.Uid1C == pluCharacteristicDto.PluGuid) ?? new();
            
            if (pluCharacteristicDto.IsMarked)
            { 
                SetCharacteristicIsMarked(pluDb, pluCharacteristicDto);
                continue;
            }
            
            ValidationResult validationResult = new ValidatorPluCharacteristicDto().Validate(pluCharacteristicDto);
            
            if (!validationResult.IsValid)
            {
                List<string> errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                responseDto.AddError(pluCharacteristicDto.Guid, string.Join(" | ", errors));
                continue;
            }

            if (IsPluValid(pluDb, pluCharacteristicDto))
            {
                PluCharacteristicSaveOrUpdate(pluDb, pluCharacteristicDto);
                continue;
            }
            pluBlackList.Add(pluCharacteristicDto.PluGuid);
        }
    }
}