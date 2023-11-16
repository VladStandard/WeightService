using FluentValidation.Results;
using Ws.WebApiScales.Dto.PluCharacteristic;
using Ws.WebApiScales.Dto.Response;
using Ws.WebApiScales.Validators;
using Ws.StorageCore.Entities.SchemaDiag.LogsWebs;
using Ws.StorageCore.Entities.SchemaRef1c.Boxes;
using Ws.StorageCore.Entities.SchemaRef1c.Plus;
using Ws.StorageCore.Entities.SchemaScale.PlusNestingFks;
using WsWebApiCore.Utils;

namespace Ws.WebApiScales.Services;

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
        
        SqlPluRepository pluRepository = new();
        foreach (PluCharacteristicDto pluCharacteristicDto in pluCharacteristics.Characteristics.OrderBy(item=>item.AttachmentsCountAsInt))
        {
            SqlPluEntity pluDb = pluRepository.GetByUid1C(pluCharacteristicDto.PluGuid);

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
        new SqlLogWebRepository().Save(requestTime,   
        XmlUtil.SerializeToXml(pluCharacteristics),   
        XmlUtil.SerializeToXml(_responseDto), currentUrl, _responseDto.SuccessesCount, _responseDto.ErrorsCount);
        
        return _responseDto;
    }
    
    //TODO: REFACTOR
    #region REFACTORED
    
    private void SetCharacteristicIsMarked(SqlPluEntity plu, PluCharacteristicDto pluCharacteristicDto)
    {
        SqlPluNestingFkRepository pluNestingFkRepository = new();
        
        SqlPluNestingFkEntity pluNestingFkDefault = pluNestingFkRepository.GetDefaultByPlu(plu);
        
        if (pluNestingFkDefault.IsExists && 
            pluNestingFkDefault.BundleCount.Equals((short)pluCharacteristicDto.AttachmentsCountAsInt))
        {
            _responseDto.AddError(pluCharacteristicDto.Guid, $"Номенклатура {plu.Number} - характеристика совпадает со вложенностью по-молчанию!");
            return;
        }
        SqlPluNestingFkEntity nesting = pluNestingFkRepository.GetByPluAndUid1C(plu, pluCharacteristicDto.Guid);
        if (nesting.IsNew)
        {
            _responseDto.AddSuccess(pluCharacteristicDto.Guid, $"Номенклатура {plu.Number} - вложенность {pluCharacteristicDto.AttachmentsCountAsInt} не найдена для удаления!");
            return;
        }
   
        nesting.IsMarked = true;
        SqlCoreHelper.Instance.Update(nesting);
        
        _responseDto.AddSuccess(pluCharacteristicDto.Guid, $"Номенклатура {plu.Number} - вложенность {pluCharacteristicDto.AttachmentsCountAsInt} удалена!");
    }
    
    private bool IsPluValid(SqlPluEntity plu, PluCharacteristicDto pluCharacteristicDto)
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

    private void PluCharacteristicSaveOrUpdate(SqlPluEntity plu, PluCharacteristicDto pluCharacteristicDto)
    {
        SqlPluNestingFkRepository pluNestingFkRepository = new();
        
        SqlPluNestingFkEntity pluNestingFkDefault = pluNestingFkRepository.GetDefaultByPlu(plu);
        
        if (pluNestingFkDefault.IsExists && 
            pluNestingFkDefault.BundleCount.Equals((short)pluCharacteristicDto.AttachmentsCountAsInt))
        {
            _responseDto.AddError(pluCharacteristicDto.Guid, $"Номенклатура {plu.Number} - характеристика совпадает со вложенностью по-молчанию!");
            return;
        }
            
        SqlPluNestingFkEntity nesting = pluNestingFkRepository.GetByPluAndUid1C(plu, pluCharacteristicDto.Guid);
        
        SqlBoxEntity boxDb = new SqlBoxRepository().GetItemByUid1C(new("71bc8e8a-99cf-11ea-a220-a4bf0139eb1b"));
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
            SqlCoreHelper.Instance.Save(nesting);
        else 
            SqlCoreHelper.Instance.Update(nesting);
        _responseDto.AddSuccess(pluCharacteristicDto.Guid, $"Номенклатура: {plu.Number} / Удалить {pluCharacteristicDto.IsMarked} / AttachmentsCount {nesting.BundleCount}");

    }
    
    #endregion
}