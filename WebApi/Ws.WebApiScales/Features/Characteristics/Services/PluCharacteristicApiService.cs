using FluentValidation.Results;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Scale;
using Ws.Domain.Services.Features.Box;
using Ws.Domain.Services.Features.Plu;
using Ws.WebApiScales.Dto;
using Ws.WebApiScales.Features.Characteristics.Dto;
using Ws.WebApiScales.Features.Characteristics.Validators;

namespace Ws.WebApiScales.Features.Characteristics.Services;

internal sealed class PluCharacteristicApiService(
    ResponseDto responseDto, 
    IPluService pluService, 
    IBoxService boxService) : IPluCharacteristicApiService
{
    public void Load(CharacteristicsWrapper characteristics)
    {
        foreach (PluCharacteristicDto pluCharacteristicDto in characteristics.PluCharacteristics)
        {
            PluEntity pluDb = pluService.GetByUid1С(pluCharacteristicDto.Uid);
            
            if (pluDb.IsNew)
            {
                responseDto.AddError(pluCharacteristicDto.Uid, $"Номенклатура не найдена!");
                continue;
            }

            if (pluDb.IsCheckWeight)
            {
                responseDto.AddError(pluCharacteristicDto.Uid, $"{pluDb.DisplayName} - весовая!");
                continue;
            }
            
            foreach (CharacteristicDto characteristic in pluCharacteristicDto.Characteristics)
            {
                if (characteristic.IsDelete)
                {
                    pluService.DeleteNestingByUid1C(pluDb, characteristic.Uid);
                    responseDto.AddSuccess(characteristic.Uid, $"{pluDb.DisplayName} - вложенность {characteristic.BundleCount} удалена!");
                    continue;
                }
                
                ValidationResult validationResult = new ValidatorCharacteristicDto().Validate(characteristic);
                
                if (!validationResult.IsValid)
                {
                    List<string> errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                    responseDto.AddError(pluCharacteristicDto.Uid, string.Join(" | ", errors));
                    continue;
                }
                
                PluNestingEntity nesting = pluService.GetNestingByUid1C(pluDb, characteristic.Uid);
                
                if (nesting.IsNew) boxService.GetDefaultForCharacteristic();
                    
                nesting.Plu = pluDb;
                nesting = characteristic.AdaptTo(nesting);
                SqlCoreHelper.Instance.SaveOrUpdate(nesting);
                
                responseDto.AddSuccess(pluCharacteristicDto.Uid, $"{pluDb.DisplayName} | Кол-во вложений: {nesting.BundleCount}");
            }
        }
    }
}