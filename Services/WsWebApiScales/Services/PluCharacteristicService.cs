using FluentValidation.Results;

using WsStorageCore.Tables.TableScaleFkModels.PlusNestingFks;
using WsStorageCore.Tables.TableScaleModels.Plus;
using WsWebApiScales.Dto.PluCharacteristic;
using WsWebApiScales.Dto.Response;
using WsWebApiScales.Validators;

namespace WsWebApiScales.Services;

public class PluCharacteristicService
{
    
    private WsSqlPluRepository PluRepository { get; } = new();
    private WsSqlPluNestingFkRepository PluNestingFkRepository { get; } = new();
    
    public ActionResult<ResponseDto> LoadCharacteristics(PluCharacteristicsDto pluCharacteristics)
    {
        ResponseDto response = new();
        
        foreach (PluCharacteristicDto pluCharacteristicDto in pluCharacteristics.Characteristics)
        {
            PluCharacteristicDtoValidator dtoValidator = new();
            ValidationResult validationResult = dtoValidator.Validate(pluCharacteristicDto);

            if (!validationResult.IsValid)
            {
                List<string> errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                response.AddError(pluCharacteristicDto.Guid, string.Join(" | ", errors));
                continue;
            }
            
            List<WsSqlPluModel> plusDb = PluRepository.GetEnumerableByUid1C(pluCharacteristicDto.PluGuid).ToList();
            
            if (!plusDb.Any())
            {
                response.AddError(pluCharacteristicDto.Guid, WsLocaleCore.WebService.PluNotFound());
                continue;
            }

            // TODO FIX CHECKS
            
            #region TEMP
            
            if (plusDb.Count > 1)
            {
                response.AddError(pluCharacteristicDto.Guid, WsLocaleCore.WebService.PluFoundMoreThen1() + " | " + 
                                                             string.Join(", ", plusDb.Select(x => x.Number)));
                continue;
            }
            
            if (pluCharacteristicDto.AttachmentsCountAsDecimal % 1 != 0)
            {
                response.AddError(pluCharacteristicDto.Guid,WsLocaleCore.WebService.AttachmentsCountMustBeInt() + " | " +
                                                            string.Join(", ", plusDb.Select(x => x.Number)));
                continue;
            }

            #endregion
            
            WsSqlPluModel pluDb = plusDb.First();

            if (pluDb.IsCheckWeight)
            {
                response.AddError(pluCharacteristicDto.Guid, $"Номенклатура {pluDb.Uid1C} - весовая");
                continue;
            }
            WsSqlPluNestingFkModel pluNestingFkDefault = PluNestingFkRepository.GetDefaultByPlu(pluDb);

            if (pluNestingFkDefault.IsExists && pluNestingFkDefault.BundleCount.Equals((short)pluCharacteristicDto.AttachmentsCountAsDecimal))
            {
                response.AddError(pluCharacteristicDto.Guid,
                WsLocaleCore.WebService.FieldPluCharacteristicMustBeNotDefault());
                continue;
            }
            WsSqlPluNestingFkModel nesting = PluNestingFkRepository.GetByAttachmentsCount((short)pluCharacteristicDto.AttachmentsCountAsDecimal);
            
            nesting.IsMarked = pluCharacteristicDto.IsMarkedAsBool;
            nesting.Name = pluCharacteristicDto.Name;
            nesting.BundleCount = (short)pluCharacteristicDto.AttachmentsCountAsDecimal;
            nesting.IsDefault = false;
            
            if (nesting.IsNotNew)
                // Обновить найденную запись.
                WsServiceUtils.SqlCore.Update(nesting);
            else 
                WsServiceUtils.SqlCore.Save(nesting);
            
            response.AddSuccess(pluCharacteristicDto.Guid);

        }

        return response;
    }
}