using FluentValidation.Results;
using WsStorageCore.Tables.TableDiagModels.LogsWebs;
using WsStorageCore.Tables.TableScaleFkModels.PlusNestingFks;
using WsStorageCore.Tables.TableScaleModels.Boxes;
using WsStorageCore.Tables.TableScaleModels.Plus;
using WsWebApiScales.Dto.PluCharacteristic;
using WsWebApiScales.Dto.Response;
using WsWebApiScales.Utils;
using WsWebApiScales.Validators;

namespace WsWebApiScales.Services;

public class PluCharacteristicService
{
    private readonly ResponseDto _responseDto;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public PluCharacteristicService(ResponseDto responseDto, IHttpContextAccessor httpContextAccessor)
    {
        _responseDto = responseDto;
        _httpContextAccessor = httpContextAccessor;
    }

    public ActionResult<ResponseDto> LoadCharacteristics(PluCharacteristicsDto pluCharacteristics)
    {
        
        DateTime requestTime = DateTime.Now;
        string currentUrl = _httpContextAccessor?.HttpContext?.Request.Path ?? string.Empty; 
        
        WsSqlPluRepository pluRepository = new();
        foreach (PluCharacteristicDto pluCharacteristicDto in pluCharacteristics.Characteristics.OrderBy(item=>item.AttachmentsCountAsInt))
        {
            WsSqlPluModel pluDb = pluRepository.GetByUid1C(pluCharacteristicDto.PluGuid);

            if (pluCharacteristicDto.IsMarked)
            {
                SetCharacteristicIsMarked(pluDb, pluCharacteristicDto);
                continue;
            }
            
            ValidationResult validationResult = new PluCharacteristicDtoValidator().Validate(pluCharacteristicDto);

            if (!validationResult.IsValid)
            {
                List<string> errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                _responseDto.AddError(pluCharacteristicDto.Guid, string.Join(" | ", errors));
                continue;
            }
            
            if (IsPluValid(pluDb, pluCharacteristicDto) == false) continue;
            
            PluCharacteristicSaveOrUpdate(pluDb, pluCharacteristicDto);

        }
        new WsSqlLogWebRepository().Save(requestTime,   
        XmlUtil.SerializeToXml(pluCharacteristics),   
        XmlUtil.SerializeToXml(_responseDto), currentUrl, _responseDto.SuccessesCount, _responseDto.ErrorsCount);
        
        return _responseDto;
    }
    
    //TODO: REFACTOR
    #region REFACTORED
    
    private void SetCharacteristicIsMarked(WsSqlPluModel plu, PluCharacteristicDto pluCharacteristicDto)
    {
        WsSqlPluNestingFkRepository pluNestingFkRepository = new();
        
        WsSqlPluNestingFkModel pluNestingFkDefault = pluNestingFkRepository.GetDefaultByPlu(plu);
        
        if (pluNestingFkDefault.IsExists && 
            pluNestingFkDefault.BundleCount.Equals((short)pluCharacteristicDto.AttachmentsCountAsInt))
        {
            _responseDto.AddError(pluCharacteristicDto.Guid, $"Номенклатура {plu.Number} - характеристика совпадает со вложенностью по-молчанию!");
            return;
        }
        WsSqlPluNestingFkModel nesting = pluNestingFkRepository.GetByPluAndUid1C(plu, pluCharacteristicDto.Guid);
        if (nesting.IsNew)
        {
            _responseDto.AddSuccess(pluCharacteristicDto.Guid, $"Номенклатура {plu.Number} - вложенность {pluCharacteristicDto.AttachmentsCountAsInt} не найдена для удаления!");
            return;
        }
   
        nesting.IsMarked = true;
        WsServiceUtils.SqlCore.Update(nesting);
        
        _responseDto.AddSuccess(pluCharacteristicDto.Guid, $"Номенклатура {plu.Number} - вложенность {pluCharacteristicDto.AttachmentsCountAsInt} удалена!");
    }
    
    private bool IsPluValid(WsSqlPluModel plu, PluCharacteristicDto pluCharacteristicDto)
    {
        if (plu.IsNew)
        {
            _responseDto.AddError(pluCharacteristicDto.Guid, $"Номенклатуры {pluCharacteristicDto.PluGuid} не найдено!");
            return false;
        }
        if (plu.IsCheckWeight)
        {
            _responseDto.AddError(pluCharacteristicDto.Guid, $"Номенклатура {plu.Number} - весовая");
            return false;
        }
        return true;
    }

    private void PluCharacteristicSaveOrUpdate(WsSqlPluModel plu, PluCharacteristicDto pluCharacteristicDto)
    {
        WsSqlPluNestingFkRepository pluNestingFkRepository = new();
        
        WsSqlPluNestingFkModel pluNestingFkDefault = pluNestingFkRepository.GetDefaultByPlu(plu);
        
        if (pluNestingFkDefault.IsExists && 
            pluNestingFkDefault.BundleCount.Equals((short)pluCharacteristicDto.AttachmentsCountAsInt))
        {
            _responseDto.AddError(pluCharacteristicDto.Guid, $"Номенклатура {plu.Number} - характеристика совпадает со вложенностью по-молчанию!");
            return;
        }
            
        WsSqlPluNestingFkModel nesting = pluNestingFkRepository.GetByPluAndUid1C(plu, pluCharacteristicDto.Guid);
        
        WsSqlBoxModel boxDb = new WsSqlBoxRepository().GetItemByUid1C(new("71bc8e8a-99cf-11ea-a220-a4bf0139eb1b"));
        if (boxDb.IsNew)
        {
            _responseDto.AddError(pluCharacteristicDto.Guid, "Невозможно установить коробку");
            return;
        }
        
        nesting.Plu = plu;
        nesting.Box = boxDb;
        nesting.IsDefault = false;
        nesting.Name = pluCharacteristicDto.Name;
        nesting.IsMarked = pluCharacteristicDto.IsMarked;
        nesting.BundleCount = (short)pluCharacteristicDto.AttachmentsCountAsInt;
        nesting.Uid1C = pluCharacteristicDto.Guid;
            
        if (nesting.IsNew)
            WsServiceUtils.SqlCore.Save(nesting);
        else 
            WsServiceUtils.SqlCore.Update(nesting);
        _responseDto.AddSuccess(pluCharacteristicDto.Guid, $"Номенклатура: {plu.Number} / Удалить {pluCharacteristicDto.IsMarked} / AttachmentsCount {nesting.BundleCount}");

    }
    
    #endregion
}