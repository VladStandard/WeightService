using FluentValidation.Results;
using Ws.StorageCore.Entities.SchemaRef1c.Boxes;
using Ws.StorageCore.Entities.SchemaRef1c.Plus;
using Ws.StorageCore.Entities.SchemaScale.PlusNestingFks;
using Ws.WebApiScales.Common.Services;
using Ws.WebApiScales.Dto.PluCharacteristic;
using Ws.WebApiScales.Dto.Response;

namespace Ws.WebApiScales.Services;

public class PluCharacteristicService(ResponseDto responseDto) : IPluCharacteristicService
{
    private readonly SqlPluNestingFkRepository _pluNestingFkRepository = new();
    
    #region Private

    private static IEnumerable<SqlPluEntity> GetPluEntities(IEnumerable<PluCharacteristicDto> characteristics)
    {
        List<Guid> uniquePluGuids = characteristics.Select(i => i.PluGuid).Distinct().ToList();
        return new SqlPluRepository().GetPluUid1CInRange(uniquePluGuids).ToList();
    }
    
    private void SetCharacteristicIsMarked(SqlPluEntity plu, PluCharacteristicDto pluCharacteristicDto)
    {
        string pluNameStr = $"{plu.Number} | {plu.Name}";
        SqlPluNestingFkEntity pluNestingFkDefault = _pluNestingFkRepository.GetDefaultByPlu(plu);
        
        if (pluNestingFkDefault.IsExists && pluNestingFkDefault.BundleCount.Equals((short)pluCharacteristicDto.AttachmentsCountAsInt))
        {
            responseDto.AddError(pluCharacteristicDto.Guid, $"{pluNameStr} - характеристика совпадает со вложенностью по-молчанию!");
            return;
        }
        
        SqlPluNestingFkEntity nesting = _pluNestingFkRepository.GetByPluAndUid1C(plu, pluCharacteristicDto.Guid);
        if (nesting.IsNew)
        {
            responseDto.AddSuccess(pluCharacteristicDto.Guid, $"{pluNameStr} - вложенность {pluCharacteristicDto.AttachmentsCountAsInt} не найдена для удаления!");
            return;
        }
        SqlCoreHelper.Instance.Delete(nesting);
        responseDto.AddSuccess(pluCharacteristicDto.Guid, $"{pluNameStr} - вложенность {pluCharacteristicDto.AttachmentsCountAsInt} удалена!");
    }
    
    private bool IsPluValid(SqlPluEntity plu, PluCharacteristicDto pluCharacteristicDto)
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

    private void PluCharacteristicSaveOrUpdate(SqlPluEntity plu, PluCharacteristicDto pluCharacteristicDto)
    {
        SqlPluNestingFkEntity pluNestingFkDefault = _pluNestingFkRepository.GetDefaultByPlu(plu);
        
        if (pluNestingFkDefault.IsExists && pluNestingFkDefault.BundleCount.Equals((short)pluCharacteristicDto.AttachmentsCountAsInt))
        {
            responseDto.AddError(pluCharacteristicDto.Guid, $"{plu.Number} | {plu.Name} - характеристика совпадает со вложенностью по-молчанию!");
            return;
        }
        
        SqlPluNestingFkEntity nesting = _pluNestingFkRepository.GetByPluAndUid1C(plu, pluCharacteristicDto.Guid);
        SqlBoxEntity boxDb = new SqlBoxRepository().GetItemByUid1C(new("71bc8e8a-99cf-11ea-a220-a4bf0139eb1b"));
        if (boxDb.IsNew)
        {
            responseDto.AddError(pluCharacteristicDto.Guid, "Невозможно установить коробку");
            return;
        }
        
        nesting.Plu = plu;
        nesting.Box = boxDb;
        nesting.IsDefault = false;
        nesting = pluCharacteristicDto.AdaptTo(nesting);
        
        SqlCoreHelper.Instance.SaveOrUpdate(nesting);
        responseDto.AddSuccess(pluCharacteristicDto.Guid, $"{plu.Number} | {plu.Name} | Кол-во вложений: {nesting.BundleCount}");
    }

    #endregion
    
    public void Load(PluCharacteristicsWrapper pluCharacteristics)
    {
        List<SqlPluEntity> plusCache = GetPluEntities(pluCharacteristics.Characteristics).ToList();
        
        List<Guid> pluBlackList = new();
        
        List<PluCharacteristicDto> pluCharacteristicDtos = 
            pluCharacteristics.Characteristics.OrderBy(item => item.PluGuid).ToList();
        
        foreach (PluCharacteristicDto pluCharacteristicDto in pluCharacteristicDtos)
        {
            if (pluBlackList.Contains(pluCharacteristicDto.PluGuid)) continue;
            
            SqlPluEntity pluDb = plusCache.FirstOrDefault(i => i.Uid1C == pluCharacteristicDto.PluGuid) ?? new();
            
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